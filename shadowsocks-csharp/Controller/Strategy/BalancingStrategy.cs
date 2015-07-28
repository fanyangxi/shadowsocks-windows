using Shadowsocks.Controller;
using Shadowsocks.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Shadowsocks.Controller.Strategy
{
    class BalancingStrategy : IStrategy
    {
        ShadowsocksController _controller;
        Random _random;

        public BalancingStrategy(ShadowsocksController controller)
        {
            _controller = controller;
            _random = new Random();
        }

        public string Name
        {
            get { return I18N.GetString("Load Balance"); }
        }

        public string ID
        {
            get { return "com.shadowsocks.strategy.balancing"; }
        }

        public void ReloadServers()
        {
            // do nothing
        }

        public SsServerInfo GetAServer(IStrategyCallerType type, IPEndPoint localIPEndPoint)
        {
            var configs = _controller.GetCurrentConfiguration().ServerInfos;
            int index;
            if (type == IStrategyCallerType.TCP)
            {
                index = _random.Next();
            }
            else
            {
                index = localIPEndPoint.GetHashCode();
            }
            return configs[index % configs.Count];
        }

        public void UpdateLatency(Model.SsServerInfo server, TimeSpan latency)
        {
            // do nothing
        }

        public void UpdateLastRead(Model.SsServerInfo server)
        {
            // do nothing
        }

        public void UpdateLastWrite(Model.SsServerInfo server)
        {
            // do nothing
        }

        public void SetFailure(Model.SsServerInfo server)
        {
            // do nothing
            RemoveExpiredSsServerInfoFromConfig(server);
        }

        private void RemoveExpiredSsServerInfoFromConfig(SsServerInfo serverInfo)
        {
            try
            {
                var currentConfiguration = _controller.GetCurrentConfiguration();
                currentConfiguration.ServerInfos.Remove(serverInfo);
                _controller.SaveServers(currentConfiguration.ServerInfos, currentConfiguration.localPort);

                var aaa = string.Format("Expired ss-server ({0}) has been removed, {1} left", serverInfo.DisplayName(), currentConfiguration.ServerInfos.Count);
                Shadowsocks.View.MenuViewController.ShowBalloonTip("Info", aaa, System.Windows.Forms.ToolTipIcon.Warning, 1000);
            }
            catch (Exception ex)
            {
                Logging.LogUsefulException(ex);
            }
        }
    }
}
