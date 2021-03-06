﻿using Shadowsocks.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shadowsocks.Controller.Strategy
{
    class HighAvailabilityStrategy : IStrategy
    {
        protected SsServerInfo _currentServer;
        protected Dictionary<SsServerInfo, ServerStatus> _serverStatus;
        ShadowsocksController _controller;
        Random _random;

        public class ServerStatus
        {
            // time interval between SYN and SYN+ACK
            public TimeSpan latency;
            public DateTime lastTimeDetectLatency;

            // last time anything received
            public DateTime lastRead;

            // last time anything sent
            public DateTime lastWrite;

            // connection refused or closed before anything received
            public DateTime lastFailure;

            public SsServerInfo server;

            public double score;
        }

        public HighAvailabilityStrategy(ShadowsocksController controller)
        {
            _controller = controller;
            _random = new Random();
            _serverStatus = new Dictionary<SsServerInfo, ServerStatus>();
        }

        public string Name
        {
            get { return I18N.GetString("High Availability"); }
        }

        public string ID
        {
            get { return "com.shadowsocks.strategy.ha"; }
        }

        public void ReloadServers()
        {
            // make a copy to avoid locking
            var newServerStatus = new Dictionary<SsServerInfo, ServerStatus>(_serverStatus);

            foreach (var server in _controller.GetCurrentConfiguration().ServerInfos)
            {
                if (!newServerStatus.ContainsKey(server))
                {
                    var status = new ServerStatus();
                    status.server = server;
                    status.lastFailure = DateTime.MinValue;
                    status.lastRead = DateTime.Now;
                    status.lastWrite = DateTime.Now;
                    status.latency = new TimeSpan(0, 0, 0, 0, 10);
                    status.lastTimeDetectLatency = DateTime.Now;
                    newServerStatus[server] = status;
                }
            }
            _serverStatus = newServerStatus;

            ChooseNewServer();
        }

        public SsServerInfo GetAServer(IStrategyCallerType type, System.Net.IPEndPoint localIPEndPoint)
        {
            if (type == IStrategyCallerType.TCP)
            {
                ChooseNewServer();
            }
            return _currentServer;
        }

        /**
         * once failed, try after 5 min
         * and (last write - last read) < 5s
         * and (now - last read) <  5s  // means not stuck
         * and latency < 200ms, try after 30s
         */
        public void ChooseNewServer()
        {
            SsServerInfo oldServer = _currentServer;
            List<ServerStatus> servers = new List<ServerStatus>(_serverStatus.Values);
            DateTime now = DateTime.Now;
            foreach (var status in servers)
            {
                // all of failure, latency, (lastread - lastwrite) normalized to 1000, then
                // 100 * failure - 2 * latency - 0.5 * (lastread - lastwrite)
                status.score =
                    100 * 1000 * Math.Min(5 * 60, (now - status.lastFailure).TotalSeconds)
                    - 2 * 5 * (Math.Min(2000, status.latency.TotalMilliseconds) / (1 + (now - status.lastTimeDetectLatency).TotalSeconds / 30 / 10) +
                    -0.5 * 200 * Math.Min(5, (status.lastRead - status.lastWrite).TotalSeconds));
                Logging.Debug(String.Format("server: {0} latency:{1} score: {2}", status.server.DisplayName(), status.latency, status.score));
            }
            ServerStatus max = null;
            foreach (var status in servers)
            {
                if (max == null)
                {
                    max = status;
                }
                else
                {
                    if (status.score >= max.score)
                    {
                        max = status;
                    }
                }
            }
            if (max != null)
            {
                _currentServer = max.server;
                if (_currentServer != oldServer)
                {
                    Console.WriteLine("HA switching to server: {0}", _currentServer.DisplayName());
                }

                Logging.Debug(String.Format("choosing server: {0}", _currentServer.DisplayName()));
            }
        }

        public void UpdateLatency(Model.SsServerInfo server, TimeSpan latency)
        {
            Logging.Debug(String.Format("latency: {0} {1}", server.DisplayName(), latency));

            ServerStatus status;
            if (_serverStatus.TryGetValue(server, out status))
            {
                status.latency = latency;
                status.lastTimeDetectLatency = DateTime.Now;
            }
        }

        public void UpdateLastRead(Model.SsServerInfo server)
        {
            Logging.Debug(String.Format("last read: {0}", server.DisplayName()));

            ServerStatus status;
            if (_serverStatus.TryGetValue(server, out status))
            {
                status.lastRead = DateTime.Now;
            }
        }

        public void UpdateLastWrite(Model.SsServerInfo server)
        {
            Logging.Debug(String.Format("last write: {0}", server.DisplayName()));

            ServerStatus status;
            if (_serverStatus.TryGetValue(server, out status))
            {
                status.lastWrite = DateTime.Now;
            }
        }

        public void SetFailure(Model.SsServerInfo server)
        {
            Logging.Debug(String.Format("failure: {0}", server.DisplayName()));

            ServerStatus status;
            if (_serverStatus.TryGetValue(server, out status))
            {
                status.lastFailure = DateTime.Now;
                RemoveExpiredSsServerInfoFromConfig(server);
            }
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
