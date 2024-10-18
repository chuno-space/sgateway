
using CHUNO.Framework.Domain.Primitives;

namespace CHUNO.SGateway.Domain.Entities
{
    public class AppClient : BaseAggregateRoot
    {
        public AppClient(string clientId):base(Guid.NewGuid()) {
            ClientId = clientId;
            Name = "";
        }

        public string ClientId { get; private set; }
        public string Name { get; set; }
    }
}
