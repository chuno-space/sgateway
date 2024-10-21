using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;

namespace CHUNO.SGateway.Infrastructures.GatewayProxy
{
    public class GatewayProxyConfig : IProxyConfig
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public GatewayProxyConfig(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
            : this(routes, clusters, Guid.NewGuid().ToString())
        { }

        public GatewayProxyConfig(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters, string revisionId)
        {
            RevisionId = revisionId ?? throw new ArgumentNullException(nameof(revisionId));
            Routes = routes;
            Clusters = clusters;
            ChangeToken = new CancellationChangeToken(_cts.Token);
        }

        /// <inheritdoc/>
        public string RevisionId { get; }

        internal void SignalChange()
        {
            _cts.Cancel();
        }

        // impelement interface
        public IReadOnlyList<RouteConfig> Routes { get; }
        public IReadOnlyList<ClusterConfig> Clusters { get; }

        public IChangeToken ChangeToken { get; }

    }
}
