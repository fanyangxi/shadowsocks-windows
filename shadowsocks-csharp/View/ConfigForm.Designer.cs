namespace Shadowsocks.View
{
    partial class ConfigForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRemarks = new System.Windows.Forms.TextBox();
            this.RemarksLabel = new System.Windows.Forms.Label();
            this.IPLabel = new System.Windows.Forms.Label();
            this.ServerPortLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.tbxServerIP = new System.Windows.Forms.TextBox();
            this.tbxServerPort = new System.Windows.Forms.TextBox();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.EncryptionLabel = new System.Windows.Forms.Label();
            this.cbxEncryptions = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.gbxServerConfigs = new System.Windows.Forms.GroupBox();
            this.lbxServersList = new System.Windows.Forms.ListBox();
            this.btnLocalProxyPort = new System.Windows.Forms.TextBox();
            this.ProxyPortLabel = new System.Windows.Forms.Label();
            this.gbxGeneral = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.gbxServerConfigs.SuspendLayout();
            this.gbxGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRemarks
            // 
            this.btnRemarks.Location = new System.Drawing.Point(263, 127);
            this.btnRemarks.MaxLength = 32;
            this.btnRemarks.Name = "btnRemarks";
            this.btnRemarks.Size = new System.Drawing.Size(160, 21);
            this.btnRemarks.TabIndex = 10;
            this.btnRemarks.WordWrap = false;
            this.btnRemarks.TextChanged += new System.EventHandler(this.ViewTextBox_TextChanged);
            // 
            // RemarksLabel
            // 
            this.RemarksLabel.Location = new System.Drawing.Point(187, 127);
            this.RemarksLabel.Name = "RemarksLabel";
            this.RemarksLabel.Size = new System.Drawing.Size(72, 21);
            this.RemarksLabel.TabIndex = 9;
            this.RemarksLabel.Text = "Remarks";
            this.RemarksLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // IPLabel
            // 
            this.IPLabel.Location = new System.Drawing.Point(187, 17);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(72, 21);
            this.IPLabel.TabIndex = 0;
            this.IPLabel.Text = "Server IP";
            this.IPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ServerPortLabel
            // 
            this.ServerPortLabel.Location = new System.Drawing.Point(187, 44);
            this.ServerPortLabel.Name = "ServerPortLabel";
            this.ServerPortLabel.Size = new System.Drawing.Size(72, 21);
            this.ServerPortLabel.TabIndex = 1;
            this.ServerPortLabel.Text = "Server Port";
            this.ServerPortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.Location = new System.Drawing.Point(187, 71);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(72, 21);
            this.PasswordLabel.TabIndex = 2;
            this.PasswordLabel.Text = "Password";
            this.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbxServerIP
            // 
            this.tbxServerIP.Location = new System.Drawing.Point(263, 17);
            this.tbxServerIP.MaxLength = 512;
            this.tbxServerIP.Name = "tbxServerIP";
            this.tbxServerIP.Size = new System.Drawing.Size(160, 21);
            this.tbxServerIP.TabIndex = 0;
            this.tbxServerIP.WordWrap = false;
            this.tbxServerIP.TextChanged += new System.EventHandler(this.ViewTextBox_TextChanged);
            // 
            // tbxServerPort
            // 
            this.tbxServerPort.Location = new System.Drawing.Point(263, 44);
            this.tbxServerPort.MaxLength = 10;
            this.tbxServerPort.Name = "tbxServerPort";
            this.tbxServerPort.Size = new System.Drawing.Size(160, 21);
            this.tbxServerPort.TabIndex = 1;
            this.tbxServerPort.WordWrap = false;
            this.tbxServerPort.TextChanged += new System.EventHandler(this.ViewTextBox_TextChanged);
            // 
            // tbxPassword
            // 
            this.tbxPassword.Location = new System.Drawing.Point(263, 71);
            this.tbxPassword.MaxLength = 256;
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.PasswordChar = '*';
            this.tbxPassword.Size = new System.Drawing.Size(160, 21);
            this.tbxPassword.TabIndex = 2;
            this.tbxPassword.WordWrap = false;
            this.tbxPassword.TextChanged += new System.EventHandler(this.ViewTextBox_TextChanged);
            // 
            // EncryptionLabel
            // 
            this.EncryptionLabel.Location = new System.Drawing.Point(187, 98);
            this.EncryptionLabel.Name = "EncryptionLabel";
            this.EncryptionLabel.Size = new System.Drawing.Size(72, 21);
            this.EncryptionLabel.TabIndex = 8;
            this.EncryptionLabel.Text = "Encryption";
            this.EncryptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbxEncryptions
            // 
            this.cbxEncryptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEncryptions.FormattingEnabled = true;
            this.cbxEncryptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbxEncryptions.ItemHeight = 12;
            this.cbxEncryptions.Items.AddRange(new object[] {
            "table",
            "rc4-md5",
            "salsa20",
            "chacha20",
            "aes-256-cfb",
            "aes-192-cfb",
            "aes-128-cfb",
            "rc4"});
            this.cbxEncryptions.Location = new System.Drawing.Point(263, 98);
            this.cbxEncryptions.Name = "cbxEncryptions";
            this.cbxEncryptions.Size = new System.Drawing.Size(160, 20);
            this.cbxEncryptions.TabIndex = 3;
            this.cbxEncryptions.SelectedIndexChanged += new System.EventHandler(this.cbxEncryptions_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Location = new System.Drawing.Point(76, 125);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0, 0);
            this.panel2.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(10, 227);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(91, 227);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Location = new System.Drawing.Point(94, 125);
            this.btnDeleteItem.Margin = new System.Windows.Forms.Padding(3, 6, 0, 3);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(80, 23);
            this.btnDeleteItem.TabIndex = 7;
            this.btnDeleteItem.Text = "&Delete";
            this.btnDeleteItem.UseVisualStyleBackColor = true;
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(8, 125);
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(0, 6, 3, 3);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(80, 23);
            this.btnAddItem.TabIndex = 6;
            this.btnAddItem.Text = "&Add";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // gbxServerConfigs
            // 
            this.gbxServerConfigs.Controls.Add(this.btnRemarks);
            this.gbxServerConfigs.Controls.Add(this.btnDeleteItem);
            this.gbxServerConfigs.Controls.Add(this.RemarksLabel);
            this.gbxServerConfigs.Controls.Add(this.lbxServersList);
            this.gbxServerConfigs.Controls.Add(this.IPLabel);
            this.gbxServerConfigs.Controls.Add(this.ServerPortLabel);
            this.gbxServerConfigs.Controls.Add(this.btnAddItem);
            this.gbxServerConfigs.Controls.Add(this.PasswordLabel);
            this.gbxServerConfigs.Controls.Add(this.panel2);
            this.gbxServerConfigs.Controls.Add(this.tbxServerIP);
            this.gbxServerConfigs.Controls.Add(this.cbxEncryptions);
            this.gbxServerConfigs.Controls.Add(this.tbxServerPort);
            this.gbxServerConfigs.Controls.Add(this.EncryptionLabel);
            this.gbxServerConfigs.Controls.Add(this.tbxPassword);
            this.gbxServerConfigs.Location = new System.Drawing.Point(10, 11);
            this.gbxServerConfigs.Name = "gbxServerConfigs";
            this.gbxServerConfigs.Size = new System.Drawing.Size(432, 156);
            this.gbxServerConfigs.TabIndex = 6;
            this.gbxServerConfigs.TabStop = false;
            this.gbxServerConfigs.Text = "Server Configs:";
            // 
            // lbxServersList
            // 
            this.lbxServersList.FormattingEnabled = true;
            this.lbxServersList.IntegralHeight = false;
            this.lbxServersList.ItemHeight = 12;
            this.lbxServersList.Location = new System.Drawing.Point(8, 17);
            this.lbxServersList.Margin = new System.Windows.Forms.Padding(0);
            this.lbxServersList.Name = "lbxServersList";
            this.lbxServersList.Size = new System.Drawing.Size(166, 102);
            this.lbxServersList.TabIndex = 5;
            this.lbxServersList.SelectedIndexChanged += new System.EventHandler(this.lbxServersList_SelectedIndexChanged);
            // 
            // btnLocalProxyPort
            // 
            this.btnLocalProxyPort.Location = new System.Drawing.Point(81, 19);
            this.btnLocalProxyPort.MaxLength = 10;
            this.btnLocalProxyPort.Name = "btnLocalProxyPort";
            this.btnLocalProxyPort.Size = new System.Drawing.Size(341, 21);
            this.btnLocalProxyPort.TabIndex = 4;
            this.btnLocalProxyPort.WordWrap = false;
            this.btnLocalProxyPort.TextChanged += new System.EventHandler(this.ViewTextBox_TextChanged);
            // 
            // ProxyPortLabel
            // 
            this.ProxyPortLabel.Location = new System.Drawing.Point(10, 19);
            this.ProxyPortLabel.Name = "ProxyPortLabel";
            this.ProxyPortLabel.Size = new System.Drawing.Size(65, 21);
            this.ProxyPortLabel.TabIndex = 3;
            this.ProxyPortLabel.Text = "Proxy Port";
            this.ProxyPortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbxGeneral
            // 
            this.gbxGeneral.Controls.Add(this.btnLocalProxyPort);
            this.gbxGeneral.Controls.Add(this.ProxyPortLabel);
            this.gbxGeneral.Location = new System.Drawing.Point(10, 171);
            this.gbxGeneral.Name = "gbxGeneral";
            this.gbxGeneral.Size = new System.Drawing.Size(431, 51);
            this.gbxGeneral.TabIndex = 8;
            this.gbxGeneral.TabStop = false;
            this.gbxGeneral.Text = "General";
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(169, 229);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(270, 21);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(451, 258);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbxGeneral);
            this.Controls.Add(this.gbxServerConfigs);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Servers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigForm_FormClosed);
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.Shown += new System.EventHandler(this.ConfigForm_Shown);
            this.gbxServerConfigs.ResumeLayout(false);
            this.gbxServerConfigs.PerformLayout();
            this.gbxGeneral.ResumeLayout(false);
            this.gbxGeneral.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Label ServerPortLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label RemarksLabel;
        private System.Windows.Forms.Label ProxyPortLabel;
        private System.Windows.Forms.Label EncryptionLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbxServerIP;
        private System.Windows.Forms.TextBox tbxServerPort;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.TextBox btnRemarks;
        private System.Windows.Forms.TextBox btnLocalProxyPort;
        private System.Windows.Forms.ComboBox cbxEncryptions;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDeleteItem;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.GroupBox gbxServerConfigs;
        private System.Windows.Forms.GroupBox gbxGeneral;
        private System.Windows.Forms.ListBox lbxServersList;
        private System.Windows.Forms.Label lblStatus;
    }
}

