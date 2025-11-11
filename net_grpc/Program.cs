using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Simple;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to allow more concurrent HTTP/2 streams (gRPC calls) per connection
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.Http2.MaxStreamsPerConnection = 600;
});

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<BarServiceGrpc>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

public class BarServiceGrpc : BarService.BarServiceBase
{
    #region Overrides of BarServiceBase

    /// <inheritdoc />
    public override Task<BarsResponse> GetBar100(Empty request, ServerCallContext context)
    {
        return Task.FromResult(Storage.BarsResponse100);
    }

    /// <inheritdoc />
    public override Task<BarsResponse> GetBar5000(Empty request, ServerCallContext context)
    {
        return Task.FromResult(Storage.BarsResponse5000);
    }

    #endregion
}

public static class Storage
{
    private static Bar CreateBar() => new() { Open = 132.23, High = 4324, Low = 433, Close = 432 };

    static Storage()
    {
        BarsResponse100 = new BarsResponse
        {
            Symbol = "AAPL",
            Bars = { Enumerable.Range(0, 100).Select(_ => CreateBar()) }
        };
        BarsResponse5000 = new BarsResponse
        {
            Symbol = "AAPL",
            Bars = { Enumerable.Range(0, 5000).Select(_ => CreateBar()) }
        };
    }

    public static BarsResponse BarsResponse100 { get; }
    public static BarsResponse BarsResponse5000 { get; }
}