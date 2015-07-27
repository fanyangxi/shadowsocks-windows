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
        private SsServerInfo _lastSelectedSsServerInfo;
        private ShadowsocksController _ssController;
        private int _oldSelectedItemIndex = -1;
        private string _originalSerializedConfigData = string.Empty;

        public ConfigForm(ShadowsocksController controller)
        {
            InitializeComponent();

            this.Font = System.Drawing.SystemFonts.MessageBoxFont;
            this.Icon = Icon.FromHandle(Resources.ssw128.GetHicon());
            this._lastSelectedSsServerInfo = null;
            this._ssController = controller;
            controller.ConfigChanged += ssController_ConfigChanged;

            UpdateTexts();
            LoadCurrentConfiguration();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            CacheOriginalConfigData();
            RenderConfigurationInfo(_currentConfiguration);
        }

        private void ConfigForm_Shown(object sender, EventArgs e)
        {
            tbxServerIP.Focus();
        }

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.IsCurrentConfigDataChanged())
            {
                var dialogResult = MessageBox.Show("Do you want to save the changed data?", "Warning", MessageBoxButtons.OKCancel);
                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    _ssController.SaveServers(_currentConfiguration.ServerInfos, _currentConfiguration.localPort);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ssController.ConfigChanged -= ssController_ConfigChanged;
        }

        private void lbxServersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var itemDisplayName = Convert.ToString(lbxServersList.SelectedItem);
            var lastItemDisplayName = _lastSelectedSsServerInfo == null ? string.Empty : _lastSelectedSsServerInfo.DisplayName();

            var newSelectedIndex = lbxServersList.SelectedIndex;
            var lastSelectedIndex = _currentConfiguration.ServerInfos.FindIndex(o => o.DisplayName() == lastItemDisplayName);
            if (newSelectedIndex != lastSelectedIndex)
            {
                _lastSelectedSsServerInfo = _currentConfiguration.ServerInfos.Find(o => o.DisplayName() == itemDisplayName);
                RenderSelectedSsServerInfo();
            }

            // Record the last selected item index
            _oldSelectedItemIndex = lbxServersList.SelectedIndex;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            //if (!CheckCurrentConfigObject())
            //{
            //    return;
            //}

            _currentConfiguration.ServerInfos.Add(Configuration.GetDefaultSsServerInfo());

            RenderConfigurationInfo(_currentConfiguration);

            // Select the last new added item
            lbxServersList.SelectedIndex = _currentConfiguration.ServerInfos.Count - 1;

            this.btnOK.Enabled = this.IsCurrentConfigDataChanged();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            //var aaa = new SsServerInfoChecker();
            //aaa.Check(_lastSelectedSsServerInfo);
            //return;

            if (_lastSelectedSsServerInfo != null)
            {
                _currentConfiguration.ServerInfos.Remove(_lastSelectedSsServerInfo);
            }

            // Make sure there are at least 1 ss-server-info
            if (_currentConfiguration.ServerInfos.Count == 0)
            {
                _currentConfiguration.ServerInfos.Add(Configuration.GetDefaultSsServerInfo());
                btnDeleteItem.Enabled = false;
            }

            RenderConfigurationInfo(_currentConfiguration);

            this.btnOK.Enabled = this.IsCurrentConfigDataChanged();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //if (!CheckCurrentConfigObject())
            //{
            //    return;
            //}
            try
            {
                ShowStatusMessage(string.Empty);
                _ssController.SaveServers(_currentConfiguration.ServerInfos, _currentConfiguration.localPort);
                CacheOriginalConfigData();
            }
            catch (Exception ex)
            {
                ShowStatusMessage(ex.Message);
            }

            this.btnOK.Enabled = this.IsCurrentConfigDataChanged();
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

        private bool UpdateCurrentConfigObject(string theControlName)
        {
            if (_lastSelectedSsServerInfo == null)
            {
                return true;
            }

            ShowStatusMessage(string.Empty);

            // ServerIP
            if (theControlName.Contains("IP"))
            {
                _lastSelectedSsServerInfo.server = tbxServerIP.Text;
                this.TryAction((obj) => { Utils.IsServerIPNotEmpty(tbxServerIP.Text); });
            }

            if (theControlName.Contains("Password"))
            {
                _lastSelectedSsServerInfo.password = tbxPassword.Text;
                this.TryAction((obj) => { Utils.IsPasswordNotEmpty(tbxPassword.Text); });
            }

            if (theControlName.Contains("Encryption"))
            {
                _lastSelectedSsServerInfo.method = cbxEncryptions.Text;
                this.TryAction((obj) => { Utils.IsEncryptionNotEmpty(cbxEncryptions.Text); });
            }

            if (theControlName.Contains("ServerPort"))
            {
                this.TryAction((obj) =>
                {
                    var theServerPort = int.Parse(tbxServerPort.Text);
                    Utils.IsPortValid(theServerPort);
                    _lastSelectedSsServerInfo.server_port = theServerPort;
                });
            }

            if (theControlName.Contains("Remarks"))
            {
                _lastSelectedSsServerInfo.remarks = tbxRemarks.Text;
            }

            if (theControlName.Contains("LocalProxyPort"))
            {
                this.TryAction((obj) =>
                {
                    var localPort = int.Parse(btnLocalProxyPort.Text);
                    Utils.IsPortValid(localPort);
                    _currentConfiguration.localPort = localPort;
                });
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
        }

        private void RenderSelectedSsServerInfo()
        {
            if (_lastSelectedSsServerInfo == null)
            {
                return;
            }

            tbxServerIP.Text = _lastSelectedSsServerInfo.server;
            tbxServerPort.Text = _lastSelectedSsServerInfo.server_port.ToString();
            tbxPassword.Text = _lastSelectedSsServerInfo.password;
            btnLocalProxyPort.Text = _currentConfiguration.localPort.ToString();
            cbxEncryptions.SelectedItem = _lastSelectedSsServerInfo.method ?? "aes-256-cfb";
            tbxRemarks.Text = _lastSelectedSsServerInfo.remarks;
            gbxServerConfigs.Visible = true;
        }

        private bool IsCurrentConfigDataChanged()
        {
            var serializedConfigData = SimpleJson.SimpleJson.SerializeObject(_currentConfiguration);
            return string.Compare(_originalSerializedConfigData, serializedConfigData, true) != 0;
        }

        private void ViewTextBox_TextChanged(object sender, EventArgs e)
        {
            var theTextBox = sender as TextBox;
            UpdateCurrentConfigObject(theTextBox.Name);
            RenderConfigurationInfo(_currentConfiguration);

            this.btnOK.Enabled = this.IsCurrentConfigDataChanged();
        }

        private void cbxEncryptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            //btnLocalProxyPort
            var theCombo = sender as ComboBox;
            UpdateCurrentConfigObject(theCombo.Name);
            RenderConfigurationInfo(_currentConfiguration);
        }

        private void CacheOriginalConfigData()
        {
            // Cache original data, to check if the data has been changed
            _originalSerializedConfigData = SimpleJson.SimpleJson.SerializeObject(_currentConfiguration);
        }

        private void ShowStatusMessage(string message, bool isAppending = false)
        {
            if (isAppending)
            {
                lblStatus.Text += message;
            }
            else
            {
                lblStatus.Text = message;
            }
        }

        private bool TryAction(Action<object> action)
        {
            try
            {
                action(null);
                return true;
            }
            catch (Exception ex)
            {
                ShowStatusMessage(ex.Message, true);
                return false;
            }
        }
    }
}
