// Set minimum thread pool size

using System.Net.Http.Json;
using bomber;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;
using NBomber.Contracts;
using NBomber.Contracts.Stats;
using NBomber.CSharp;
using Simple;

ThreadPool.SetMinThreads(1300, 1300);

var services = new ServiceCollection();



var transport = args[0]; // "http"
var port = args[1]; //"5000"
var loadSize = args[2]; //"100" 
var concurrentRequests = args[3]; //"50"
var scenarioName = $"scenario_{transport}_{port}_{loadSize}_{concurrentRequests}";

// Configure gRPC client using GrpcClientFactory
services.AddGrpcClient<BarService.BarServiceClient>("grpc",options => options.Address = new Uri($"http://localhost:{port}"));
services.AddHttpClient("http", client => client.BaseAddress = new Uri($"http://localhost:{port}"));

var serviceProvider = services.BuildServiceProvider();
var grpcFactory = serviceProvider.GetRequiredService<GrpcClientFactory>();
var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();


// Define a scenario
var grpcScenario100 = Scenario.Create(scenarioName, async context =>
{
    try
    {
        var grpcBars = await grpcFactory.CreateClient<BarService.BarServiceClient>("grpc")
            .GetBar100Async(new Empty());
        return Response.Ok<BarsResponse>(payload: grpcBars);
    }
    catch (Exception)
    {
        return Response.Fail();
    }
});
// Define a scenario
var grpcScenario5000 = Scenario.Create(scenarioName, async context =>
{
    try
    {
        var grpcBars = await grpcFactory.CreateClient<BarService.BarServiceClient>("grpc")
            .GetBar5000Async(new Empty());
        return Response.Ok<BarsResponse>(payload: grpcBars);
    }
    catch (Exception)
    {
        return Response.Fail();
    }
});

var httpScenario100 = Scenario.Create(scenarioName, async context =>
{
    HttpResponseMessage response;
    try
    {
        response = await httpClientFactory.CreateClient("http").GetAsync("/bar/100");
    }
    catch (Exception )
    {
        return Response.Fail();
    }
    if (!response.IsSuccessStatusCode) return Response.Fail();
    var bars = await response.Content.ReadFromJsonAsync<JsonBarsResponse>();
    if (bars == null) return Response.Fail();
    return Response.Ok<JsonBarsResponse>(payload: bars);
});

var httpScenario5000 = Scenario.Create(scenarioName, async context =>
{
    HttpResponseMessage response;
    try
    {
        response = await httpClientFactory.CreateClient("http").GetAsync("/bar/5000");
    }
    catch (Exception )
    {
        return Response.Fail();
    }
    if (!response.IsSuccessStatusCode) return Response.Fail();
    var bars = await response.Content.ReadFromJsonAsync<JsonBarsResponse>();
    if (bars == null) return Response.Fail();
    return Response.Ok<JsonBarsResponse>(payload: bars);
});

// Define load simulation
var loadSimulation = Simulation.KeepConstant(int.Parse(concurrentRequests), TimeSpan.FromMinutes(2));

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
        .Run();
}

