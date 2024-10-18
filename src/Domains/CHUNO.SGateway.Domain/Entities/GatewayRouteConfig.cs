using CHUNO.Framework.Domain.Abstractions;
using CHUNO.Framework.Domain.Primitives;

namespace CHUNO.SGateway.Domain.Entities
{
    public class GatewayRouteConfig : BaseAggregateRoot
    {
        public GatewayRouteConfig(Guid appClientId, 
            string routeUrl, 
            string destination
            ) :base(Guid.NewGuid()) {
            AppClientId = appClientId;
            RouteUrl = routeUrl;
            Destination = destination;
        }  

        public Guid AppClientId { get; private set; }
        public string RouteUrl { get; private set; }

        public string Destination { get; private set; }



    }
}
