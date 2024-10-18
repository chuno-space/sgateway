using CHUNO.SGateway.Data;
using CHUNO.SGateway.Domain.Entities;
namespace CHUNO.SGateway.Application
{
    public class RouteConfigStorage
    {
        private GatewayDBContext _dBContext;
        public RouteConfigStorage(GatewayDBContext dBContext) {
            _dBContext = dBContext;
        }
        public async Task<IList<GatewayRouteConfig>> GetGatewayRouteConfigs()
        {
            return new List<GatewayRouteConfig>();
        }
    }
}
