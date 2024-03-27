using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;
using NBomber.Contracts;
using NBomber.CSharp;
using Simple;

var grpcUrl = "http://localhost:50051";
var httpUrl = "http://localhost:3000";

var services = new ServiceCollection();

// Configure gRPC client using GrpcClientFactory
services.AddGrpcClient<BarService.BarServiceClient>("simple", options => { options.Address = new Uri(grpcUrl); });
services.AddHttpClient("simple_http", client => client.BaseAddress = new Uri(httpUrl));

var serviceProvider = services.BuildServiceProvider();
var grpcFactory = serviceProvider.GetRequiredService<GrpcClientFactory>();
var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

// Create clients
var httpClient = new HttpClient { BaseAddress = new Uri(httpUrl) };
var singleClient = grpcFactory.CreateClient<BarService.BarServiceClient>("simple");

// Define load simulation
LoadSimulation[] loadSimulations = [
    // keep 1 copy of scenario during 30 seconds
    Simulation.KeepConstant(1, TimeSpan.FromSeconds(30)),

    // ramp up from 0 to 50 copies    
    // duration: 30 seconds (it executes from [00:00:00] to [00:00:30])
    Simulation.RampingConstant(50, TimeSpan.FromSeconds(30)),

    // keep 50 copies of scenario during 30 seconds
    Simulation.KeepConstant(50, TimeSpan.FromSeconds(30)),

    // ramp down from 50 to 20 copies
    // duration: 30 seconds (it executes from [00:00:30] to [00:01:00])
    Simulation.RampingConstant(20, TimeSpan.FromSeconds(30))
];

// Define a scenario
var grpcScenario = Scenario.Create("scenario_grpc", async context =>
    {
        var grpcBars = await grpcFactory.CreateClient<BarService.BarServiceClient>("simple").GetBarsAsync(new Empty());
        return Response.Ok(grpcBars);
    })
    .WithLoadSimulations(loadSimulations);
var httpScenario = Scenario.Create("scenario_http", async context =>
    {
        var response = await httpClientFactory.CreateClient("simple_http").GetAsync("/bars");
        var httpBars = await response.Content.ReadFromJsonAsync<JsonBarsReponse>();
        return Response.Ok(httpBars);
    })
    .WithLoadSimulations(loadSimulations);
var httpStaticScenario = Scenario.Create("scenario_http_static", async context =>
    {
        var response = await httpClientFactory.CreateClient("simple_http").GetAsync("/bars-static");
        var httpBars = await response.Content.ReadFromJsonAsync<JsonBarsReponse>();
        return Response.Ok(httpBars);
    })
    .WithLoadSimulations(loadSimulations);

// Run scenarios
NBomberRunner.RegisterScenarios(httpStaticScenario)
    .Run();

return;

// Warmup
await singleClient.GetBarsAsync(new Empty());

var sw = Stopwatch.StartNew();
var grpcBars = await singleClient.GetBarsAsync(new Empty());
sw.Stop();
Console.WriteLine(
    $"gRPC duration: {sw.Elapsed}. Length: {grpcBars.Bars.Count:N0}. Size: {grpcBars.CalculateSize():N0}");


await httpClient.GetFromJsonAsync("/bars", typeof(BarsResponse));
sw.Restart();
var response = await httpClient.GetAsync("/bars");
var httpBars = await response.Content.ReadFromJsonAsync<JsonBarsReponse>();
sw.Stop();
Console.WriteLine(
    $"HTTP duration: {sw.Elapsed}. Length: {httpBars!.Bars.Length:N0}. Size: {response.Content.Headers.ContentLength:N0}");

sw.Restart();
response = await httpClient.GetAsync("/bars-static");
httpBars = await response.Content.ReadFromJsonAsync<JsonBarsReponse>();
sw.Stop();
Console.WriteLine(
    $"HTTP duration: {sw.Elapsed}. Length: {httpBars!.Bars.Length:N0}. Size: {response.Content.Headers.ContentLength:N0}");

public class JsonBarsReponse
{
    [JsonPropertyName("bars")] public JsonBar[] Bars { get; set; } = Array.Empty<JsonBar>();
}

public class JsonBar
{
    [JsonPropertyName("open")] public double Open { get; set; }

    [JsonPropertyName("high")] public double High { get; set; }

    [JsonPropertyName("low")] public double Low { get; set; }

    [JsonPropertyName("close")] public double Close { get; set; }
}