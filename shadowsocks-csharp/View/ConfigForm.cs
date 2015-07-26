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
        private Configuration _currentConfiguration;
        private SsServerInfo _selectedSsServerInfo;
        private ShadowsocksController _ssController;
        private int _oldSelectedItemIndex = -1;

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
            RenderConfigurationInfo(_currentConfiguration);
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
            var itemDisplayName = Convert.ToString(lbxServersList.SelectedItem);
            _selectedSsServerInfo = _currentConfiguration.ServerInfos.Find(o => o.DisplayName() == itemDisplayName);

            if (!CheckCurrentSelectedSsServerInfo())
            {
                // why this won't cause stack overflow?
                lbxServersList.SelectedIndex = _oldSelectedItemIndex;
                return;
            }

            RenderSelectedSsServerInfo();

            // Record the last selected item index
            _oldSelectedItemIndex = lbxServersList.SelectedIndex;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (!CheckCurrentSelectedSsServerInfo())
            {
                return;
            }

            _currentConfiguration.ServerInfos.Add(Configuration.GetDefaultSsServerInfo());

            RenderConfigurationInfo(_currentConfiguration);

            // Select the last new added item
            lbxServersList.SelectedIndex = _currentConfiguration.ServerInfos.Count - 1;
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (_selectedSsServerInfo != null)
            {
                _currentConfiguration.ServerInfos.Remove(_selectedSsServerInfo);
            }

            // Make sure there are at least 1 ss-server-info
            if (_currentConfiguration.ServerInfos.Count == 0)
            {
                _currentConfiguration.ServerInfos.Add(Configuration.GetDefaultSsServerInfo());
            }

            RenderConfigurationInfo(_currentConfiguration);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!CheckCurrentSelectedSsServerInfo())
            {
                return;
            }

            if (!CheckCurrentGeneralConfigInfo())
            {
                return;
            }

            if (_currentConfiguration.ServerInfos.Count == 0)
            {
                MessageBox.Show(I18N.GetString("Please add at least one server"));
                return;
            }

            _ssController.SaveServers(_currentConfiguration.ServerInfos, _currentConfiguration.localPort);
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

        private bool CheckCurrentSelectedSsServerInfo()
        {
            try
            {
                if (_selectedSsServerInfo == null)
                {
                    return true;
                }

                _selectedSsServerInfo.server = tbxServerIP.Text;
                _selectedSsServerInfo.server_port = int.Parse(tbxServerPort.Text);
                _selectedSsServerInfo.password = tbxPassword.Text;
                _selectedSsServerInfo.method = cbxEncryptions.Text;
                _selectedSsServerInfo.remarks = btnRemarks.Text;
                Utils.IsSsServerInfoValid(_selectedSsServerInfo);

                return true;
            }
            catch (Exception)
            {
                MessageBox.Show(I18N.GetString("Illegal port number format"));
            }

            return false;
        }

        private bool CheckCurrentGeneralConfigInfo()
        {
            try
            {
                var localPort = int.Parse(btnProxyPort.Text);
                Utils.IsPortValid(localPort);
                _currentConfiguration.localPort = localPort;

                return true;
            }
            catch (Exception)
            {
                MessageBox.Show(I18N.GetString("Illegal port number format"));
            }

            return false;
        }

        private void LoadCurrentConfiguration()
        {
            _currentConfiguration = _ssController.GetConfigurationCopy();
        }

        private void RenderConfigurationInfo(Configuration configuration)
        {
            // Record the old selected-index
            var oldSelectedIndex = lbxServersList.SelectedIndex;
            if (oldSelectedIndex == -1)
            {
                oldSelectedIndex = configuration.selectedSsServerInfoIndex;
            }

            // If the selected item has been removed, then select the last one instead
            if (oldSelectedIndex >= configuration.ServerInfos.Count)
            {
                oldSelectedIndex = configuration.ServerInfos.Count - 1;
            }

            lbxServersList.Items.Clear();
            foreach (SsServerInfo server in configuration.ServerInfos)
            {
                lbxServersList.Items.Add(server.DisplayName());
            }

            // Re-set back to the old selected-index
            lbxServersList.SelectedIndex = oldSelectedIndex;

            RenderSelectedSsServerInfo();
        }

        private void RenderSelectedSsServerInfo()
        {
            if (lbxServersList.SelectedIndex < 0 ||
                lbxServersList.SelectedIndex >= _currentConfiguration.ServerInfos.Count)
            {
                return;
            }

            var server = _currentConfiguration.ServerInfos[lbxServersList.SelectedIndex];
            tbxServerIP.Text = server.server;
            tbxServerPort.Text = server.server_port.ToString();
            tbxPassword.Text = server.password;
            btnProxyPort.Text = _currentConfiguration.localPort.ToString();
            cbxEncryptions.Text = server.method ?? "aes-256-cfb";
            btnRemarks.Text = server.remarks;
            gbxServerConfigs.Visible = true;
        }
    }
}
