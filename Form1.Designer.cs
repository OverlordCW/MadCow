namespace MadCow
{
    partial class Form1
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
                this.notifyIcon1.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.EnableAutoUpdateBox = new System.Windows.Forms.CheckBox();
            this.AutoUpdateValue = new System.Windows.Forms.NumericUpDown();
            this.PlayDiabloButton = new System.Windows.Forms.Button();
            this.LaunchServerButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RemoteServerButton = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.TickGameServerPort = new System.Windows.Forms.PictureBox();
            this.TickBnetServerPort = new System.Windows.Forms.PictureBox();
            this.TickPublicServerIp = new System.Windows.Forms.PictureBox();
            this.TickGameServerIp = new System.Windows.Forms.PictureBox();
            this.TickBnetServerIP = new System.Windows.Forms.PictureBox();
            this.ErrorGameServerPort = new System.Windows.Forms.PictureBox();
            this.ErrorBnetServerPort = new System.Windows.Forms.PictureBox();
            this.ErrorPublicServerIp = new System.Windows.Forms.PictureBox();
            this.ErrorGameServerIp = new System.Windows.Forms.PictureBox();
            this.ErrorBnetServerIp = new System.Windows.Forms.PictureBox();
            this.LoadProfile = new System.Windows.Forms.Button();
            this.SaveProfile = new System.Windows.Forms.Button();
            this.MOTD = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.RestoreDefaults = new System.Windows.Forms.LinkLabel();
            this.NATcheckBox = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.PublicServerIp = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.GameServerPort = new System.Windows.Forms.TextBox();
            this.GameServerIp = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.BnetServerPort = new System.Windows.Forms.TextBox();
            this.BnetServerIp = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label25 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.BranchComboBox = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.FindDiabloButton = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.ValidateRepoButton = new System.Windows.Forms.Button();
            this.Diablo3UserPathSelection = new System.Windows.Forms.TextBox();
            this.UpdateMooegeButton = new System.Windows.Forms.Button();
            this.CopyMPQButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.generalProgressBar = new System.Windows.Forms.ProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.chain = new System.Windows.Forms.PictureBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.DownloadFileSpeed = new System.Windows.Forms.Label();
            this.DownloadingFileName = new System.Windows.Forms.Label();
            this.DownloadMPQSprogressBar = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.DownloadMPQSButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.ResetRepoFolder = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker5 = new System.ComponentModel.BackgroundWorker();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.backgroundWorker6 = new System.ComponentModel.BackgroundWorker();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.VersionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AutoUpdateValue)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TickGameServerPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TickBnetServerPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TickPublicServerIp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TickGameServerIp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TickBnetServerIP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGameServerPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorBnetServerPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorPublicServerIp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGameServerIp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorBnetServerIp)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chain)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // EnableAutoUpdateBox
            // 
            this.EnableAutoUpdateBox.AutoSize = true;
            this.EnableAutoUpdateBox.Location = new System.Drawing.Point(9, 43);
            this.EnableAutoUpdateBox.Name = "EnableAutoUpdateBox";
            this.EnableAutoUpdateBox.Size = new System.Drawing.Size(59, 17);
            this.EnableAutoUpdateBox.TabIndex = 3;
            this.EnableAutoUpdateBox.Text = "Enable";
            this.EnableAutoUpdateBox.UseVisualStyleBackColor = true;
            this.EnableAutoUpdateBox.CheckedChanged += new System.EventHandler(this.AutoUpdate_CheckedChanged);
            // 
            // AutoUpdateValue
            // 
            this.AutoUpdateValue.Location = new System.Drawing.Point(5, 19);
            this.AutoUpdateValue.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.AutoUpdateValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AutoUpdateValue.Name = "AutoUpdateValue";
            this.AutoUpdateValue.Size = new System.Drawing.Size(86, 20);
            this.AutoUpdateValue.TabIndex = 4;
            this.AutoUpdateValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AutoUpdateValue.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // PlayDiabloButton
            // 
            this.PlayDiabloButton.BackColor = System.Drawing.Color.Transparent;
            this.PlayDiabloButton.Enabled = false;
            this.PlayDiabloButton.Location = new System.Drawing.Point(9, 286);
            this.PlayDiabloButton.Name = "PlayDiabloButton";
            this.PlayDiabloButton.Size = new System.Drawing.Size(327, 42);
            this.PlayDiabloButton.TabIndex = 7;
            this.PlayDiabloButton.Text = "Play Diablo (Local)";
            this.PlayDiabloButton.UseVisualStyleBackColor = false;
            this.PlayDiabloButton.Click += new System.EventHandler(this.PlayDiablo_Click);
            // 
            // LaunchServerButton
            // 
            this.LaunchServerButton.Location = new System.Drawing.Point(6, 279);
            this.LaunchServerButton.Name = "LaunchServerButton";
            this.LaunchServerButton.Size = new System.Drawing.Size(327, 43);
            this.LaunchServerButton.TabIndex = 9;
            this.LaunchServerButton.Text = "Launch Server Only";
            this.LaunchServerButton.UseVisualStyleBackColor = true;
            this.LaunchServerButton.Click += new System.EventHandler(this.LaunchServer_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.AutoUpdateValue);
            this.groupBox2.Controls.Add(this.EnableAutoUpdateBox);
            this.groupBox2.Location = new System.Drawing.Point(245, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(96, 68);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Auto Updating";
            // 
            // RemoteServerButton
            // 
            this.RemoteServerButton.Enabled = false;
            this.RemoteServerButton.Location = new System.Drawing.Point(101, 95);
            this.RemoteServerButton.Name = "RemoteServerButton";
            this.RemoteServerButton.Size = new System.Drawing.Size(141, 46);
            this.RemoteServerButton.TabIndex = 14;
            this.RemoteServerButton.Text = "Play on Remote Server";
            this.RemoteServerButton.UseVisualStyleBackColor = true;
            this.RemoteServerButton.Click += new System.EventHandler(this.RemoteServer_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.groupBox4);
            this.tabPage3.Controls.Add(this.RemoteServerButton);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(345, 334);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Remote Server";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(54, 153);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(225, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "To Activate, Add Diablo Path on Updates Tab";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.textBox3);
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Location = new System.Drawing.Point(5, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(332, 83);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Remote Server";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(239, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "This is to connect to server on another computer.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(220, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Remote Port:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Remote Host:";
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(223, 57);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 1;
            this.textBox3.Text = "1345";
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(3, 57);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(197, 20);
            this.textBox2.TabIndex = 0;
            this.textBox2.Text = "0.0.0.0";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label12.Location = new System.Drawing.Point(5, 133);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(334, 186);
            this.label12.TabIndex = 1;
            this.label12.Text = resources.GetString("label12.Text");
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(345, 334);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Server Control";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.TickGameServerPort);
            this.groupBox5.Controls.Add(this.TickBnetServerPort);
            this.groupBox5.Controls.Add(this.TickPublicServerIp);
            this.groupBox5.Controls.Add(this.TickGameServerIp);
            this.groupBox5.Controls.Add(this.TickBnetServerIP);
            this.groupBox5.Controls.Add(this.ErrorGameServerPort);
            this.groupBox5.Controls.Add(this.ErrorBnetServerPort);
            this.groupBox5.Controls.Add(this.ErrorPublicServerIp);
            this.groupBox5.Controls.Add(this.ErrorGameServerIp);
            this.groupBox5.Controls.Add(this.ErrorBnetServerIp);
            this.groupBox5.Controls.Add(this.LoadProfile);
            this.groupBox5.Controls.Add(this.SaveProfile);
            this.groupBox5.Controls.Add(this.MOTD);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.RestoreDefaults);
            this.groupBox5.Controls.Add(this.LaunchServerButton);
            this.groupBox5.Controls.Add(this.NATcheckBox);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.PublicServerIp);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.GameServerPort);
            this.groupBox5.Controls.Add(this.GameServerIp);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.BnetServerPort);
            this.groupBox5.Controls.Add(this.BnetServerIp);
            this.groupBox5.Location = new System.Drawing.Point(3, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(339, 325);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Server Configuration";
            // 
            // TickGameServerPort
            // 
            this.TickGameServerPort.Image = global::MadCow.Properties.Resources.green_tick;
            this.TickGameServerPort.Location = new System.Drawing.Point(318, 80);
            this.TickGameServerPort.Name = "TickGameServerPort";
            this.TickGameServerPort.Size = new System.Drawing.Size(15, 16);
            this.TickGameServerPort.TabIndex = 25;
            this.TickGameServerPort.TabStop = false;
            this.TickGameServerPort.Visible = false;
            // 
            // TickBnetServerPort
            // 
            this.TickBnetServerPort.Image = global::MadCow.Properties.Resources.green_tick;
            this.TickBnetServerPort.Location = new System.Drawing.Point(318, 31);
            this.TickBnetServerPort.Name = "TickBnetServerPort";
            this.TickBnetServerPort.Size = new System.Drawing.Size(15, 16);
            this.TickBnetServerPort.TabIndex = 24;
            this.TickBnetServerPort.TabStop = false;
            this.TickBnetServerPort.Visible = false;
            // 
            // TickPublicServerIp
            // 
            this.TickPublicServerIp.Image = global::MadCow.Properties.Resources.green_tick;
            this.TickPublicServerIp.Location = new System.Drawing.Point(207, 132);
            this.TickPublicServerIp.Name = "TickPublicServerIp";
            this.TickPublicServerIp.Size = new System.Drawing.Size(15, 16);
            this.TickPublicServerIp.TabIndex = 23;
            this.TickPublicServerIp.TabStop = false;
            this.TickPublicServerIp.Visible = false;
            // 
            // TickGameServerIp
            // 
            this.TickGameServerIp.Image = global::MadCow.Properties.Resources.green_tick;
            this.TickGameServerIp.Location = new System.Drawing.Point(207, 81);
            this.TickGameServerIp.Name = "TickGameServerIp";
            this.TickGameServerIp.Size = new System.Drawing.Size(15, 16);
            this.TickGameServerIp.TabIndex = 22;
            this.TickGameServerIp.TabStop = false;
            this.TickGameServerIp.Visible = false;
            // 
            // TickBnetServerIP
            // 
            this.TickBnetServerIP.Image = global::MadCow.Properties.Resources.green_tick;
            this.TickBnetServerIP.Location = new System.Drawing.Point(207, 30);
            this.TickBnetServerIP.Name = "TickBnetServerIP";
            this.TickBnetServerIP.Size = new System.Drawing.Size(15, 16);
            this.TickBnetServerIP.TabIndex = 21;
            this.TickBnetServerIP.TabStop = false;
            this.TickBnetServerIP.Visible = false;
            // 
            // ErrorGameServerPort
            // 
            this.ErrorGameServerPort.Image = global::MadCow.Properties.Resources.error_cross;
            this.ErrorGameServerPort.Location = new System.Drawing.Point(318, 81);
            this.ErrorGameServerPort.Name = "ErrorGameServerPort";
            this.ErrorGameServerPort.Size = new System.Drawing.Size(15, 15);
            this.ErrorGameServerPort.TabIndex = 20;
            this.ErrorGameServerPort.TabStop = false;
            this.ErrorGameServerPort.Visible = false;
            // 
            // ErrorBnetServerPort
            // 
            this.ErrorBnetServerPort.Image = global::MadCow.Properties.Resources.error_cross;
            this.ErrorBnetServerPort.Location = new System.Drawing.Point(318, 31);
            this.ErrorBnetServerPort.Name = "ErrorBnetServerPort";
            this.ErrorBnetServerPort.Size = new System.Drawing.Size(15, 15);
            this.ErrorBnetServerPort.TabIndex = 19;
            this.ErrorBnetServerPort.TabStop = false;
            this.ErrorBnetServerPort.Visible = false;
            // 
            // ErrorPublicServerIp
            // 
            this.ErrorPublicServerIp.Image = global::MadCow.Properties.Resources.error_cross;
            this.ErrorPublicServerIp.Location = new System.Drawing.Point(207, 133);
            this.ErrorPublicServerIp.Name = "ErrorPublicServerIp";
            this.ErrorPublicServerIp.Size = new System.Drawing.Size(15, 15);
            this.ErrorPublicServerIp.TabIndex = 18;
            this.ErrorPublicServerIp.TabStop = false;
            this.ErrorPublicServerIp.Visible = false;
            // 
            // ErrorGameServerIp
            // 
            this.ErrorGameServerIp.Image = global::MadCow.Properties.Resources.error_cross;
            this.ErrorGameServerIp.Location = new System.Drawing.Point(207, 82);
            this.ErrorGameServerIp.Name = "ErrorGameServerIp";
            this.ErrorGameServerIp.Size = new System.Drawing.Size(15, 15);
            this.ErrorGameServerIp.TabIndex = 17;
            this.ErrorGameServerIp.TabStop = false;
            this.ErrorGameServerIp.Visible = false;
            // 
            // ErrorBnetServerIp
            // 
            this.ErrorBnetServerIp.Image = global::MadCow.Properties.Resources.error_cross;
            this.ErrorBnetServerIp.Location = new System.Drawing.Point(207, 31);
            this.ErrorBnetServerIp.Name = "ErrorBnetServerIp";
            this.ErrorBnetServerIp.Size = new System.Drawing.Size(15, 15);
            this.ErrorBnetServerIp.TabIndex = 16;
            this.ErrorBnetServerIp.TabStop = false;
            this.ErrorBnetServerIp.Visible = false;
            // 
            // LoadProfile
            // 
            this.LoadProfile.Location = new System.Drawing.Point(109, 220);
            this.LoadProfile.Name = "LoadProfile";
            this.LoadProfile.Size = new System.Drawing.Size(75, 42);
            this.LoadProfile.TabIndex = 15;
            this.LoadProfile.Text = "Load Profile";
            this.LoadProfile.UseVisualStyleBackColor = true;
            this.LoadProfile.Click += new System.EventHandler(this.LoadProfile_Click);
            // 
            // SaveProfile
            // 
            this.SaveProfile.Location = new System.Drawing.Point(11, 220);
            this.SaveProfile.Name = "SaveProfile";
            this.SaveProfile.Size = new System.Drawing.Size(75, 42);
            this.SaveProfile.TabIndex = 14;
            this.SaveProfile.Text = "Save Profile";
            this.SaveProfile.UseVisualStyleBackColor = true;
            this.SaveProfile.Click += new System.EventHandler(this.SaveProfile_Click);
            // 
            // MOTD
            // 
            this.MOTD.Location = new System.Drawing.Point(11, 183);
            this.MOTD.Name = "MOTD";
            this.MOTD.Size = new System.Drawing.Size(322, 20);
            this.MOTD.TabIndex = 12;
            this.MOTD.Text = "Welcome to mooege development server!";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 167);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 13);
            this.label18.TabIndex = 13;
            this.label18.Text = "MOTD";
            // 
            // RestoreDefaults
            // 
            this.RestoreDefaults.AutoSize = true;
            this.RestoreDefaults.LinkColor = System.Drawing.Color.DimGray;
            this.RestoreDefaults.Location = new System.Drawing.Point(247, 206);
            this.RestoreDefaults.Name = "RestoreDefaults";
            this.RestoreDefaults.Size = new System.Drawing.Size(86, 13);
            this.RestoreDefaults.TabIndex = 11;
            this.RestoreDefaults.TabStop = true;
            this.RestoreDefaults.Text = "Restore Defaults";
            this.RestoreDefaults.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RestoreDefault_LinkClicked);
            // 
            // NATcheckBox
            // 
            this.NATcheckBox.AutoSize = true;
            this.NATcheckBox.Location = new System.Drawing.Point(230, 133);
            this.NATcheckBox.Name = "NATcheckBox";
            this.NATcheckBox.Size = new System.Drawing.Size(90, 17);
            this.NATcheckBox.TabIndex = 10;
            this.NATcheckBox.Text = "NAT Enabled";
            this.NATcheckBox.UseVisualStyleBackColor = true;
            this.NATcheckBox.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 117);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "Public Server IP:";
            // 
            // PublicServerIp
            // 
            this.PublicServerIp.Location = new System.Drawing.Point(11, 133);
            this.PublicServerIp.Name = "PublicServerIp";
            this.PublicServerIp.Size = new System.Drawing.Size(196, 20);
            this.PublicServerIp.TabIndex = 8;
            this.PublicServerIp.Text = "0.0.0.0";
            this.PublicServerIp.TextChanged += new System.EventHandler(this.PublicServerIp_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(227, 66);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 13);
            this.label14.TabIndex = 7;
            this.label14.Text = "Game Server Port:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 66);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(85, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "Game Server IP:";
            // 
            // GameServerPort
            // 
            this.GameServerPort.Location = new System.Drawing.Point(230, 81);
            this.GameServerPort.Name = "GameServerPort";
            this.GameServerPort.Size = new System.Drawing.Size(83, 20);
            this.GameServerPort.TabIndex = 5;
            this.GameServerPort.Text = "1999";
            this.GameServerPort.TextChanged += new System.EventHandler(this.GameServerPort_TextChanged);
            // 
            // GameServerIp
            // 
            this.GameServerIp.Location = new System.Drawing.Point(11, 82);
            this.GameServerIp.Name = "GameServerIp";
            this.GameServerIp.Size = new System.Drawing.Size(196, 20);
            this.GameServerIp.TabIndex = 4;
            this.GameServerIp.Text = "0.0.0.0";
            this.GameServerIp.TextChanged += new System.EventHandler(this.GameServerIp_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(225, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "Bnet Server Port:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 15);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "Bnet Server IP:";
            // 
            // BnetServerPort
            // 
            this.BnetServerPort.Location = new System.Drawing.Point(228, 31);
            this.BnetServerPort.Name = "BnetServerPort";
            this.BnetServerPort.Size = new System.Drawing.Size(85, 20);
            this.BnetServerPort.TabIndex = 1;
            this.BnetServerPort.Text = "1345";
            this.BnetServerPort.TextChanged += new System.EventHandler(this.BnetServerPort_TextChanged);
            // 
            // BnetServerIp
            // 
            this.BnetServerIp.Location = new System.Drawing.Point(11, 31);
            this.BnetServerIp.Name = "BnetServerIp";
            this.BnetServerIp.Size = new System.Drawing.Size(196, 20);
            this.BnetServerIp.TabIndex = 0;
            this.BnetServerIp.Text = "0.0.0.0";
            this.BnetServerIp.TextChanged += new System.EventHandler(this.BnetServerIp_TextChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label25);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.FindDiabloButton);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.PlayDiabloButton);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.ValidateRepoButton);
            this.tabPage1.Controls.Add(this.Diablo3UserPathSelection);
            this.tabPage1.Controls.Add(this.UpdateMooegeButton);
            this.tabPage1.Controls.Add(this.CopyMPQButton);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(345, 334);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Updates";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label25.Location = new System.Drawing.Point(4, 270);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(335, 13);
            this.label25.TabIndex = 19;
            this.label25.Text = "Disable \"Remember Last Repository\" to view repo selection list again.";
            this.toolTip1.SetToolTip(this.label25, "Help Tab -> Remember Last Repository");
            this.label25.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 14;
            this.label1.UseMnemonic = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.BranchComboBox);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 68);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Repository";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label24.Location = new System.Drawing.Point(103, 47);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(104, 13);
            this.label24.TabIndex = 19;
            this.label24.Text = "Branch Selection";
            this.label24.Visible = false;
            // 
            // BranchComboBox
            // 
            this.BranchComboBox.FormattingEnabled = true;
            this.BranchComboBox.Location = new System.Drawing.Point(7, 43);
            this.BranchComboBox.Name = "BranchComboBox";
            this.BranchComboBox.Size = new System.Drawing.Size(90, 21);
            this.BranchComboBox.TabIndex = 18;
            this.BranchComboBox.Visible = false;
            this.BranchComboBox.SelectedIndexChanged += new System.EventHandler(this.BranchComboBox_SelectedIndexChanged);
            this.BranchComboBox.SelectionChangeCommitted += new System.EventHandler(this.BranchComboBox_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 18);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(201, 21);
            this.comboBox1.TabIndex = 17;
            this.comboBox1.Text = "Type or Select a Repository.";
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(209, 18);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(17, 20);
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(209, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(17, 20);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label10.Location = new System.Drawing.Point(5, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(199, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "e.g https://github.com/mooege/mooege";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(201, 26);
            this.label4.TabIndex = 16;
            // 
            // FindDiabloButton
            // 
            this.FindDiabloButton.Location = new System.Drawing.Point(263, 184);
            this.FindDiabloButton.Name = "FindDiabloButton";
            this.FindDiabloButton.Size = new System.Drawing.Size(75, 20);
            this.FindDiabloButton.TabIndex = 4;
            this.FindDiabloButton.Text = "Find Diablo3";
            this.FindDiabloButton.UseVisualStyleBackColor = true;
            this.FindDiabloButton.Click += new System.EventHandler(this.FindDiablo_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.panel1);
            this.groupBox6.Location = new System.Drawing.Point(5, 80);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(333, 42);
            this.groupBox6.TabIndex = 18;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Download Bar";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar2);
            this.panel1.Location = new System.Drawing.Point(9, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(316, 17);
            this.panel1.TabIndex = 17;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(5, 4);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(308, 10);
            this.progressBar2.TabIndex = 0;
            // 
            // ValidateRepoButton
            // 
            this.ValidateRepoButton.Location = new System.Drawing.Point(9, 221);
            this.ValidateRepoButton.Name = "ValidateRepoButton";
            this.ValidateRepoButton.Size = new System.Drawing.Size(94, 44);
            this.ValidateRepoButton.TabIndex = 0;
            this.ValidateRepoButton.Text = "Validate Repository";
            this.ValidateRepoButton.UseVisualStyleBackColor = true;
            this.ValidateRepoButton.Click += new System.EventHandler(this.Validate_Repository_Click);
            // 
            // Diablo3UserPathSelection
            // 
            this.Diablo3UserPathSelection.Location = new System.Drawing.Point(9, 184);
            this.Diablo3UserPathSelection.Name = "Diablo3UserPathSelection";
            this.Diablo3UserPathSelection.ReadOnly = true;
            this.Diablo3UserPathSelection.Size = new System.Drawing.Size(248, 20);
            this.Diablo3UserPathSelection.TabIndex = 3;
            // 
            // UpdateMooegeButton
            // 
            this.UpdateMooegeButton.Enabled = false;
            this.UpdateMooegeButton.Location = new System.Drawing.Point(123, 221);
            this.UpdateMooegeButton.Name = "UpdateMooegeButton";
            this.UpdateMooegeButton.Size = new System.Drawing.Size(100, 44);
            this.UpdateMooegeButton.TabIndex = 17;
            this.UpdateMooegeButton.Text = "Update Mooege";
            this.UpdateMooegeButton.UseVisualStyleBackColor = true;
            this.UpdateMooegeButton.Click += new System.EventHandler(this.Update_Mooege_Click);
            // 
            // CopyMPQButton
            // 
            this.CopyMPQButton.Enabled = false;
            this.CopyMPQButton.Location = new System.Drawing.Point(245, 221);
            this.CopyMPQButton.Name = "CopyMPQButton";
            this.CopyMPQButton.Size = new System.Drawing.Size(93, 44);
            this.CopyMPQButton.TabIndex = 18;
            this.CopyMPQButton.Text = "Copy MPQ\'s";
            this.CopyMPQButton.UseVisualStyleBackColor = true;
            this.CopyMPQButton.Click += new System.EventHandler(this.CopyMPQs_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Location = new System.Drawing.Point(6, 128);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(332, 45);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "General Progress";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.generalProgressBar);
            this.panel2.Location = new System.Drawing.Point(9, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(316, 17);
            this.panel2.TabIndex = 17;
            // 
            // generalProgressBar
            // 
            this.generalProgressBar.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.generalProgressBar.ForeColor = System.Drawing.Color.RoyalBlue;
            this.generalProgressBar.Location = new System.Drawing.Point(5, 3);
            this.generalProgressBar.Name = "generalProgressBar";
            this.generalProgressBar.Size = new System.Drawing.Size(308, 11);
            this.generalProgressBar.Step = 25;
            this.generalProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.generalProgressBar.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(3, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(353, 360);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.button5);
            this.tabPage4.Controls.Add(this.chain);
            this.tabPage4.Controls.Add(this.label27);
            this.tabPage4.Controls.Add(this.label26);
            this.tabPage4.Controls.Add(this.button4);
            this.tabPage4.Controls.Add(this.label23);
            this.tabPage4.Controls.Add(this.button3);
            this.tabPage4.Controls.Add(this.label22);
            this.tabPage4.Controls.Add(this.label21);
            this.tabPage4.Controls.Add(this.label20);
            this.tabPage4.Controls.Add(this.button2);
            this.tabPage4.Controls.Add(this.label19);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.button1);
            this.tabPage4.Controls.Add(this.DownloadFileSpeed);
            this.tabPage4.Controls.Add(this.DownloadingFileName);
            this.tabPage4.Controls.Add(this.DownloadMPQSprogressBar);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.DownloadMPQSButton);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.ResetRepoFolder);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(345, 334);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Help";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(218, 92);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(108, 35);
            this.button5.TabIndex = 25;
            this.button5.Text = "Update MadCow";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // chain
            // 
            this.chain.Image = global::MadCow.Properties.Resources.process_chain;
            this.chain.Location = new System.Drawing.Point(292, 296);
            this.chain.Name = "chain";
            this.chain.Size = new System.Drawing.Size(18, 14);
            this.chain.TabIndex = 24;
            this.chain.TabStop = false;
            this.chain.Visible = false;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.SeaGreen;
            this.label27.Location = new System.Drawing.Point(274, 281);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(0, 13);
            this.label27.TabIndex = 23;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(22, 281);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(83, 13);
            this.label26.TabIndex = 22;
            this.label26.Text = "Minimize to Tray";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(179, 275);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 25);
            this.button4.TabIndex = 21;
            this.button4.Text = "On/Off";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.SeaGreen;
            this.label23.Location = new System.Drawing.Point(274, 251);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(0, 13);
            this.label23.TabIndex = 20;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(179, 245);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 25);
            this.button3.TabIndex = 19;
            this.button3.Text = "On/Off";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(22, 251);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(134, 13);
            this.label22.TabIndex = 18;
            this.label22.Text = "Remember Last Repository";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.SeaGreen;
            this.label21.Location = new System.Drawing.Point(274, 311);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(0, 13);
            this.label21.TabIndex = 17;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(22, 311);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(89, 13);
            this.label20.TabIndex = 16;
            this.label20.Text = "Tray Notifications";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(179, 306);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "On/Off";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(22, 221);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(106, 13);
            this.label19.TabIndex = 14;
            this.label19.Text = "Shortcut Auto Create";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.SeaGreen;
            this.label9.Location = new System.Drawing.Point(274, 221);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(179, 215);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 12;
            this.button1.Text = "On/Off";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DownloadFileSpeed
            // 
            this.DownloadFileSpeed.AutoSize = true;
            this.DownloadFileSpeed.Location = new System.Drawing.Point(23, 189);
            this.DownloadFileSpeed.Name = "DownloadFileSpeed";
            this.DownloadFileSpeed.Size = new System.Drawing.Size(95, 13);
            this.DownloadFileSpeed.TabIndex = 10;
            this.DownloadFileSpeed.Text = "Download Speed: ";
            this.DownloadFileSpeed.Visible = false;
            // 
            // DownloadingFileName
            // 
            this.DownloadingFileName.AutoSize = true;
            this.DownloadingFileName.Location = new System.Drawing.Point(23, 176);
            this.DownloadingFileName.Name = "DownloadingFileName";
            this.DownloadingFileName.Size = new System.Drawing.Size(94, 13);
            this.DownloadingFileName.TabIndex = 9;
            this.DownloadingFileName.Text = "Downloading File: ";
            this.DownloadingFileName.Visible = false;
            // 
            // DownloadMPQSprogressBar
            // 
            this.DownloadMPQSprogressBar.Location = new System.Drawing.Point(25, 144);
            this.DownloadMPQSprogressBar.Name = "DownloadMPQSprogressBar";
            this.DownloadMPQSprogressBar.Size = new System.Drawing.Size(301, 29);
            this.DownloadMPQSprogressBar.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Download all MPQ needed by Mooege.";
            // 
            // DownloadMPQSButton
            // 
            this.DownloadMPQSButton.Location = new System.Drawing.Point(218, 48);
            this.DownloadMPQSButton.Name = "DownloadMPQSButton";
            this.DownloadMPQSButton.Size = new System.Drawing.Size(108, 38);
            this.DownloadMPQSButton.TabIndex = 6;
            this.DownloadMPQSButton.Text = "Download MPQ\'s";
            this.DownloadMPQSButton.UseVisualStyleBackColor = true;
            this.DownloadMPQSButton.Click += new System.EventHandler(this.DownloadMPQSButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Reset Mooege Repository Folder";
            // 
            // ResetRepoFolder
            // 
            this.ResetRepoFolder.Location = new System.Drawing.Point(218, 6);
            this.ResetRepoFolder.Name = "ResetRepoFolder";
            this.ResetRepoFolder.Size = new System.Drawing.Size(108, 36);
            this.ResetRepoFolder.TabIndex = 2;
            this.ResetRepoFolder.Text = "Reset Folder";
            this.ResetRepoFolder.UseVisualStyleBackColor = true;
            this.ResetRepoFolder.Click += new System.EventHandler(this.ResetRepoFolder_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox7);
            this.tabPage5.Controls.Add(this.label12);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(345, 334);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "About";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "exe";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk_1);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(6, 6);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(301, 322);
            this.txtConsole.TabIndex = 16;
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.WorkerReportsProgress = true;
            this.backgroundWorker3.WorkerSupportsCancellation = true;
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DownloadMPQS);
            this.backgroundWorker3.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.downloader_ProgressChanged);
            this.backgroundWorker3.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.downloader_DownloadedComplete);
            // 
            // backgroundWorker4
            // 
            this.backgroundWorker4.WorkerReportsProgress = true;
            this.backgroundWorker4.WorkerSupportsCancellation = true;
            this.backgroundWorker4.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DownloadSpecificMPQS);
            this.backgroundWorker4.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.downloader_ProgressChanged2);
            this.backgroundWorker4.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.downloader_DownloadedComplete2);
            // 
            // backgroundWorker5
            // 
            this.backgroundWorker5.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker5_DoWork);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Location = new System.Drawing.Point(358, 0);
            this.tabControl2.Multiline = true;
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(318, 360);
            this.tabControl2.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl2.TabIndex = 17;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.txtConsole);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(310, 334);
            this.tabPage6.TabIndex = 0;
            this.tabPage6.Text = "Output";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.textBox1);
            this.tabPage7.Controls.Add(this.label3);
            this.tabPage7.Controls.Add(this.comboBox2);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(310, 334);
            this.tabPage7.TabIndex = 1;
            this.tabPage7.Text = "Changelog";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(8, 37);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(295, 291);
            this.textBox1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Select A Repository";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(182, 10);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 0;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // backgroundWorker6
            // 
            this.backgroundWorker6.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker6_DoWork);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "MadCow";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.VersionLabel);
            this.groupBox7.Location = new System.Drawing.Point(73, 21);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(200, 100);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Version";
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(76, 45);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(0, 13);
            this.VersionLabel.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 362);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MadCow By Wesko";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.AutoUpdateValue)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TickGameServerPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TickBnetServerPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TickPublicServerIp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TickGameServerIp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TickBnetServerIP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGameServerPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorBnetServerPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorPublicServerIp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGameServerIp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorBnetServerIp)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chain)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox EnableAutoUpdateBox;
        private System.Windows.Forms.NumericUpDown AutoUpdateValue;
        private System.Windows.Forms.Button LaunchServerButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button RemoteServerButton;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.LinkLabel RestoreDefaults;
        private System.Windows.Forms.CheckBox NATcheckBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox PublicServerIp;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox GameServerPort;
        private System.Windows.Forms.TextBox GameServerIp;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox BnetServerPort;
        private System.Windows.Forms.TextBox BnetServerIp;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button FindDiabloButton;
        private System.Windows.Forms.TextBox Diablo3UserPathSelection;
        private System.Windows.Forms.Button ValidateRepoButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button UpdateMooegeButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ResetRepoFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox MOTD;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Button LoadProfile;
        private System.Windows.Forms.Button SaveProfile;
        private System.Windows.Forms.PictureBox ErrorGameServerPort;
        private System.Windows.Forms.PictureBox ErrorBnetServerPort;
        private System.Windows.Forms.PictureBox ErrorPublicServerIp;
        private System.Windows.Forms.PictureBox ErrorGameServerIp;
        private System.Windows.Forms.PictureBox ErrorBnetServerIp;
        private System.Windows.Forms.PictureBox TickBnetServerIP;
        private System.Windows.Forms.PictureBox TickGameServerPort;
        private System.Windows.Forms.PictureBox TickBnetServerPort;
        private System.Windows.Forms.PictureBox TickPublicServerIp;
        private System.Windows.Forms.PictureBox TickGameServerIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DownloadMPQSButton;
        public System.Windows.Forms.ProgressBar DownloadMPQSprogressBar;
        private System.Windows.Forms.Label DownloadingFileName;
        private System.Windows.Forms.Label DownloadFileSpeed;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
        private System.ComponentModel.BackgroundWorker backgroundWorker5;
        public System.Windows.Forms.TextBox txtConsole;
        public System.Windows.Forms.ProgressBar generalProgressBar;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker6;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
        public System.Windows.Forms.Button PlayDiabloButton;
        public System.Windows.Forms.Button CopyMPQButton;
        public System.Windows.Forms.ComboBox BranchComboBox;
        public System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label19;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label21;
        public System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        public System.Windows.Forms.Label label25;
        public System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox chain;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label VersionLabel;

    }
}