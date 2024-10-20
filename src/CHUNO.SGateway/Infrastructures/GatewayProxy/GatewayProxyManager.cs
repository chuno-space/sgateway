﻿using CHUNO.SGateway.Data;
using CHUNO.SGateway.Infrastructures.GatewayProxy.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Timers;
using Yarp.ReverseProxy.Configuration;

namespace CHUNO.SGateway.Infrastructures.GatewayProxy
{
    public class GatewayProxyManager : IGatewayProxyUpdater, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly System.Timers.Timer _timer;

        private CancellationTokenSource _cts = new CancellationTokenSource();
        private IChangeToken? _changeToken;
        private IDisposable? _subscription;
        private readonly IDbContextFactory<GatewayDBContext> _contextFactory;
        public GatewayProxyManager(IDbContextFactory<GatewayDBContext> contextFactory, IConfiguration configuration)
        {
            _contextFactory = contextFactory;
            _configuration = configuration;

            //_timer = new System.Timers.Timer(30000);  // 5 seconds interval
            //_timer.Elapsed += OnTimerElapsed;
            //_timer.Start();

            //System.Threading.Timer timer1 = new System.Threading.Timer((Object obj) =>
            //{
            //    RaiseChanged();
            //},null,  10*1000, 10 * 1000);
          

            // Initial registration of change monitoring
            RegisterForChange();
        }

        public ConfigurationSnapshot ReadConfiguration()
        {
            //return ProxyConfigUtils.ReadConfiguration(_configuration);

            return ReadDB();
        }
        public IChangeToken? GetChangeToken()
        {
            //return _configuration.GetReloadToken(); 
            return _changeToken!;
        }

        public void ConfigUpdate()
        {
            RaiseChanged();
        }

        private void RegisterForChange()
        {
            // https://github.com/dotnet/runtime/blob/24dd2bc956efb7485c667be6433aef107af793a9/src/libraries/Microsoft.Extensions.Configuration/src/ConfigurationManager.cs#L100

            _cts = new CancellationTokenSource();
            _changeToken = new CancellationChangeToken(_cts.Token);

            IChangeToken configurationChangeToken;
            _subscription = ChangeToken.OnChange(() =>
            {
                var newToken = _configuration.GetReloadToken();
                configurationChangeToken = newToken;
                return newToken;
            }, () =>
            {
                RaiseChanged();
            });
        }

        private void RaiseChanged()
        {
            var cts = new CancellationTokenSource();
            var newChangeTken = new CancellationChangeToken(cts.Token);
            var oldChangeToken = _changeToken;
            Interlocked.Exchange(ref _changeToken, newChangeTken);
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
            _cts = cts;
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            // TODO: Read Database to monitor Config changed?
            //https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/
            Console.WriteLine("Timer ticked, simulating change.");
            var changed = true;
            if (changed)
            {
                //RaiseChanged();
            }
        }

        private ConfigurationSnapshot ReadDB()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var guiid = Guid.NewGuid().ToString();
                var routeConfigs = context.GatewayRouteConfigs.ToList();

                var snapshot = routeConfigs.ToConfiugrationSnapshot();
                return snapshot;
            }
        }
        public void Dispose()
        {
            _timer.Stop();
            _timer.Dispose();
            _cts?.Dispose();
            _subscription?.Dispose();
        }


    }
}
