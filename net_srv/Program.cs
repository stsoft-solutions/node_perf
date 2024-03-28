using net_srv;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddSingleton<Storage>(provider => new Storage());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GrpcBarsService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.MapGet("/bars", (Storage storage) => storage.Response);
app.MapGet("/bars-static", (Storage storage) => storage.StaticResponse);

app.Run();