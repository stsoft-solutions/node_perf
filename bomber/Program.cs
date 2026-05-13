using System.Net.Http.Json;
using bomber;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;
using NBomber;
using NBomber.Contracts;
using NBomber.Contracts.Stats;
using NBomber.CSharp;
using Simple;
using Enum = System.Enum;

if (args.Length < 4)
{
    Console.Error.WriteLine("Usage: bomber <http|grpc> <port> <loadSize> <concurrentRequests> [Single|Pool|Factory]");
    return 1;
}

var transport = args[0];
var port = args[1];
var loadSize = args[2];
var concurrentRequests = int.Parse(args[3]);
var grpcClientMode = args.Length > 4 ? Enum.Parse<GrpcMode>(args[4]) : GrpcMode.Pool;

ConfigureThreadPool(concurrentRequests);

var services = new ServiceCollection();
services.AddGrpcClient<BarService.BarServiceClient>("grpc",
    options => options.Address = new Uri($"http://localhost:{port}"));
services.AddHttpClient("http", client => client.BaseAddress = new Uri($"http://localhost:{port}"));

using var serviceProvider = services.BuildServiceProvider();
var grpcFactory = serviceProvider.GetRequiredService<GrpcClientFactory>();
var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

var httpClientPool = new ClientPool<HttpClient>();
var grpcClientPool = new ClientPool<BarService.BarServiceClient>();

var warmupDuration = TimeSpan.FromSeconds(10);
var testDuration = TimeSpan.FromSeconds(60);
var scenarioName = $"scenario_{transport}_{port}_{loadSize}_{concurrentRequests}_{grpcClientMode}";
var loadSimulation = Simulation.KeepConstant(concurrentRequests, testDuration);

var scenario = transport switch
{
    "grpc" => BuildGrpcScenario(scenarioName, loadSize, concurrentRequests, grpcClientMode, grpcFactory, grpcClientPool, warmupDuration),
    "http" => BuildHttpScenario(scenarioName, loadSize, concurrentRequests, httpClientFactory, httpClientPool, warmupDuration),
    _ => throw new InvalidOperationException($"Unknown transport '{transport}'. Use 'http' or 'grpc'.")
};

RunSimulation(scenario, loadSimulation);
return 0;

static void ConfigureThreadPool(int concurrentRequests)
{
    ThreadPool.GetMaxThreads(out var maxWorker, out var maxIocp);
    Console.WriteLine($"Max worker threads: {maxWorker}, Max completion port threads: {maxIocp}");

    ThreadPool.GetMinThreads(out var minWorker, out var minIocp);
    Console.WriteLine($"Min worker threads: {minWorker}, Min completion port threads: {minIocp}");

    var targetMin = (int)Math.Ceiling(concurrentRequests * 1.1);
    ThreadPool.SetMinThreads(Math.Max(targetMin, minWorker), minIocp);

    ThreadPool.GetMinThreads(out minWorker, out minIocp);
    Console.WriteLine($"Min worker threads set to: {minWorker}, Min completion port threads: {minIocp}");
}

static ScenarioProps BuildGrpcScenario(
    string name, string loadSize, int concurrentRequests,
    GrpcMode mode, GrpcClientFactory factory, ClientPool<BarService.BarServiceClient> pool,
    TimeSpan warmup)
{
    BarService.BarServiceClient? singleClient = mode == GrpcMode.Single
        ? factory.CreateClient<BarService.BarServiceClient>("grpc")
        : null;

    BarService.BarServiceClient GetClient(ScenarioInfo info) => mode switch
    {
        GrpcMode.Single => singleClient!,
        GrpcMode.Pool => pool.GetClient(info),
        GrpcMode.Factory => factory.CreateClient<BarService.BarServiceClient>("grpc"),
        _ => throw new ArgumentOutOfRangeException(nameof(mode))
    };

    return Scenario.Create(name, async context =>
        {
            try
            {
                var client = GetClient(context.ScenarioInfo);
                BarsResponse response = loadSize switch
                {
                    "100" => await client.GetBar100Async(new Empty()),
                    "5000" => await client.GetBar5000Async(new Empty()),
                    _ => throw new InvalidOperationException($"Unknown gRPC loadSize: {loadSize}")
                };
                return response.Bars.Count > 0
                    ? Response.Ok(payload: response, sizeBytes: response.CalculateSize())
                    : Response.Fail<BarsResponse>("Empty bars response");
            }
            catch (Exception ex)
            {
                return Response.Fail<BarsResponse>(ex.Message);
            }
        })
        .WithInit(_ =>
        {
            if (mode == GrpcMode.Pool)
                for (var i = 0; i < concurrentRequests; i++)
                    pool.AddClient(factory.CreateClient<BarService.BarServiceClient>("grpc"));
            return Task.CompletedTask;
        })
        .WithWarmUpDuration(warmup);
}

static ScenarioProps BuildHttpScenario(
    string name, string loadSize, int concurrentRequests,
    IHttpClientFactory factory, ClientPool<HttpClient> pool,
    TimeSpan warmup)
{
    return Scenario.Create(name, async context =>
        {
            try
            {
                var client = pool.GetClient(context.ScenarioInfo);
                var httpResponse = await client.GetAsync($"/bar/{loadSize}");
                if (!httpResponse.IsSuccessStatusCode)
                    return Response.Fail<JsonBarsResponse>($"HTTP {(int)httpResponse.StatusCode}");
                var bars = await httpResponse.Content.ReadFromJsonAsync<JsonBarsResponse>();
                return bars?.Bars.Length > 0
                    ? Response.Ok(payload: bars, sizeBytes: (int)(httpResponse.Content.Headers.ContentLength ?? 0))
                    : Response.Fail<JsonBarsResponse>("Empty or null response");
            }
            catch (Exception ex)
            {
                return Response.Fail<JsonBarsResponse>(ex.Message);
            }
        })
        .WithInit(_ =>
        {
            for (var i = 0; i < concurrentRequests; i++)
                pool.AddClient(factory.CreateClient("http"));
            return Task.CompletedTask;
        })
        .WithWarmUpDuration(warmup);
}

static NodeStats RunSimulation(ScenarioProps scenarioProps, LoadSimulation loadSimulation)
{
    return NBomberRunner.RegisterScenarios(scenarioProps.WithLoadSimulations(loadSimulation))
        .WithTargetScenarios(scenarioProps.ScenarioName)
        .WithReportFormats(ReportFormat.Md)
        .WithReportFileName(scenarioProps.ScenarioName + ".md")
        .WithReportingInterval(TimeSpan.FromSeconds(5))
        .Run();
}

public enum GrpcMode
{
    Single,
    Pool,
    Factory
}
