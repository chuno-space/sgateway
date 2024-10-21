using CHUNO.ConsoleService.Constract.Proto;
using CHUNO.Framework.Domain.Primitives.Result;
using CHUNO.Framework.Infrastructure.Messaging;

namespace CHUNO.ConsoleService.Usecases.Commands
{
    public class RegisterAppClientCommandHandler : ICommandHandler<RegisterAppClientCommand, RegisterAppClientResponse>
    {
        public RegisterAppClientCommandHandler() { 
        }
        public Task<RegisterAppClientResponse> Handle(RegisterAppClientCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
