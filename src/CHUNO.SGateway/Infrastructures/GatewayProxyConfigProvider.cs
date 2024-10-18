using Microsoft.Extensions.Primitives;
using System.Diagnostics.CodeAnalysis;
using Yarp.ReverseProxy.Configuration;

namespace CHUNO.SGateway.Infrastructures
{
    internal class GatewayProxyConfigProvider : IProxyConfigProvider
    {
        private readonly object _lockObject = new();
        private readonly ILogger<GatewayProxyConfigProvider> _logger;
        private ConfigurationSnapshot? _snapshot;
        private CancellationTokenSource? _changeToken;
        private bool _disposed;
        private IDisposable? _subscription;
        private readonly GatewayProxySource _gatewayProxySource;

        public GatewayProxyConfigProvider(
            ILogger<GatewayProxyConfigProvider> logger,
            GatewayProxySource gatewayProxySource)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            //_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _gatewayProxySource = gatewayProxySource;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _subscription?.Dispose();
                _changeToken?.Dispose();
                _disposed = true;
            }
        }

        private IChangeToken? GetOnChangeToken()
        {
            return _gatewayProxySource.GetChangeToken();
        }
        private ConfigurationSnapshot ReadConfiguration()
        {
            return _gatewayProxySource.ReadConfiguration();
        }


        public IProxyConfig GetConfig()
        {
            // First time load
            if (_snapshot is null)
            {
                _subscription = ChangeToken.OnChange(GetOnChangeToken, UpdateSnapshot);
                UpdateSnapshot();
            }

            return _snapshot;
        }

        [MemberNotNull(nameof(_snapshot))]
        private void UpdateSnapshot()
        {
            // Prevent overlapping updates, especially on startup.
            lock (_lockObject)
            {
                Log.LoadData(_logger);
                ConfigurationSnapshot newSnapshot;
                try
                {
                    newSnapshot = ReadConfiguration();
                }
                catch (Exception ex)
                {
                    Log.ConfigurationDataConversionFailed(_logger, ex);

                    // Re-throw on the first time load to prevent app from starting.
                    if (_snapshot is null)
                    {
                        throw;
                    }

                    return;
                }

                var oldToken = _changeToken;
                _changeToken = new CancellationTokenSource();

                // update snapshot
                newSnapshot.ChangeToken = new CancellationChangeToken(_changeToken.Token);
                _snapshot = newSnapshot;

                try
                {
                    // Call this to trigger get new config from GetConfig
                    oldToken?.Cancel(throwOnFirstException: false);
                }
                catch (Exception ex)
                {
                    Log.ErrorSignalingChange(_logger, ex);
                }
            }
        }

        private static class Log
        {
            public static void ErrorSignalingChange(ILogger logger, Exception exception)
            {
            }
            public static void LoadData(ILogger logger)
            {
            }
            public static void ConfigurationDataConversionFailed(ILogger logger, Exception exception)
            {
              
            }
        }
    }

    internal class ConfigurationSnapshot : IProxyConfig
    {
        public List<RouteConfig> Routes { get; internal set; } = new List<RouteConfig>();

        public List<ClusterConfig> Clusters { get; internal set; } = new List<ClusterConfig>();


        // impelement interface
        IReadOnlyList<RouteConfig> IProxyConfig.Routes => Routes;

        IReadOnlyList<ClusterConfig> IProxyConfig.Clusters => Clusters;
        public IChangeToken ChangeToken { get; internal set; } = default!;

    }

}
