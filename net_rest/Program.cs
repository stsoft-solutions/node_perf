using System.Net;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

Bar CreateBar() => new() { Open = 132.23, High = 4324, Low = 433, Close = 432 };
var response100 = new BarResponse("AAPL", Enumerable.Range(1, 100).Select(_ => CreateBar()).ToArray());
var response5000 = new BarResponse("AAPL", Enumerable.Range(1, 5000).Select(_ => CreateBar()).ToArray());

app.MapGet("/bar/100", () => response100);
app.MapGet("/bar/5000", () => response5000);

app.Run();

public record struct Bar(double Open, double High, double Low, double Close, double Volume);

public record struct BarResponse(string Symbol, Bar[] Bars);