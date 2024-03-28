using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Simple;

namespace net_srv;

public class GrpcBarsService : BarService.BarServiceBase
{
    #region Overrides of BarServiceBase

    /// <inheritdoc />
    public override Task<BarsResponse> GetBars(Empty request, ServerCallContext context)
    {
        return Task.FromResult(Storage.Response);
    }

    #endregion
}