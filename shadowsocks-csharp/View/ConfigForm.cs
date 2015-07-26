using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using Shadowsocks.Controller;
using Shadowsocks.Model;
using Shadowsocks.Properties;

namespace Shadowsocks.View
{
    public partial class ConfigForm : Form
    {
        // this is a copy of configuration that we are working on
        private Configuration _modifiedConfiguration;
        private int _oldSelectedIndex = -1;
        private ShadowsocksController _ssController;

        public ConfigForm(ShadowsocksController controller)
        {
            InitializeComponent();

            this.Font = System.Drawing.SystemFonts.MessageBoxFont;
            this.Icon = Icon.FromHandle(Resources.ssw128.GetHicon());

            this._ssController = controller;
            controller.ConfigChanged += ssController_ConfigChanged;

            UpdateTexts();
            LoadCurrentConfiguration();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {

        }

        private void ConfigForm_Shown(object sender, EventArgs e)
        {
            tbxServerIP.Focus();
        }

        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ssController.ConfigChanged -= ssController_ConfigChanged;
        }

        private void lbxServersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_oldSelectedIndex == lbxServersList.SelectedIndex)
            {
                // we are moving back to oldSelectedIndex or doing a force move
                return;
            }
            if (!SaveOldSelectedServer())
            {
                // why this won't cause stack overflow?
                lbxServersList.SelectedIndex = _oldSelectedIndex;
                return;
            }
            LoadSelectedServer();
            _oldSelectedIndex = lbxServersList.SelectedIndex;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            SsServerInfo server = Configuration.GetDefaultServer();
            _modifiedConfiguration.ServerInfos.Add(server);
            LoadConfiguration(_modifiedConfiguration);
            lbxServersList.SelectedIndex = _modifiedConfiguration.ServerInfos.Count - 1;
            _oldSelectedIndex = lbxServersList.SelectedIndex;
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            _oldSelectedIndex = lbxServersList.SelectedIndex;
            if (_oldSelectedIndex >= 0 && _oldSelectedIndex < _modifiedConfiguration.ServerInfos.Count)
            {
                _modifiedConfiguration.ServerInfos.RemoveAt(_oldSelectedIndex);
            }
            if (_oldSelectedIndex >= _modifiedConfiguration.ServerInfos.Count)
            {
                // can be -1
                _oldSelectedIndex = _modifiedConfiguration.ServerInfos.Count - 1;
            }

            lbxServersList.SelectedIndex = _oldSelectedIndex;
            LoadConfiguration(_modifiedConfiguration);
            lbxServersList.SelectedIndex = _oldSelectedIndex;

            LoadSelectedServer();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!SaveOldSelectedServer())
            {
                return;
            }
            if (_modifiedConfiguration.ServerInfos.Count == 0)
            {
                MessageBox.Show(I18N.GetString("Please add at least one server"));
                return;
            }
            _ssController.SaveServers(_modifiedConfiguration.ServerInfos, _modifiedConfiguration.localPort);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ssController_ConfigChanged(object sender, EventArgs e)
        {
            LoadCurrentConfiguration();
        }

        private void UpdateTexts()
        {
            btnAddItem.Text = I18N.GetString("&Add");
            btnDeleteItem.Text = I18N.GetString("&Delete");
            IPLabel.Text = I18N.GetString("Server IP");
            ServerPortLabel.Text = I18N.GetString("Server Port");
            PasswordLabel.Text = I18N.GetString("Password");
            EncryptionLabel.Text = I18N.GetString("Encryption");
            ProxyPortLabel.Text = I18N.GetString("Proxy Port");
            RemarksLabel.Text = I18N.GetString("Remarks");
            gbxServerConfigs.Text = I18N.GetString("Server");
            btnOK.Text = I18N.GetString("OK");
            btnCancel.Text = I18N.GetString("Cancel");
            this.Text = I18N.GetString("Edit Servers");
        }

        private void ShowWindow()
        {
            this.Opacity = 1;
            this.Show();
            tbxServerIP.Focus();
        }

        private bool SaveOldSelectedServer()
        {
            try
            {
                if (_oldSelectedIndex == -1 || _oldSelectedIndex >= _modifiedConfiguration.ServerInfos.Count)
                {
                    return true;
                }
                SsServerInfo server = new SsServerInfo
                {
                    server = tbxServerIP.Text,
                    server_port = int.Parse(tbxServerPort.Text),
                    password = tbxPassword.Text,
                    method = cbxEncryptions.Text,
                    remarks = btnRemarks.Text
                };
                int localPort = int.Parse(btnProxyPort.Text);
                Configuration.CheckServer(server);
                Utils.IsPortValid(localPort);
                _modifiedConfiguration.ServerInfos[_oldSelectedIndex] = server;
                _modifiedConfiguration.localPort = localPort;

                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show(I18N.GetString("Illegal port number format"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void LoadSelectedServer()
        {
            if (lbxServersList.SelectedIndex >= 0 && lbxServersList.SelectedIndex < _modifiedConfiguration.ServerInfos.Count)
            {
                var server = _modifiedConfiguration.ServerInfos[lbxServersList.SelectedIndex];

                tbxServerIP.Text = server.server;
                tbxServerPort.Text = server.server_port.ToString();
                tbxPassword.Text = server.password;
                btnProxyPort.Text = _modifiedConfiguration.localPort.ToString();
                cbxEncryptions.Text = server.method ?? "aes-256-cfb";
                btnRemarks.Text = server.remarks;
                gbxServerConfigs.Visible = true;
                //IPTextBox.Focus();
            }
            else
            {
                //ServerGroupBox.Visible = false;
            }
        }

        private void LoadConfiguration(Configuration configuration)
        {
            lbxServersList.Items.Clear();
            foreach (SsServerInfo server in _modifiedConfiguration.ServerInfos)
            {
                lbxServersList.Items.Add(server.FriendlyName());
            }
        }

        private void LoadCurrentConfiguration()
        {
            _modifiedConfiguration = _ssController.GetConfigurationCopy();
            LoadConfiguration(_modifiedConfiguration);

            _oldSelectedIndex = _modifiedConfiguration.index;
            if (_oldSelectedIndex < 0)
            {
                _oldSelectedIndex = 0;
            }

            lbxServersList.SelectedIndex = _oldSelectedIndex;
            LoadSelectedServer();
        }
    }
}
