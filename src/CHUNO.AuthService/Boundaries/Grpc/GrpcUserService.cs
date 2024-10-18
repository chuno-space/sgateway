using CHUNO.AuthService.Constract.Proto;
using Grpc.Core;

namespace CHUNO.AuthService.Boundaries.Grpc
{
    public class GrpcUserService : Greeter.GreeterBase
    {
        public GrpcUserService() { }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            var clientCertificate = httpContext.Connection.ClientCertificate;
            return new HelloReply()
            {
                Message = $"Pong To {request.Name} at {DateTime.Now.ToString("HH:mm:ss.fff")}. "
                + (clientCertificate?.Issuer?? "clientCertificate IS NULL")
            };
        }
    }
}
