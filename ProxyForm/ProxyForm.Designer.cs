namespace MadCow.ProxyForm
{
    partial class ProxyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProxyForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Password = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.Label();
            this.Port = new System.Windows.Forms.Label();
            this.IP = new System.Windows.Forms.Label();
            this.PasswordTxtBox = new System.Windows.Forms.TextBox();
            this.UserNameTxtBox = new System.Windows.Forms.TextBox();
            this.PortTxtBox = new System.Windows.Forms.TextBox();
            this.IPTxtBox = new System.Windows.Forms.TextBox();
            this.ProxyOkButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Password);
            this.groupBox1.Controls.Add(this.Username);
            this.groupBox1.Controls.Add(this.Port);
            this.groupBox1.Controls.Add(this.IP);
            this.groupBox1.Controls.Add(this.PasswordTxtBox);
            this.groupBox1.Controls.Add(this.UserNameTxtBox);
            this.groupBox1.Controls.Add(this.PortTxtBox);
            this.groupBox1.Controls.Add(this.IPTxtBox);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 124);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Proxy Configuration";
            // 
            // Password
            // 
            this.Password.AutoSize = true;
            this.Password.Location = new System.Drawing.Point(6, 100);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(53, 13);
            this.Password.TabIndex = 6;
            this.Password.Text = "Password";
            // 
            // Username
            // 
            this.Username.AutoSize = true;
            this.Username.Location = new System.Drawing.Point(4, 74);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(55, 13);
            this.Username.TabIndex = 5;
            this.Username.Text = "Username";
            // 
            // Port
            // 
            this.Port.AutoSize = true;
            this.Port.Location = new System.Drawing.Point(6, 48);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(26, 13);
            this.Port.TabIndex = 4;
            this.Port.Text = "Port";
            // 
            // IP
            // 
            this.IP.AutoSize = true;
            this.IP.Location = new System.Drawing.Point(6, 22);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(17, 13);
            this.IP.TabIndex = 3;
            this.IP.Text = "IP";
            // 
            // PasswordTxtBox
            // 
            this.PasswordTxtBox.Location = new System.Drawing.Point(65, 97);
            this.PasswordTxtBox.Name = "PasswordTxtBox";
            this.PasswordTxtBox.PasswordChar = '*';
            this.PasswordTxtBox.Size = new System.Drawing.Size(119, 20);
            this.PasswordTxtBox.TabIndex = 2;
            // 
            // UserNameTxtBox
            // 
            this.UserNameTxtBox.Location = new System.Drawing.Point(65, 71);
            this.UserNameTxtBox.Name = "UserNameTxtBox";
            this.UserNameTxtBox.Size = new System.Drawing.Size(119, 20);
            this.UserNameTxtBox.TabIndex = 1;
            // 
            // PortTxtBox
            // 
            this.PortTxtBox.Location = new System.Drawing.Point(65, 45);
            this.PortTxtBox.Name = "PortTxtBox";
            this.PortTxtBox.Size = new System.Drawing.Size(54, 20);
            this.PortTxtBox.TabIndex = 1;
            // 
            // IPTxtBox
            // 
            this.IPTxtBox.Location = new System.Drawing.Point(65, 19);
            this.IPTxtBox.Name = "IPTxtBox";
            this.IPTxtBox.Size = new System.Drawing.Size(119, 20);
            this.IPTxtBox.TabIndex = 0;
            // 
            // ProxyOkButton
            // 
            this.ProxyOkButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProxyOkButton.Location = new System.Drawing.Point(0, 143);
            this.ProxyOkButton.Name = "ProxyOkButton";
            this.ProxyOkButton.Size = new System.Drawing.Size(222, 43);
            this.ProxyOkButton.TabIndex = 1;
            this.ProxyOkButton.Text = "OK";
            this.ProxyOkButton.UseVisualStyleBackColor = true;
            this.ProxyOkButton.Click += new System.EventHandler(this.ProxyOkButton_Click);
            // 
            // ProxyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(222, 186);
            this.Controls.Add(this.ProxyOkButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProxyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proxy Configuration";
            this.Load += new System.EventHandler(this.ProxyForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label Port;
        private System.Windows.Forms.Label IP;
        private System.Windows.Forms.TextBox PasswordTxtBox;
        private System.Windows.Forms.TextBox UserNameTxtBox;
        private System.Windows.Forms.TextBox PortTxtBox;
        private System.Windows.Forms.TextBox IPTxtBox;
        private System.Windows.Forms.Label Password;
        private System.Windows.Forms.Label Username;
        private System.Windows.Forms.Button ProxyOkButton;

    }
}