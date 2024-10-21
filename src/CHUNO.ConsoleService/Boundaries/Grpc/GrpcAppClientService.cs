extern alias SGatewayConstract;
using SGatewayTypes = SGatewayConstract.CHUNO.SGateway.Constract.Proto;


using CHUNO.ConsoleService.Constract.Proto;
using CHUNO.Framework.Infrastructure.Authentication;
using Grpc.Core;

namespace CHUNO.ConsoleService.Boundaries.Grpc
{
    public class GrpcAppClientService : AppClientService.AppClientServiceBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly SGatewayTypes.SGateway.SGatewayClient _gatewayClient;
        public GrpcAppClientService(IServiceProvider serviceProvider, IUserProvider userProvider,
             SGatewayTypes.SGateway.SGatewayClient gatewayClient) {
            _serviceProvider = serviceProvider;
            _gatewayClient = gatewayClient;
        }
        public override async Task<RegisterAppClientResponse> RegisterAppClient(RegisterAppClientRequest request, ServerCallContext context)
        {
            var _userProvider = _serviceProvider.GetRequiredService<IUserProvider>(); 
            var claimsPrincipal = context.GetHttpContext().User;
            var user = _userProvider.User;
            var response = await _gatewayClient.ConfigUpdateAsync(new SGatewayTypes.ConfigUpdateRequest()
            {
               
            }, cancellationToken: context.CancellationToken);
            return new RegisterAppClientResponse
            {
                Status = new Common.Constract.Proto.ResponseStatus()
                {
                    Success = true,
                    Message ="User: "+ user?.UserId + ". " + response.Status?.Message
                }
            };
        }

        public override Task<DisableAppClientResponse> DisableAppClient(DisableAppClientRequest request, ServerCallContext context)
        {
            return base.DisableAppClient(request, context);
        }

        public override Task<ApplyProxyConfigResponse> ApplyProxyConfig(ApplyProxyConfigRequest request, ServerCallContext context)
        {
            return base.ApplyProxyConfig(request, context);
        }
    }
}
