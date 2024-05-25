// Set minimum thread pool size

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

var services = new ServiceCollection();

var httpClientPool = new ClientPool<HttpClient>();
var grpcClientPool = new ClientPool<BarService.BarServiceClient>();

var transport = args[0]; // "http"
var port = args[1]; //"5000"
var loadSize = args[2]; //"100"
var concurrentRequests = args[3]; //"50"
var grpcClientMode = args.Length > 4 ? Enum.Parse<GrpcMode>(args[4]) : GrpcMode.Pool;
var scenarioName = $"scenario_{transport}_{port}_{loadSize}_{concurrentRequests}_{grpcClientMode}";

ThreadPool.GetMaxThreads(out var workerThreads, out var completionPortThreads);
Console.WriteLine($"Max worker threads: {workerThreads}, Max completion port threads: {completionPortThreads}");

ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
Console.WriteLine($"Min worker threads: {workerThreads}, Min completion port threads: {completionPortThreads}");
ThreadPool.SetMinThreads((int)Math.Max(int.Parse(concurrentRequests) * 1.1, workerThreads), completionPortThreads);
ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
Console.WriteLine($"Min worker threads: {workerThreads}, Min completion port threads: {completionPortThreads}");

// Configure gRPC client using GrpcClientFactory
services.AddGrpcClient<BarService.BarServiceClient>("grpc",
    options => options.Address = new Uri($"http://localhost:{port}"));
services.AddHttpClient("http", client => client.BaseAddress = new Uri($"http://localhost:{port}"));

var serviceProvider = services.BuildServiceProvider();
var grpcFactory = serviceProvider.GetRequiredService<GrpcClientFactory>();
var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
var grpcClient = grpcFactory.CreateClient<BarService.BarServiceClient>("grpc");

// Function to get a client depending on the mode
var getGrpcClient = new Func<ScenarioInfo, BarService.BarServiceClient>(_ => grpcClient);
switch (grpcClientMode)
{
    case GrpcMode.Single:
        break;
    case GrpcMode.Pool:
        getGrpcClient = scenarioInfo => grpcClientPool.GetClient(scenarioInfo);
        break;
    case GrpcMode.Factory:
        getGrpcClient = _ => grpcFactory.CreateClient<BarService.BarServiceClient>("grpc");
        break;
    default:
        throw new ArgumentOutOfRangeException();
}

// Define a scenarios
var warmupDuration = TimeSpan.FromSeconds(10);
var grpcScenario100 = Scenario.Create(scenarioName, async context =>
    {
        try
        {
            var grpcBars = await getGrpcClient(context.ScenarioInfo).GetBar100Async(new Empty());
            return Response.Ok<BarsResponse>(grpcBars);
        }
        catch (Exception)
        {
            return Response.Fail();
        }
    }).WithInit(_ =>
    {
        if (grpcClientMode == GrpcMode.Pool) InitGrpcPool(concurrentRequests, grpcFactory, grpcClientPool);
        return Task.CompletedTask;
    })
    .WithWarmUpDuration(warmupDuration);

var grpcScenario5000 = Scenario.Create(scenarioName, async context =>
{
    try
    {
        var grpcBars = await getGrpcClient(context.ScenarioInfo).GetBar5000Async(new Empty());
        return Response.Ok<BarsResponse>(grpcBars);
    }
    catch (Exception)
    {
        return Response.Fail();
    }
}).WithInit(_ =>
{
    if (grpcClientMode == GrpcMode.Pool) InitGrpcPool(concurrentRequests, grpcFactory, grpcClientPool);
    return Task.CompletedTask;
}).WithWarmUpDuration(warmupDuration);


var httpScenario100 = Scenario.Create(scenarioName, async context =>
{
    HttpResponseMessage response;
    try
    {
        var client = httpClientPool.GetClient(context.ScenarioInfo);
        response = await client.GetAsync("/bar/100");
    }
    catch (Exception)
    {
        return Response.Fail();
    }

    if (!response.IsSuccessStatusCode) return Response.Fail();
    var bars = await response.Content.ReadFromJsonAsync<JsonBarsResponse>();
    if (bars == null) return Response.Fail();
    return Response.Ok<JsonBarsResponse>(bars);
}).WithInit(context =>
{
    InitHttpPool(concurrentRequests, httpClientFactory, httpClientPool);
    return Task.CompletedTask;
}).WithWarmUpDuration(warmupDuration);

var httpScenario5000 = Scenario.Create(scenarioName, async context =>
{
    HttpResponseMessage response;
    try
    {
        var client = httpClientPool.GetClient(context.ScenarioInfo);
        response = await client.GetAsync("/bar/5000");
    }
    catch (Exception)
    {
        return Response.Fail();
    }

    if (!response.IsSuccessStatusCode) return Response.Fail();
    var bars = await response.Content.ReadFromJsonAsync<JsonBarsResponse>();
    if (bars == null) return Response.Fail();
    return Response.Ok<JsonBarsResponse>(bars);
}).WithInit(context =>
{
    InitHttpPool(concurrentRequests, httpClientFactory, httpClientPool);
    return Task.CompletedTask;
}).WithWarmUpDuration(warmupDuration);


// Define load simulation
var loadSimulation = Simulation.KeepConstant(int.Parse(concurrentRequests), TimeSpan.FromSeconds(60));

var result = (transport, loadSize) switch
{
    ("http", "100") => RunSimulation(httpScenario100, loadSimulation),
    ("http", "5000") => RunSimulation(httpScenario5000, loadSimulation),
    ("grpc", "100") => RunSimulation(grpcScenario100, loadSimulation),
    ("grpc", "5000") => RunSimulation(grpcScenario5000, loadSimulation),
    _ => throw new InvalidOperationException()
};

return;

static NodeStats RunSimulation(ScenarioProps scenarioProps, LoadSimulation loadSimulation)
{
    return NBomberRunner.RegisterScenarios(scenarioProps.WithLoadSimulations(loadSimulation))
        .WithTargetScenarios(scenarioProps.ScenarioName)
        .WithReportFormats(ReportFormat.Md)
        .WithReportFileName(scenarioProps.ScenarioName + ".md")
        .WithReportingInterval(TimeSpan.FromSeconds(5))
        .Run();
}

void InitGrpcPool(string s, GrpcClientFactory factory, ClientPool<BarService.BarServiceClient> clientPool)
{
    for (var i = 0; i < int.Parse(s); i++)
    {
        var client = factory.CreateClient<BarService.BarServiceClient>("grpc");
        clientPool.AddClient(client);
    }
}

void InitHttpPool(string s, IHttpClientFactory factory, ClientPool<HttpClient> clientPool)
{
    for (var i = 0; i < int.Parse(s); i++)
    {
        var client = factory.CreateClient("http");
        clientPool.AddClient(client);
    }
}

public enum GrpcMode
{
    Single,
    Pool,
    Factory
}