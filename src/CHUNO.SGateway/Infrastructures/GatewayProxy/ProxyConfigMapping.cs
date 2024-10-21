using CHUNO.SGateway.Domain.Entities;
using Yarp.ReverseProxy.Configuration;

namespace CHUNO.SGateway.Infrastructures.GatewayProxy
{
    public static class ProxyConfigMapping
    {
        public static ConfigurationSnapshot ToConfiugrationSnapshot(this IList<GatewayRouteConfig> gatewayRouteConfigs)
        {
            var newSnapshot = new ConfigurationSnapshot();
            foreach (var config in gatewayRouteConfigs)
            {
                var cluster = config!.ToCluster();
                newSnapshot.Clusters.Add(cluster);

                var route = config!.ToRoute();
                newSnapshot.Routes.Add(route);
            }

            return newSnapshot;
        }

        public static RouteConfig ToRoute(this GatewayRouteConfig config)
        {
            var match = new RouteMatch()
            {
                Hosts = new List<string>()
                {
                    config.RouteUrl
                },
            };
            return new RouteConfig
            {
                RouteId = config.Id.ToString(),
                Order = null,
                MaxRequestBodySize = null,
                ClusterId = config.GetClusterId(),
                AuthorizationPolicy = null,
                RateLimiterPolicy = null,
                OutputCachePolicy = null,

                TimeoutPolicy = null,
                Timeout = TimeSpan.FromSeconds(15),

                CorsPolicy = null,
                Metadata = null,
                Transforms = null,
                Match = match
            };
        }

        public static ClusterConfig ToCluster(this GatewayRouteConfig config)
        {
            var destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase);
            destinations.Add("dest1", new DestinationConfig
            {
                Address = config.Destination,
                Health = null,
                Metadata = null,
                Host = null
            });

            return new ClusterConfig
            {
                ClusterId = config.GetClusterId(),
                LoadBalancingPolicy = null,
                SessionAffinity = null,
                HealthCheck = null,
                HttpClient = null,
                HttpRequest = null,
                Metadata = null,
                Destinations = destinations,
            };
        }

        private static string GetClusterId(this GatewayRouteConfig config)
        {
            return config.Id.ToString();
        }
    }
}
