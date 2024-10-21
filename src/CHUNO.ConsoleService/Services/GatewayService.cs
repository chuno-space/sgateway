extern alias SGatewayConstract;

using SGatewayClient = SGatewayConstract.CHUNO.SGateway.Constract.Proto.SGateway.SGatewayClient;

using Grpc.Net.ClientFactory;
namespace CHUNO.ConsoleService.Services
{
    public class GatewayService
    {
        private readonly SGatewayClient _gatewayClient;
        public GatewayService(GrpcClientFactory grpcClientFactory, SGatewayClient gatewayClient)
        {
            _gatewayClient = gatewayClient;
        }


    }
}
