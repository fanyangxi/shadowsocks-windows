using Microsoft.Win32;
using Shadowsocks.Controller;
using Shadowsocks.Controller.Strategy;
using Shadowsocks.Encryption;
using Shadowsocks.Model;
using Shadowsocks.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;
//using System.Windows.Forms;

namespace Shadowsocks.View
{
    /// <summary>
    /// - Manually invoked by user/right-click
    /// - Timer check
    /// </summary>
    public class BackendConfigUpdateController
    {
        private ShadowsocksController _ssController;
        private Configuration _currentConfiguration;

        public BackendConfigUpdateController(ShadowsocksController controller)
        {
            this._ssController = controller;
            this._ssController.ConfigChanged += ssController_ConfigChanged;
        }

        public void GetFree()
        {
        }

        public void SaveLoadFreeSsServerInfos(List<SsServerInfo> serverInfos, int localPort)
        {
            try
            {
                _ssController.SaveServers(serverInfos, localPort);
            }
            catch (Exception ex)
            {
                //Logging.Debug(String.Format("server: {0} latency:{1} score: {2}", status.server.DisplayName(), status.latency, status.score));
                Logging.LogUsefulException(ex);
            }
        }

        private void ssController_ConfigChanged(object sender, EventArgs e)
        {
            _currentConfiguration = _ssController.GetConfigurationCopy();
        }
    }
}
