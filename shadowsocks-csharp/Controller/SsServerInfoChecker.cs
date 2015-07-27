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
        private Dictionary<string, Socket> _remoteSocketsDict;

        public IEncryptor encryptor;

        private const int RecvSize = 16384;
        public const int BufferSize = RecvSize + 32;
        private byte[] remoteRecvBuffer = new byte[RecvSize];
        private int totalRead = 0;
        private byte[] connetionRecvBuffer = new byte[RecvSize];
        private byte[] connetionSendBuffer = new byte[BufferSize];
        private object objLock = new object();

        public SsServerInfoChecker(ShadowsocksController controller)
        {
            this._ssController = controller;
            this._ssController.ConfigChanged += ssController_ConfigChanged;
            this._remoteSocketsDict = new Dictionary<string, Socket>();
        }

        public void ReStartCheckTimer()
        {
            timer = new System.Threading.Timer(new System.Threading.TimerCallback(connectTimer_Elapsed), null, 0, 40 * 1000);
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
                Socket reSocket;
                var serverInfo = config.ServerInfos[i];
                try
                {
                    // TODO async resolving
                    IPEndPoint remoteEP = GenerateIPEndPointFromSsServerInfo(serverInfo);

                    reSocket = new Socket(remoteEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    reSocket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);

                    // Connect to the remote endpoint.
                    //remoteSocket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), serverInfo);
                    reSocket.SendTimeout = 3000;
                    reSocket.ReceiveTimeout = 3000;
                    reSocket.Connect(remoteEP);

                    // Add the socket to dict after the connection established
                    _remoteSocketsDict.Add(serverInfo.ShortName(), reSocket);

                    //// Start pipe
                    //// Try to send test-password-bytes
                    //this.encryptor = EncryptorFactory.GetEncryptor(serverInfo.method, serverInfo.password);

                    //int bytesToSend;
                    //var sampleTestData = Encoding.UTF8.GetBytes("this is the test data");
                    //connetionRecvBuffer = sampleTestData;
                    //var bytesRead = sampleTestData.Length;
                    //encryptor.Encrypt(connetionRecvBuffer, bytesRead, connetionSendBuffer, out bytesToSend);
                    //reSocket.BeginSend(connetionSendBuffer, 0, bytesToSend, 0, new AsyncCallback(PipeRemoteSendCallback), serverInfo);

                    //// Checking sending data with password
                    //reSocket.BeginReceive(remoteRecvBuffer, 0, RecvSize, 0, new AsyncCallback(PipeRemoteReceiveCallback), serverInfo);
                }
                catch (Exception ex)
                {
                    Logging.LogUsefulException(ex);

                    //Failed to connect / send, remove the ss-server-info
                    this.RemoveExpiredSsServerInfoFromConfig(serverInfo);
                    i--;
                }
                finally
                {
                    this.Close(serverInfo);
                }
            }
        }

        private void PipeRemoteSendCallback(IAsyncResult ar)
        {
            lock (objLock)
            {
                var serverInfo = (SsServerInfo)ar.AsyncState;
                var theSocket = FindSocketObj(serverInfo);
                try
                {
                    theSocket.EndSend(ar);
                }
                catch (Exception e)
                {
                    Logging.LogUsefulException(e);
                    this.Close(serverInfo);
                }
            }
        }

        private void PipeRemoteReceiveCallback(IAsyncResult ar)
        {
            lock (objLock)
            {
                var serverInfo = (SsServerInfo)ar.AsyncState;
                var theSocket = FindSocketObj(serverInfo);
                try
                {
                    int bytesRead = theSocket.EndReceive(ar);
                    totalRead += bytesRead;

                    if (bytesRead > 0)
                    {
                        //~\Service\TCPRelay.cs, PipeRemoteReceiveCallback (481)
                    }
                    else
                    {
                        ////Console.WriteLine("bytesRead: " + bytesRead.ToString());
                        //connection.Shutdown(SocketShutdown.Send);
                        //connectionShutdown = true;
                        //CheckClose();

                        if (totalRead == 0)
                        {
                            // closed before anything received, reports as failure
                            // disable this feature
                            // controller.GetCurrentStrategy().SetFailure(this.server);

                            // Failed to authorize with the password, remove the ss-server-info
                            //this.RemoveExpiredSsServerInfoFromConfig(serverInfo);
                        }
                    }
                }
                catch (Exception e)
                {
                    Logging.LogUsefulException(e);
                }
                finally
                {
                    // Testing finished, close the socket
                    this.Close(serverInfo);
                }
            }
        }

        private void Close(SsServerInfo serverInfo)
        {
            lock (objLock)
            {
                var theSocket = FindSocketObj(serverInfo);
                if (theSocket != null)
                {
                    try
                    {
                        theSocket.Shutdown(SocketShutdown.Both);
                        theSocket.Close();

                        _remoteSocketsDict.Remove(serverInfo.ShortName());
                    }
                    catch (Exception e)
                    {
                        Logging.LogUsefulException(e);
                    }
                }
            }
        }

        private Socket FindSocketObj(SsServerInfo serverInfo)
        {
            if (this._remoteSocketsDict.ContainsKey(serverInfo.ShortName()))
            {
                var remo = this._remoteSocketsDict[serverInfo.ShortName()];
                return remo;
            }
            else
            {
                return null;
            }
        }

        private void RemoveExpiredSsServerInfoFromConfig(SsServerInfo serverInfo)
        {
            try
            {
                _currentConfiguration.ServerInfos.Remove(serverInfo);
                _ssController.SaveServers(_currentConfiguration.ServerInfos, _currentConfiguration.localPort);

                var aaa = string.Format("Expired ss-servers has been removed, {0} left", _currentConfiguration.ServerInfos.Count);
                Shadowsocks.View.MenuViewController.ShowBalloonTip("Info", aaa, System.Windows.Forms.ToolTipIcon.Warning, 1000);
            }
            catch (Exception ex)
            {
                Logging.LogUsefulException(ex);
            }
        }

        private IPEndPoint GenerateIPEndPointFromSsServerInfo(SsServerInfo serverInfo)
        {
            IPAddress ipAddress;
            bool parsed = IPAddress.TryParse(serverInfo.server, out ipAddress);
            if (!parsed)
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(serverInfo.server);
                ipAddress = ipHostInfo.AddressList[0];
            }

            IPEndPoint remoteEP = new IPEndPoint(ipAddress, serverInfo.server_port);
            return remoteEP;
        }

        private bool IsSocketNotDisposed(Socket socket)
        {
            try
            {
                var aaa = socket.RemoteEndPoint;
                return true;
            }
            catch
            {
                return false;
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
