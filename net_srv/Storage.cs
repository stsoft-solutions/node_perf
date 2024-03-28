using Simple;

namespace net_srv;

public static class Storage
{
    static Storage()
    {
        var response = new BarsResponse();
        response.Bars.AddRange(Enumerable.Range(0, 100)
            .Select(_ => new Bar
            {
                Open = 132.23, High = 4324, Low = 433, Close = 432
            }));
        
        Response = response;
        StaticResponse = System.Text.Json.JsonSerializer.Serialize(response);
    }


    public static string StaticResponse { get; }

    public static BarsResponse Response { get; }
}