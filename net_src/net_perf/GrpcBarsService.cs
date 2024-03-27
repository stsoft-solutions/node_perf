using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Simple;

namespace net_perf;

public class GrpcBarsService(Storage storage) : BarService.BarServiceBase
{
    private readonly BarsResponse _response = storage.Response;

    #region Overrides of BarServiceBase

    /// <inheritdoc />
    public override Task<BarsResponse> GetBars(Empty request, ServerCallContext context)
    {
        return Task.FromResult(_response);
    }

    #endregion
}