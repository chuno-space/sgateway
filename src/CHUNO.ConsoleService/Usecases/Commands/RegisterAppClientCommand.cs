using CHUNO.Framework.Infrastructure.Messaging;
using CHUNO.ConsoleService.Constract.Proto;

namespace CHUNO.ConsoleService.Usecases.Commands
{
    public class RegisterAppClientCommand : ICommand<RegisterAppClientResponse>
    {
        public RegisterAppClientRequest Payload { get; set; }
        public RegisterAppClientCommand(RegisterAppClientRequest req) { 
            Payload = req;
        }
    }
}
