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
            this.RemarksTextBox = new System.Windows.Forms.TextBox();
            this.RemarksLabel = new System.Windows.Forms.Label();
            this.IPLabel = new System.Windows.Forms.Label();
            this.ServerPortLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.ServerPortTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.EncryptionLabel = new System.Windows.Forms.Label();
            this.EncryptionSelect = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.OKButton = new System.Windows.Forms.Button();
            this.MyCancelButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.ServerGroupBox = new System.Windows.Forms.GroupBox();
            this.ServersListBox = new System.Windows.Forms.ListBox();
            this.ProxyPortTextBox = new System.Windows.Forms.TextBox();
            this.ProxyPortLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ServerGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RemarksTextBox
            // 
            this.RemarksTextBox.Location = new System.Drawing.Point(263, 124);
            this.RemarksTextBox.MaxLength = 32;
            this.RemarksTextBox.Name = "RemarksTextBox";
            this.RemarksTextBox.Size = new System.Drawing.Size(160, 21);
            this.RemarksTextBox.TabIndex = 10;
            this.RemarksTextBox.WordWrap = false;
            // 
            // RemarksLabel
            // 
            this.RemarksLabel.Location = new System.Drawing.Point(187, 124);
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
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(263, 17);
            this.IPTextBox.MaxLength = 512;
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(160, 21);
            this.IPTextBox.TabIndex = 0;
            this.IPTextBox.WordWrap = false;
            // 
            // ServerPortTextBox
            // 
            this.ServerPortTextBox.Location = new System.Drawing.Point(263, 44);
            this.ServerPortTextBox.MaxLength = 10;
            this.ServerPortTextBox.Name = "ServerPortTextBox";
            this.ServerPortTextBox.Size = new System.Drawing.Size(160, 21);
            this.ServerPortTextBox.TabIndex = 1;
            this.ServerPortTextBox.WordWrap = false;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(263, 71);
            this.PasswordTextBox.MaxLength = 256;
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(160, 21);
            this.PasswordTextBox.TabIndex = 2;
            this.PasswordTextBox.WordWrap = false;
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
            // EncryptionSelect
            // 
            this.EncryptionSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EncryptionSelect.FormattingEnabled = true;
            this.EncryptionSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.EncryptionSelect.ItemHeight = 12;
            this.EncryptionSelect.Items.AddRange(new object[] {
            "table",
            "rc4-md5",
            "salsa20",
            "chacha20",
            "aes-256-cfb",
            "aes-192-cfb",
            "aes-128-cfb",
            "rc4"});
            this.EncryptionSelect.Location = new System.Drawing.Point(263, 98);
            this.EncryptionSelect.Name = "EncryptionSelect";
            this.EncryptionSelect.Size = new System.Drawing.Size(160, 20);
            this.EncryptionSelect.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Location = new System.Drawing.Point(76, 122);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0, 0);
            this.panel2.TabIndex = 1;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(10, 224);
            this.OKButton.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 25);
            this.OKButton.TabIndex = 8;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // MyCancelButton
            // 
            this.MyCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MyCancelButton.Location = new System.Drawing.Point(91, 224);
            this.MyCancelButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.MyCancelButton.Name = "MyCancelButton";
            this.MyCancelButton.Size = new System.Drawing.Size(75, 25);
            this.MyCancelButton.TabIndex = 9;
            this.MyCancelButton.Text = "Cancel";
            this.MyCancelButton.UseVisualStyleBackColor = true;
            this.MyCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(94, 122);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(3, 6, 0, 3);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(80, 23);
            this.DeleteButton.TabIndex = 7;
            this.DeleteButton.Text = "&Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(8, 122);
            this.AddButton.Margin = new System.Windows.Forms.Padding(0, 6, 3, 3);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(80, 23);
            this.AddButton.TabIndex = 6;
            this.AddButton.Text = "&Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ServerGroupBox
            // 
            this.ServerGroupBox.Controls.Add(this.RemarksTextBox);
            this.ServerGroupBox.Controls.Add(this.DeleteButton);
            this.ServerGroupBox.Controls.Add(this.RemarksLabel);
            this.ServerGroupBox.Controls.Add(this.ServersListBox);
            this.ServerGroupBox.Controls.Add(this.IPLabel);
            this.ServerGroupBox.Controls.Add(this.ServerPortLabel);
            this.ServerGroupBox.Controls.Add(this.AddButton);
            this.ServerGroupBox.Controls.Add(this.PasswordLabel);
            this.ServerGroupBox.Controls.Add(this.panel2);
            this.ServerGroupBox.Controls.Add(this.IPTextBox);
            this.ServerGroupBox.Controls.Add(this.EncryptionSelect);
            this.ServerGroupBox.Controls.Add(this.ServerPortTextBox);
            this.ServerGroupBox.Controls.Add(this.EncryptionLabel);
            this.ServerGroupBox.Controls.Add(this.PasswordTextBox);
            this.ServerGroupBox.Location = new System.Drawing.Point(10, 11);
            this.ServerGroupBox.Name = "ServerGroupBox";
            this.ServerGroupBox.Size = new System.Drawing.Size(432, 153);
            this.ServerGroupBox.TabIndex = 6;
            this.ServerGroupBox.TabStop = false;
            this.ServerGroupBox.Text = "Server Configs:";
            // 
            // ServersListBox
            // 
            this.ServersListBox.FormattingEnabled = true;
            this.ServersListBox.IntegralHeight = false;
            this.ServersListBox.ItemHeight = 12;
            this.ServersListBox.Location = new System.Drawing.Point(8, 17);
            this.ServersListBox.Margin = new System.Windows.Forms.Padding(0);
            this.ServersListBox.Name = "ServersListBox";
            this.ServersListBox.Size = new System.Drawing.Size(166, 102);
            this.ServersListBox.TabIndex = 5;
            this.ServersListBox.SelectedIndexChanged += new System.EventHandler(this.ServersListBox_SelectedIndexChanged);
            // 
            // ProxyPortTextBox
            // 
            this.ProxyPortTextBox.Location = new System.Drawing.Point(81, 19);
            this.ProxyPortTextBox.MaxLength = 10;
            this.ProxyPortTextBox.Name = "ProxyPortTextBox";
            this.ProxyPortTextBox.Size = new System.Drawing.Size(341, 21);
            this.ProxyPortTextBox.TabIndex = 4;
            this.ProxyPortTextBox.WordWrap = false;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ProxyPortTextBox);
            this.groupBox1.Controls.Add(this.ProxyPortLabel);
            this.groupBox1.Location = new System.Drawing.Point(11, 167);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 51);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.MyCancelButton;
            this.ClientSize = new System.Drawing.Size(451, 255);
            this.Controls.Add(this.MyCancelButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ServerGroupBox);
            this.Controls.Add(this.OKButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Servers";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigForm_FormClosed);
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.Shown += new System.EventHandler(this.ConfigForm_Shown);
            this.ServerGroupBox.ResumeLayout(false);
            this.ServerGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Label ServerPortLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.TextBox ServerPortTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label EncryptionLabel;
        private System.Windows.Forms.ComboBox EncryptionSelect;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button MyCancelButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.GroupBox ServerGroupBox;
        private System.Windows.Forms.ListBox ServersListBox;
        private System.Windows.Forms.TextBox RemarksTextBox;
        private System.Windows.Forms.Label RemarksLabel;
        private System.Windows.Forms.TextBox ProxyPortTextBox;
        private System.Windows.Forms.Label ProxyPortLabel;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

