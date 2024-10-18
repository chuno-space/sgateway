using Microsoft.Extensions.Primitives;

namespace CHUNO.SGateway.Infrastructures
{
    internal class GatewayProxySource
    {
        private readonly IConfiguration _configuration;
        private readonly IChangeToken _changeToken;
        public GatewayProxySource(IConfiguration configuration) {
            _configuration = configuration;
            _changeToken = _configuration.GetReloadToken();
        }
        public ConfigurationSnapshot ReadConfiguration()
        {
           return ProxyConfigUtils.ReadConfiguration(_configuration);
        }

        public IChangeToken ChangeToken => _configuration.GetReloadToken();
    }
}
