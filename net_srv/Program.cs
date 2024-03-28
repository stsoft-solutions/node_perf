using net_srv;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
// builder.WebHost.ConfigureKestrel(options =>
// {
//         var http2 = options.Limits.Http2;
//         http2.InitialConnectionWindowSize = 1024 * 1024 * 2; // 2 MB
//         http2.InitialStreamWindowSize = 1024 * 1024; // 1 MB
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GrpcBarsService>();
app.MapGet("/", () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.MapGet("/bars", () => Storage.Response);
app.MapGet("/bars-static", () => Storage.StaticResponse);

app.Run();