using Shadowsocks.Controller.Strategy;
using Shadowsocks.Encryption;
using Shadowsocks.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Shadowsocks.Controller
{
    public class SsServerInfoChecker
    {
        private ShadowsocksController _ssController;
        private Configuration _currentConfiguration;
        private System.Threading.Timer timer;
        private Socket remoteSocket;

        public SsServerInfoChecker(ShadowsocksController controller)
        {
            this._ssController = controller;
            this._ssController.ConfigChanged += ssController_ConfigChanged;
        }

        public void ReStartCheckTimer()
        {
            timer = new System.Threading.Timer(new System.Threading.TimerCallback(connectTimer_Elapsed), null, 0, 30 * 1000);
        }

        private void connectTimer_Elapsed(object sender)
        {
            Check(_currentConfiguration);
        }

        private void ssController_ConfigChanged(object sender, EventArgs e)
        {
            _currentConfiguration = _ssController.GetConfigurationCopy();
        }

        private void Check(Configuration config)
        {
            if (config == null)
            {
                return;
            }

            for (int i = 0; i < config.ServerInfos.Count; i++)
            {
                var serverInfo = config.ServerInfos[i];
                try
                {
                    // TODO async resolving
                    IPAddress ipAddress;
                    bool parsed = IPAddress.TryParse(serverInfo.server, out ipAddress);
                    if (!parsed)
                    {
                        IPHostEntry ipHostInfo = Dns.GetHostEntry(serverInfo.server);
                        ipAddress = ipHostInfo.AddressList[0];
                    }
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, serverInfo.server_port);

                    remoteSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    remoteSocket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);

                    // Connect to the remote endpoint.
                    //remoteSocket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), serverInfo);
                    remoteSocket.Connect(remoteEP);
                }
                catch (Exception ex)
                {
                    RemoveExpiredSsServerInfoFromConfig(serverInfo);
                    i--;
                    Logging.LogUsefulException(ex);
                }
            }
        }

        private void RemoveExpiredSsServerInfoFromConfig(SsServerInfo serverInfo)
        {
            try
            {
                _currentConfiguration.ServerInfos.Remove(serverInfo);
                _ssController.SaveServers(_currentConfiguration.ServerInfos, _currentConfiguration.localPort);

                var aaa = string.Format("Expired ss-server has been removed, {0} left", _currentConfiguration.ServerInfos.Count);
                Shadowsocks.View.MenuViewController.ShowBalloonTip("Info", aaa, System.Windows.Forms.ToolTipIcon.Warning, 3000);
            }
            catch (Exception ex)
            {
                Logging.LogUsefulException(ex);
            }
        }
    }
}

//private void ConnectCallback(IAsyncResult ar)
//{
//    SsServerInfo serverInfo = null;
//    try
//    {
//        serverInfo = (SsServerInfo)ar.AsyncState;
//        // Complete the connection.
//        remoteSocket.EndConnect(ar);
//    }
//    catch (Exception e)
//    {
//        //throw;
//        RemoveExpiredSsServerInfoFromConfig(serverInfo);
//    }
//}
