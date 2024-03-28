using Simple;

namespace net_srv;

public class Storage
{
    public Storage()
    {
        var response = new BarsResponse();
        response.Bars.AddRange(Enumerable.Range(0, 10_000)
            .Select(_ => new Bar
            {
                Open = 132.23, High = 4324, Low = 433, Close = 432
            }));
        
        Response = response;
        StaticResponse = System.Text.Json.JsonSerializer.Serialize(response);
    }

    public string StaticResponse { get; }

    public BarsResponse Response { get; }
}