using CHUNO.ConsoleService.Constract.Proto;
using Grpc.Core;

namespace CHUNO.ConsoleService.Boundaries.Grpc
{
    public class GrpcProxyConfigService : ProxyConfigService.ProxyConfigServiceBase
    {
        public override Task<AddProxyConfigResponse> AddProxyConfig(AddProxyConfigRequest request, ServerCallContext context)
        {
            return base.AddProxyConfig(request, context);
        }

        public override Task<UpdateProxyConfigResponse> UpdateProxyConfig(UpdateProxyConfigRequest request, ServerCallContext context)
        {
            return base.UpdateProxyConfig(request, context);
        }

        public override Task<UpdateProxyConfigResponse> DeleteProxyConfig(AddProxyConfigRequest request, ServerCallContext context)
        {
            return base.DeleteProxyConfig(request, context);
        }
    }
}
