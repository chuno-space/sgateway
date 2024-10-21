using CHUNO.Common.Constract.Proto;
using CHUNO.SGateway.Constract.Proto;
using CHUNO.SGateway.Infrastructures.GatewayProxy.Interfaces;
using Grpc.Core;
namespace CHUNO.SGateway.Boundaries.Grpc
{
    public class GrpcGatewayService : Constract.Proto.SGateway.SGatewayBase
    {
        private readonly IGatewayProxyUpdater _gatewayProxyUpdater;
        public GrpcGatewayService(IGatewayProxyUpdater gatewayProxyManager) { 
            _gatewayProxyUpdater = gatewayProxyManager;
        }
        public override async Task<ConfigUpdateResponse> ConfigUpdate(ConfigUpdateRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();

            _gatewayProxyUpdater.ConfigUpdate();

            return new ConfigUpdateResponse()
            {
                Status = new ResponseStatus()
                {
                    Success = true,
                    Message = "OK1: " + DateTime.Now.ToString("HH:mm:ss.ffff"),
                    DetailErrorMessage = "Chuno"
                }
            };
        }
    }
}
