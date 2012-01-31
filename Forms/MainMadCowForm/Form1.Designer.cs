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
                this.MadCowTrayIcon.Dispose();
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
            this.RemoteServerLaunchButton = new System.Windows.Forms.Button();
            this.RemoteServerTab = new System.Windows.Forms.TabPage();
            this.RemoteServerTip = new System.Windows.Forms.Label();
            this.RemoteServerGrpBox = new System.Windows.Forms.GroupBox();
            this.RemoteServerTip2 = new System.Windows.Forms.Label();
            this.RemotePort = new System.Windows.Forms.Label();
            this.RemoteHost = new System.Windows.Forms.Label();
            this.remotePortTxtBox = new System.Windows.Forms.TextBox();
            this.remoteHostTxtBox = new System.Windows.Forms.TextBox();
            this.ServerControlTab = new System.Windows.Forms.TabPage();
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
            this.MotdTxtBox = new System.Windows.Forms.TextBox();
            this.MotdLabel = new System.Windows.Forms.Label();
            this.RestoreDefaultsLabel = new System.Windows.Forms.LinkLabel();
            this.NATcheckBox = new System.Windows.Forms.CheckBox();
            this.PublicServerIpLabel = new System.Windows.Forms.Label();
            this.PublicServerIp = new System.Windows.Forms.TextBox();
            this.GameServerPortLabel = new System.Windows.Forms.Label();
            this.GameServerIpLabel = new System.Windows.Forms.Label();
            this.GameServerPort = new System.Windows.Forms.TextBox();
            this.GameServerIp = new System.Windows.Forms.TextBox();
            this.BnetServerPortLabel = new System.Windows.Forms.Label();
            this.BnetServerIpLabel = new System.Windows.Forms.Label();
            this.BnetServerPort = new System.Windows.Forms.TextBox();
            this.BnetServerIp = new System.Windows.Forms.TextBox();
            this.UpdatesTab = new System.Windows.Forms.TabPage();
            this.LastPlayedRepoReminderLabel = new System.Windows.Forms.Label();
            this.AutoUpdateTimerLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BranchSelectionLabel = new System.Windows.Forms.Label();
            this.BranchComboBox = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.RepositoryHintLabel = new System.Windows.Forms.Label();
            this.FindDiabloButton = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DownloadRepoBar = new System.Windows.Forms.ProgressBar();
            this.ValidateRepoButton = new System.Windows.Forms.Button();
            this.Diablo3UserPathSelection = new System.Windows.Forms.TextBox();
            this.UpdateMooegeButton = new System.Windows.Forms.Button();
            this.CopyMPQButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.generalProgressBar = new System.Windows.Forms.ProgressBar();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.MooegeTab = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.SettingsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.BrowseMPQPathButton = new System.Windows.Forms.Button();
            this.MPQDestTextBox = new System.Windows.Forms.TextBox();
            this.mpqDestinationDisableLabel = new System.Windows.Forms.Label();
            this.HelpTab = new System.Windows.Forms.TabPage();
            this.DownloadNetLabel = new System.Windows.Forms.LinkLabel();
            this.ProblemsCompilingLabel = new System.Windows.Forms.Label();
            this.AutoUpdateMadcowLabel = new System.Windows.Forms.Label();
            this.UpdateMadcowButton = new System.Windows.Forms.Button();
            this.DownloadSpeedLabel = new System.Windows.Forms.Label();
            this.DownloadFileNameLabel = new System.Windows.Forms.Label();
            this.DownloadMPQSprogressBar = new System.Windows.Forms.ProgressBar();
            this.DownloadMpqLabel = new System.Windows.Forms.Label();
            this.DownloadMPQSButton = new System.Windows.Forms.Button();
            this.ResetMooegeReposLabel = new System.Windows.Forms.Label();
            this.ResetRepoFolder = new System.Windows.Forms.Button();
            this.DownloadSpeedTimer = new System.Windows.Forms.Timer(this.components);
            this.DiabloPathFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.DownloadRepository = new System.ComponentModel.BackgroundWorker();
            this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.ConsoleOutputTxtBox = new System.Windows.Forms.TextBox();
            this.VerifyDiablo3Version = new System.ComponentModel.BackgroundWorker();
            this.DownloadSelectedMpqs = new System.ComponentModel.BackgroundWorker();
            this.ErrorFilesDownloaders = new System.ComponentModel.BackgroundWorker();
            this.ValidateRepository = new System.ComponentModel.BackgroundWorker();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.OutPutTab = new System.Windows.Forms.TabPage();
            this.ChaneglogTab = new System.Windows.Forms.TabPage();
            this.ChangeLogTxtBox = new System.Windows.Forms.TextBox();
            this.SelectRepositoryChngLogLabel = new System.Windows.Forms.Label();
            this.SelectRepoChngLogComboBox = new System.Windows.Forms.ComboBox();
            this.LiveHelpTab = new System.Windows.Forms.TabPage();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.PleaseWaitLabel = new System.Windows.Forms.Label();
            this.Advertencia = new System.Windows.Forms.PictureBox();
            this.Rules = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BotonAlerta = new System.Windows.Forms.PictureBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.TypeHereLabel = new System.Windows.Forms.Label();
            this.ChatMessageBox = new System.Windows.Forms.TextBox();
            this.ChatUsersBox = new System.Windows.Forms.TextBox();
            this.ChatDisplayBox = new System.Windows.Forms.TextBox();
            this.ChangelogDownloader = new System.ComponentModel.BackgroundWorker();
            this.MadCowTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.MpqPathBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.madCowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableTrayNotificationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rememberLastRepositoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desktopShortcutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mooegeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileAsDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.AutoUpdateValue)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.RemoteServerTab.SuspendLayout();
            this.RemoteServerGrpBox.SuspendLayout();
            this.ServerControlTab.SuspendLayout();
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
            this.UpdatesTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.MooegeTab.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.HelpTab.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.OutPutTab.SuspendLayout();
            this.ChaneglogTab.SuspendLayout();
            this.LiveHelpTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Advertencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BotonAlerta)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // EnableAutoUpdateBox
            // 
            this.EnableAutoUpdateBox.AutoSize = true;
            this.EnableAutoUpdateBox.Location = new System.Drawing.Point(5, 45);
            this.EnableAutoUpdateBox.Name = "EnableAutoUpdateBox";
            this.EnableAutoUpdateBox.Size = new System.Drawing.Size(64, 17);
            this.EnableAutoUpdateBox.TabIndex = 3;
            this.EnableAutoUpdateBox.Text = "Enable";
            this.EnableAutoUpdateBox.UseVisualStyleBackColor = true;
            this.EnableAutoUpdateBox.CheckedChanged += new System.EventHandler(this.AutoUpdate_CheckedChanged);
            // 
            // AutoUpdateValue
            // 
            this.AutoUpdateValue.Location = new System.Drawing.Point(5, 18);
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
            this.AutoUpdateValue.Size = new System.Drawing.Size(81, 21);
            this.AutoUpdateValue.TabIndex = 4;
            this.AutoUpdateValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(245, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(91, 68);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Auto Update";
            // 
            // RemoteServerLaunchButton
            // 
            this.RemoteServerLaunchButton.BackColor = System.Drawing.Color.White;
            this.RemoteServerLaunchButton.Enabled = false;
            this.RemoteServerLaunchButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RemoteServerLaunchButton.Location = new System.Drawing.Point(101, 95);
            this.RemoteServerLaunchButton.Name = "RemoteServerLaunchButton";
            this.RemoteServerLaunchButton.Size = new System.Drawing.Size(141, 46);
            this.RemoteServerLaunchButton.TabIndex = 14;
            this.RemoteServerLaunchButton.Text = "Play on Remote Server";
            this.ToolTips.SetToolTip(this.RemoteServerLaunchButton, "Play on a server hosted elsewhere.");
            this.RemoteServerLaunchButton.UseVisualStyleBackColor = false;
            this.RemoteServerLaunchButton.Click += new System.EventHandler(this.RemoteServer_Click);
            // 
            // RemoteServerTab
            // 
            this.RemoteServerTab.BackColor = System.Drawing.Color.White;
            this.RemoteServerTab.Controls.Add(this.RemoteServerTip);
            this.RemoteServerTab.Controls.Add(this.RemoteServerGrpBox);
            this.RemoteServerTab.Controls.Add(this.RemoteServerLaunchButton);
            this.RemoteServerTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RemoteServerTab.Location = new System.Drawing.Point(4, 22);
            this.RemoteServerTab.Name = "RemoteServerTab";
            this.RemoteServerTab.Padding = new System.Windows.Forms.Padding(3);
            this.RemoteServerTab.Size = new System.Drawing.Size(345, 334);
            this.RemoteServerTab.TabIndex = 3;
            this.RemoteServerTab.Text = "Remote Server";
            // 
            // RemoteServerTip
            // 
            this.RemoteServerTip.AutoSize = true;
            this.RemoteServerTip.Location = new System.Drawing.Point(54, 153);
            this.RemoteServerTip.Name = "RemoteServerTip";
            this.RemoteServerTip.Size = new System.Drawing.Size(225, 13);
            this.RemoteServerTip.TabIndex = 15;
            this.RemoteServerTip.Text = "To Activate, Add Diablo Path on Updates Tab";
            // 
            // RemoteServerGrpBox
            // 
            this.RemoteServerGrpBox.Controls.Add(this.RemoteServerTip2);
            this.RemoteServerGrpBox.Controls.Add(this.RemotePort);
            this.RemoteServerGrpBox.Controls.Add(this.RemoteHost);
            this.RemoteServerGrpBox.Controls.Add(this.remotePortTxtBox);
            this.RemoteServerGrpBox.Controls.Add(this.remoteHostTxtBox);
            this.RemoteServerGrpBox.Location = new System.Drawing.Point(5, 6);
            this.RemoteServerGrpBox.Name = "RemoteServerGrpBox";
            this.RemoteServerGrpBox.Size = new System.Drawing.Size(332, 83);
            this.RemoteServerGrpBox.TabIndex = 1;
            this.RemoteServerGrpBox.TabStop = false;
            this.RemoteServerGrpBox.Text = "Remote Server";
            // 
            // RemoteServerTip2
            // 
            this.RemoteServerTip2.AutoSize = true;
            this.RemoteServerTip2.Location = new System.Drawing.Point(6, 16);
            this.RemoteServerTip2.Name = "RemoteServerTip2";
            this.RemoteServerTip2.Size = new System.Drawing.Size(239, 13);
            this.RemoteServerTip2.TabIndex = 15;
            this.RemoteServerTip2.Text = "This is to connect to server on another computer.";
            // 
            // RemotePort
            // 
            this.RemotePort.AutoSize = true;
            this.RemotePort.Location = new System.Drawing.Point(220, 41);
            this.RemotePort.Name = "RemotePort";
            this.RemotePort.Size = new System.Drawing.Size(69, 13);
            this.RemotePort.TabIndex = 3;
            this.RemotePort.Text = "Remote Port:";
            // 
            // RemoteHost
            // 
            this.RemoteHost.AutoSize = true;
            this.RemoteHost.Location = new System.Drawing.Point(1, 41);
            this.RemoteHost.Name = "RemoteHost";
            this.RemoteHost.Size = new System.Drawing.Size(72, 13);
            this.RemoteHost.TabIndex = 2;
            this.RemoteHost.Text = "Remote Host:";
            // 
            // remotePortTxtBox
            // 
            this.remotePortTxtBox.Enabled = false;
            this.remotePortTxtBox.Location = new System.Drawing.Point(223, 57);
            this.remotePortTxtBox.Name = "remotePortTxtBox";
            this.remotePortTxtBox.Size = new System.Drawing.Size(100, 20);
            this.remotePortTxtBox.TabIndex = 1;
            this.remotePortTxtBox.Text = "1345";
            // 
            // remoteHostTxtBox
            // 
            this.remoteHostTxtBox.Enabled = false;
            this.remoteHostTxtBox.Location = new System.Drawing.Point(3, 57);
            this.remoteHostTxtBox.Name = "remoteHostTxtBox";
            this.remoteHostTxtBox.Size = new System.Drawing.Size(197, 20);
            this.remoteHostTxtBox.TabIndex = 0;
            this.remoteHostTxtBox.Text = "0.0.0.0";
            // 
            // ServerControlTab
            // 
            this.ServerControlTab.BackColor = System.Drawing.Color.White;
            this.ServerControlTab.Controls.Add(this.groupBox5);
            this.ServerControlTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ServerControlTab.Location = new System.Drawing.Point(4, 22);
            this.ServerControlTab.Name = "ServerControlTab";
            this.ServerControlTab.Padding = new System.Windows.Forms.Padding(3);
            this.ServerControlTab.Size = new System.Drawing.Size(345, 334);
            this.ServerControlTab.TabIndex = 2;
            this.ServerControlTab.Text = "Server Control";
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
            this.groupBox5.Controls.Add(this.MotdTxtBox);
            this.groupBox5.Controls.Add(this.MotdLabel);
            this.groupBox5.Controls.Add(this.RestoreDefaultsLabel);
            this.groupBox5.Controls.Add(this.LaunchServerButton);
            this.groupBox5.Controls.Add(this.NATcheckBox);
            this.groupBox5.Controls.Add(this.PublicServerIpLabel);
            this.groupBox5.Controls.Add(this.PublicServerIp);
            this.groupBox5.Controls.Add(this.GameServerPortLabel);
            this.groupBox5.Controls.Add(this.GameServerIpLabel);
            this.groupBox5.Controls.Add(this.GameServerPort);
            this.groupBox5.Controls.Add(this.GameServerIp);
            this.groupBox5.Controls.Add(this.BnetServerPortLabel);
            this.groupBox5.Controls.Add(this.BnetServerIpLabel);
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
            // MotdTxtBox
            // 
            this.MotdTxtBox.Location = new System.Drawing.Point(11, 183);
            this.MotdTxtBox.Name = "MotdTxtBox";
            this.MotdTxtBox.Size = new System.Drawing.Size(322, 20);
            this.MotdTxtBox.TabIndex = 12;
            this.MotdTxtBox.Text = "Welcome to mooege development server!";
            // 
            // MotdLabel
            // 
            this.MotdLabel.AutoSize = true;
            this.MotdLabel.Location = new System.Drawing.Point(8, 167);
            this.MotdLabel.Name = "MotdLabel";
            this.MotdLabel.Size = new System.Drawing.Size(39, 13);
            this.MotdLabel.TabIndex = 13;
            this.MotdLabel.Text = "MOTD";
            // 
            // RestoreDefaultsLabel
            // 
            this.RestoreDefaultsLabel.AutoSize = true;
            this.RestoreDefaultsLabel.LinkColor = System.Drawing.Color.DimGray;
            this.RestoreDefaultsLabel.Location = new System.Drawing.Point(247, 206);
            this.RestoreDefaultsLabel.Name = "RestoreDefaultsLabel";
            this.RestoreDefaultsLabel.Size = new System.Drawing.Size(86, 13);
            this.RestoreDefaultsLabel.TabIndex = 11;
            this.RestoreDefaultsLabel.TabStop = true;
            this.RestoreDefaultsLabel.Text = "Restore Defaults";
            this.RestoreDefaultsLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RestoreDefault_LinkClicked);
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
            // 
            // PublicServerIpLabel
            // 
            this.PublicServerIpLabel.AutoSize = true;
            this.PublicServerIpLabel.Location = new System.Drawing.Point(6, 117);
            this.PublicServerIpLabel.Name = "PublicServerIpLabel";
            this.PublicServerIpLabel.Size = new System.Drawing.Size(86, 13);
            this.PublicServerIpLabel.TabIndex = 9;
            this.PublicServerIpLabel.Text = "Public Server IP:";
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
            // GameServerPortLabel
            // 
            this.GameServerPortLabel.AutoSize = true;
            this.GameServerPortLabel.Location = new System.Drawing.Point(227, 66);
            this.GameServerPortLabel.Name = "GameServerPortLabel";
            this.GameServerPortLabel.Size = new System.Drawing.Size(94, 13);
            this.GameServerPortLabel.TabIndex = 7;
            this.GameServerPortLabel.Text = "Game Server Port:";
            // 
            // GameServerIpLabel
            // 
            this.GameServerIpLabel.AutoSize = true;
            this.GameServerIpLabel.Location = new System.Drawing.Point(8, 66);
            this.GameServerIpLabel.Name = "GameServerIpLabel";
            this.GameServerIpLabel.Size = new System.Drawing.Size(85, 13);
            this.GameServerIpLabel.TabIndex = 6;
            this.GameServerIpLabel.Text = "Game Server IP:";
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
            // BnetServerPortLabel
            // 
            this.BnetServerPortLabel.AutoSize = true;
            this.BnetServerPortLabel.Location = new System.Drawing.Point(225, 16);
            this.BnetServerPortLabel.Name = "BnetServerPortLabel";
            this.BnetServerPortLabel.Size = new System.Drawing.Size(88, 13);
            this.BnetServerPortLabel.TabIndex = 3;
            this.BnetServerPortLabel.Text = "Bnet Server Port:";
            // 
            // BnetServerIpLabel
            // 
            this.BnetServerIpLabel.AutoSize = true;
            this.BnetServerIpLabel.Location = new System.Drawing.Point(8, 15);
            this.BnetServerIpLabel.Name = "BnetServerIpLabel";
            this.BnetServerIpLabel.Size = new System.Drawing.Size(79, 13);
            this.BnetServerIpLabel.TabIndex = 2;
            this.BnetServerIpLabel.Text = "Bnet Server IP:";
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
            // UpdatesTab
            // 
            this.UpdatesTab.BackColor = System.Drawing.Color.White;
            this.UpdatesTab.Controls.Add(this.LastPlayedRepoReminderLabel);
            this.UpdatesTab.Controls.Add(this.AutoUpdateTimerLabel);
            this.UpdatesTab.Controls.Add(this.groupBox1);
            this.UpdatesTab.Controls.Add(this.FindDiabloButton);
            this.UpdatesTab.Controls.Add(this.groupBox6);
            this.UpdatesTab.Controls.Add(this.PlayDiabloButton);
            this.UpdatesTab.Controls.Add(this.groupBox2);
            this.UpdatesTab.Controls.Add(this.ValidateRepoButton);
            this.UpdatesTab.Controls.Add(this.Diablo3UserPathSelection);
            this.UpdatesTab.Controls.Add(this.UpdateMooegeButton);
            this.UpdatesTab.Controls.Add(this.CopyMPQButton);
            this.UpdatesTab.Controls.Add(this.groupBox3);
            this.UpdatesTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UpdatesTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdatesTab.ForeColor = System.Drawing.SystemColors.ControlText;
            this.UpdatesTab.Location = new System.Drawing.Point(4, 22);
            this.UpdatesTab.Name = "UpdatesTab";
            this.UpdatesTab.Padding = new System.Windows.Forms.Padding(3);
            this.UpdatesTab.Size = new System.Drawing.Size(345, 334);
            this.UpdatesTab.TabIndex = 1;
            this.UpdatesTab.Text = "Updates";
            // 
            // LastPlayedRepoReminderLabel
            // 
            this.LastPlayedRepoReminderLabel.AutoSize = true;
            this.LastPlayedRepoReminderLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.LastPlayedRepoReminderLabel.Location = new System.Drawing.Point(4, 270);
            this.LastPlayedRepoReminderLabel.Name = "LastPlayedRepoReminderLabel";
            this.LastPlayedRepoReminderLabel.Size = new System.Drawing.Size(335, 13);
            this.LastPlayedRepoReminderLabel.TabIndex = 19;
            this.LastPlayedRepoReminderLabel.Text = "Disable \"Remember Last Repository\" to view repo selection list again.";
            this.ToolTips.SetToolTip(this.LastPlayedRepoReminderLabel, "Help Tab -> Remember Last Repository");
            this.LastPlayedRepoReminderLabel.Visible = false;
            // 
            // AutoUpdateTimerLabel
            // 
            this.AutoUpdateTimerLabel.AutoSize = true;
            this.AutoUpdateTimerLabel.Location = new System.Drawing.Point(120, 237);
            this.AutoUpdateTimerLabel.Name = "AutoUpdateTimerLabel";
            this.AutoUpdateTimerLabel.Size = new System.Drawing.Size(0, 13);
            this.AutoUpdateTimerLabel.TabIndex = 14;
            this.AutoUpdateTimerLabel.UseMnemonic = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BranchSelectionLabel);
            this.groupBox1.Controls.Add(this.BranchComboBox);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.RepositoryHintLabel);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 68);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Repository";
            // 
            // BranchSelectionLabel
            // 
            this.BranchSelectionLabel.AutoSize = true;
            this.BranchSelectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BranchSelectionLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.BranchSelectionLabel.Location = new System.Drawing.Point(103, 47);
            this.BranchSelectionLabel.Name = "BranchSelectionLabel";
            this.BranchSelectionLabel.Size = new System.Drawing.Size(104, 13);
            this.BranchSelectionLabel.TabIndex = 19;
            this.BranchSelectionLabel.Text = "Branch Selection";
            this.BranchSelectionLabel.Visible = false;
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
            // RepositoryHintLabel
            // 
            this.RepositoryHintLabel.AutoSize = true;
            this.RepositoryHintLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.RepositoryHintLabel.Location = new System.Drawing.Point(5, 42);
            this.RepositoryHintLabel.Name = "RepositoryHintLabel";
            this.RepositoryHintLabel.Size = new System.Drawing.Size(199, 13);
            this.RepositoryHintLabel.TabIndex = 13;
            this.RepositoryHintLabel.Text = "e.g https://github.com/mooege/mooege";
            this.RepositoryHintLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.panel1.Controls.Add(this.DownloadRepoBar);
            this.panel1.Location = new System.Drawing.Point(9, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(316, 17);
            this.panel1.TabIndex = 17;
            // 
            // DownloadRepoBar
            // 
            this.DownloadRepoBar.Location = new System.Drawing.Point(5, 4);
            this.DownloadRepoBar.Name = "DownloadRepoBar";
            this.DownloadRepoBar.Size = new System.Drawing.Size(308, 10);
            this.DownloadRepoBar.TabIndex = 0;
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
            // Tabs
            // 
            this.Tabs.Controls.Add(this.UpdatesTab);
            this.Tabs.Controls.Add(this.ServerControlTab);
            this.Tabs.Controls.Add(this.RemoteServerTab);
            this.Tabs.Controls.Add(this.MooegeTab);
            this.Tabs.Controls.Add(this.HelpTab);
            this.Tabs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Tabs.HotTrack = true;
            this.Tabs.Location = new System.Drawing.Point(3, 27);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(353, 360);
            this.Tabs.TabIndex = 13;
            // 
            // MooegeTab
            // 
            this.MooegeTab.BackColor = System.Drawing.Color.White;
            this.MooegeTab.Controls.Add(this.groupBox9);
            this.MooegeTab.Controls.Add(this.groupBox8);
            this.MooegeTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MooegeTab.Location = new System.Drawing.Point(4, 22);
            this.MooegeTab.Name = "MooegeTab";
            this.MooegeTab.Padding = new System.Windows.Forms.Padding(3);
            this.MooegeTab.Size = new System.Drawing.Size(345, 334);
            this.MooegeTab.TabIndex = 6;
            this.MooegeTab.Text = "Mooege";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.SettingsCheckedListBox);
            this.groupBox9.Location = new System.Drawing.Point(6, 66);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(333, 262);
            this.groupBox9.TabIndex = 1;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Mooege Settings";
            // 
            // SettingsCheckedListBox
            // 
            this.SettingsCheckedListBox.CheckOnClick = true;
            this.SettingsCheckedListBox.FormattingEnabled = true;
            this.SettingsCheckedListBox.Items.AddRange(new object[] {
            "Enable File Logging",
            "Enable Packet Logging",
            "Enable Tasks",
            "Enable LazyLoading",
            "Enable No Password Check"});
            this.SettingsCheckedListBox.Location = new System.Drawing.Point(6, 19);
            this.SettingsCheckedListBox.Name = "SettingsCheckedListBox";
            this.SettingsCheckedListBox.Size = new System.Drawing.Size(321, 79);
            this.SettingsCheckedListBox.TabIndex = 0;
            this.SettingsCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.SettingsCheckedListBox_SelectedIndexChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.BrowseMPQPathButton);
            this.groupBox8.Controls.Add(this.MPQDestTextBox);
            this.groupBox8.Controls.Add(this.mpqDestinationDisableLabel);
            this.groupBox8.Location = new System.Drawing.Point(6, 6);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(333, 54);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "MPQ Destination";
            // 
            // BrowseMPQPathButton
            // 
            this.BrowseMPQPathButton.Location = new System.Drawing.Point(303, 17);
            this.BrowseMPQPathButton.Name = "BrowseMPQPathButton";
            this.BrowseMPQPathButton.Size = new System.Drawing.Size(24, 23);
            this.BrowseMPQPathButton.TabIndex = 1;
            this.BrowseMPQPathButton.Text = "...";
            this.BrowseMPQPathButton.UseVisualStyleBackColor = true;
            this.BrowseMPQPathButton.Click += new System.EventHandler(this.BrowseMPQPathButton_Click);
            // 
            // MPQDestTextBox
            // 
            this.MPQDestTextBox.Location = new System.Drawing.Point(6, 19);
            this.MPQDestTextBox.Name = "MPQDestTextBox";
            this.MPQDestTextBox.ReadOnly = true;
            this.MPQDestTextBox.Size = new System.Drawing.Size(291, 20);
            this.MPQDestTextBox.TabIndex = 0;
            // 
            // mpqDestinationDisableLabel
            // 
            this.mpqDestinationDisableLabel.AutoSize = true;
            this.mpqDestinationDisableLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpqDestinationDisableLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.mpqDestinationDisableLabel.Location = new System.Drawing.Point(15, 38);
            this.mpqDestinationDisableLabel.Name = "mpqDestinationDisableLabel";
            this.mpqDestinationDisableLabel.Size = new System.Drawing.Size(301, 13);
            this.mpqDestinationDisableLabel.TabIndex = 20;
            this.mpqDestinationDisableLabel.Text = "Disable \"Remember Last Repository\" to enable this.";
            this.ToolTips.SetToolTip(this.mpqDestinationDisableLabel, "Help Tab -> Remember Last Repository");
            this.mpqDestinationDisableLabel.Visible = false;
            // 
            // HelpTab
            // 
            this.HelpTab.BackColor = System.Drawing.Color.White;
            this.HelpTab.Controls.Add(this.DownloadNetLabel);
            this.HelpTab.Controls.Add(this.ProblemsCompilingLabel);
            this.HelpTab.Controls.Add(this.AutoUpdateMadcowLabel);
            this.HelpTab.Controls.Add(this.UpdateMadcowButton);
            this.HelpTab.Controls.Add(this.DownloadSpeedLabel);
            this.HelpTab.Controls.Add(this.DownloadFileNameLabel);
            this.HelpTab.Controls.Add(this.DownloadMPQSprogressBar);
            this.HelpTab.Controls.Add(this.DownloadMpqLabel);
            this.HelpTab.Controls.Add(this.DownloadMPQSButton);
            this.HelpTab.Controls.Add(this.ResetMooegeReposLabel);
            this.HelpTab.Controls.Add(this.ResetRepoFolder);
            this.HelpTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HelpTab.Location = new System.Drawing.Point(4, 22);
            this.HelpTab.Name = "HelpTab";
            this.HelpTab.Padding = new System.Windows.Forms.Padding(3);
            this.HelpTab.Size = new System.Drawing.Size(345, 334);
            this.HelpTab.TabIndex = 4;
            this.HelpTab.Text = "Help";
            // 
            // DownloadNetLabel
            // 
            this.DownloadNetLabel.AutoSize = true;
            this.DownloadNetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadNetLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.DownloadNetLabel.Location = new System.Drawing.Point(218, 17);
            this.DownloadNetLabel.Name = "DownloadNetLabel";
            this.DownloadNetLabel.Size = new System.Drawing.Size(103, 15);
            this.DownloadNetLabel.TabIndex = 28;
            this.DownloadNetLabel.TabStop = true;
            this.DownloadNetLabel.Text = "Download .NET 4";
            this.DownloadNetLabel.VisitedLinkColor = System.Drawing.Color.Silver;
            this.DownloadNetLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.DownloadNetLabel_LinkClicked);
            // 
            // ProblemsCompilingLabel
            // 
            this.ProblemsCompilingLabel.AutoSize = true;
            this.ProblemsCompilingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProblemsCompilingLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ProblemsCompilingLabel.Location = new System.Drawing.Point(18, 19);
            this.ProblemsCompilingLabel.Name = "ProblemsCompilingLabel";
            this.ProblemsCompilingLabel.Size = new System.Drawing.Size(137, 13);
            this.ProblemsCompilingLabel.TabIndex = 27;
            this.ProblemsCompilingLabel.Text = "Mooege wont compile?";
            // 
            // AutoUpdateMadcowLabel
            // 
            this.AutoUpdateMadcowLabel.AutoSize = true;
            this.AutoUpdateMadcowLabel.Location = new System.Drawing.Point(18, 109);
            this.AutoUpdateMadcowLabel.Name = "AutoUpdateMadcowLabel";
            this.AutoUpdateMadcowLabel.Size = new System.Drawing.Size(112, 13);
            this.AutoUpdateMadcowLabel.TabIndex = 26;
            this.AutoUpdateMadcowLabel.Text = "Auto Update MadCow";
            // 
            // UpdateMadcowButton
            // 
            this.UpdateMadcowButton.Location = new System.Drawing.Point(218, 103);
            this.UpdateMadcowButton.Name = "UpdateMadcowButton";
            this.UpdateMadcowButton.Size = new System.Drawing.Size(108, 24);
            this.UpdateMadcowButton.TabIndex = 25;
            this.UpdateMadcowButton.Text = "Update MadCow";
            this.ToolTips.SetToolTip(this.UpdateMadcowButton, "Searchs and Autoupdate MadCow");
            this.UpdateMadcowButton.UseVisualStyleBackColor = true;
            this.UpdateMadcowButton.Click += new System.EventHandler(this.button5_Click);
            // 
            // DownloadSpeedLabel
            // 
            this.DownloadSpeedLabel.AutoSize = true;
            this.DownloadSpeedLabel.Location = new System.Drawing.Point(23, 189);
            this.DownloadSpeedLabel.Name = "DownloadSpeedLabel";
            this.DownloadSpeedLabel.Size = new System.Drawing.Size(95, 13);
            this.DownloadSpeedLabel.TabIndex = 10;
            this.DownloadSpeedLabel.Text = "Download Speed: ";
            this.DownloadSpeedLabel.Visible = false;
            // 
            // DownloadFileNameLabel
            // 
            this.DownloadFileNameLabel.AutoSize = true;
            this.DownloadFileNameLabel.Location = new System.Drawing.Point(23, 176);
            this.DownloadFileNameLabel.Name = "DownloadFileNameLabel";
            this.DownloadFileNameLabel.Size = new System.Drawing.Size(94, 13);
            this.DownloadFileNameLabel.TabIndex = 9;
            this.DownloadFileNameLabel.Text = "Downloading File: ";
            this.DownloadFileNameLabel.Visible = false;
            // 
            // DownloadMPQSprogressBar
            // 
            this.DownloadMPQSprogressBar.Location = new System.Drawing.Point(25, 143);
            this.DownloadMPQSprogressBar.Name = "DownloadMPQSprogressBar";
            this.DownloadMPQSprogressBar.Size = new System.Drawing.Size(301, 30);
            this.DownloadMPQSprogressBar.TabIndex = 8;
            // 
            // DownloadMpqLabel
            // 
            this.DownloadMpqLabel.AutoSize = true;
            this.DownloadMpqLabel.Location = new System.Drawing.Point(18, 78);
            this.DownloadMpqLabel.Name = "DownloadMpqLabel";
            this.DownloadMpqLabel.Size = new System.Drawing.Size(190, 13);
            this.DownloadMpqLabel.TabIndex = 7;
            this.DownloadMpqLabel.Text = "Download all MPQ needed by Mooege";
            // 
            // DownloadMPQSButton
            // 
            this.DownloadMPQSButton.Location = new System.Drawing.Point(218, 72);
            this.DownloadMPQSButton.Name = "DownloadMPQSButton";
            this.DownloadMPQSButton.Size = new System.Drawing.Size(108, 25);
            this.DownloadMPQSButton.TabIndex = 6;
            this.DownloadMPQSButton.Text = "Download MPQ\'s";
            this.DownloadMPQSButton.UseVisualStyleBackColor = true;
            this.DownloadMPQSButton.Click += new System.EventHandler(this.DownloadMPQSButton_Click);
            // 
            // ResetMooegeReposLabel
            // 
            this.ResetMooegeReposLabel.AutoSize = true;
            this.ResetMooegeReposLabel.Location = new System.Drawing.Point(18, 47);
            this.ResetMooegeReposLabel.Name = "ResetMooegeReposLabel";
            this.ResetMooegeReposLabel.Size = new System.Drawing.Size(162, 13);
            this.ResetMooegeReposLabel.TabIndex = 5;
            this.ResetMooegeReposLabel.Text = "Reset Mooege Repository Folder";
            // 
            // ResetRepoFolder
            // 
            this.ResetRepoFolder.Location = new System.Drawing.Point(218, 41);
            this.ResetRepoFolder.Name = "ResetRepoFolder";
            this.ResetRepoFolder.Size = new System.Drawing.Size(108, 25);
            this.ResetRepoFolder.TabIndex = 2;
            this.ResetRepoFolder.Text = "Reset Folder";
            this.ResetRepoFolder.UseVisualStyleBackColor = true;
            this.ResetRepoFolder.Click += new System.EventHandler(this.ResetRepoFolder_Click);
            // 
            // DownloadSpeedTimer
            // 
            this.DownloadSpeedTimer.Interval = 60000;
            this.DownloadSpeedTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DiabloPathFileDialog
            // 
            this.DiabloPathFileDialog.DefaultExt = "exe";
            this.DiabloPathFileDialog.FileName = "openFileDialog1";
            // 
            // DownloadRepository
            // 
            this.DownloadRepository.WorkerReportsProgress = true;
            this.DownloadRepository.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.DownloadRepository.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.DownloadRepository.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // ConsoleOutputTxtBox
            // 
            this.ConsoleOutputTxtBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ConsoleOutputTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConsoleOutputTxtBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ConsoleOutputTxtBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsoleOutputTxtBox.ForeColor = System.Drawing.Color.White;
            this.ConsoleOutputTxtBox.Location = new System.Drawing.Point(6, 6);
            this.ConsoleOutputTxtBox.Multiline = true;
            this.ConsoleOutputTxtBox.Name = "ConsoleOutputTxtBox";
            this.ConsoleOutputTxtBox.ReadOnly = true;
            this.ConsoleOutputTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConsoleOutputTxtBox.Size = new System.Drawing.Size(353, 322);
            this.ConsoleOutputTxtBox.TabIndex = 16;
            // 
            // VerifyDiablo3Version
            // 
            this.VerifyDiablo3Version.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // DownloadSelectedMpqs
            // 
            this.DownloadSelectedMpqs.WorkerReportsProgress = true;
            this.DownloadSelectedMpqs.WorkerSupportsCancellation = true;
            this.DownloadSelectedMpqs.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DownloadMPQS);
            this.DownloadSelectedMpqs.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.downloader_ProgressChanged);
            this.DownloadSelectedMpqs.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.downloader_DownloadedComplete);
            // 
            // ErrorFilesDownloaders
            // 
            this.ErrorFilesDownloaders.WorkerReportsProgress = true;
            this.ErrorFilesDownloaders.WorkerSupportsCancellation = true;
            this.ErrorFilesDownloaders.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DownloadSpecificMPQS);
            this.ErrorFilesDownloaders.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.downloader_ProgressChanged2);
            this.ErrorFilesDownloaders.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.downloader_DownloadedComplete2);
            // 
            // ValidateRepository
            // 
            this.ValidateRepository.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker5_DoWork);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.OutPutTab);
            this.tabControl2.Controls.Add(this.ChaneglogTab);
            this.tabControl2.Controls.Add(this.LiveHelpTab);
            this.tabControl2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabControl2.HotTrack = true;
            this.tabControl2.ItemSize = new System.Drawing.Size(96, 18);
            this.tabControl2.Location = new System.Drawing.Point(358, 27);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(373, 360);
            this.tabControl2.TabIndex = 17;
            // 
            // OutPutTab
            // 
            this.OutPutTab.Controls.Add(this.ConsoleOutputTxtBox);
            this.OutPutTab.Location = new System.Drawing.Point(4, 22);
            this.OutPutTab.Name = "OutPutTab";
            this.OutPutTab.Padding = new System.Windows.Forms.Padding(3);
            this.OutPutTab.Size = new System.Drawing.Size(365, 334);
            this.OutPutTab.TabIndex = 0;
            this.OutPutTab.Text = "Output";
            this.OutPutTab.UseVisualStyleBackColor = true;
            // 
            // ChaneglogTab
            // 
            this.ChaneglogTab.BackColor = System.Drawing.Color.Transparent;
            this.ChaneglogTab.Controls.Add(this.ChangeLogTxtBox);
            this.ChaneglogTab.Controls.Add(this.SelectRepositoryChngLogLabel);
            this.ChaneglogTab.Controls.Add(this.SelectRepoChngLogComboBox);
            this.ChaneglogTab.Location = new System.Drawing.Point(4, 22);
            this.ChaneglogTab.Name = "ChaneglogTab";
            this.ChaneglogTab.Padding = new System.Windows.Forms.Padding(3);
            this.ChaneglogTab.Size = new System.Drawing.Size(365, 334);
            this.ChaneglogTab.TabIndex = 1;
            this.ChaneglogTab.Text = "Changelog";
            // 
            // ChangeLogTxtBox
            // 
            this.ChangeLogTxtBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ChangeLogTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChangeLogTxtBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeLogTxtBox.ForeColor = System.Drawing.Color.White;
            this.ChangeLogTxtBox.Location = new System.Drawing.Point(8, 37);
            this.ChangeLogTxtBox.Multiline = true;
            this.ChangeLogTxtBox.Name = "ChangeLogTxtBox";
            this.ChangeLogTxtBox.ReadOnly = true;
            this.ChangeLogTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChangeLogTxtBox.Size = new System.Drawing.Size(351, 291);
            this.ChangeLogTxtBox.TabIndex = 2;
            // 
            // SelectRepositoryChngLogLabel
            // 
            this.SelectRepositoryChngLogLabel.AutoSize = true;
            this.SelectRepositoryChngLogLabel.Location = new System.Drawing.Point(66, 13);
            this.SelectRepositoryChngLogLabel.Name = "SelectRepositoryChngLogLabel";
            this.SelectRepositoryChngLogLabel.Size = new System.Drawing.Size(100, 13);
            this.SelectRepositoryChngLogLabel.TabIndex = 1;
            this.SelectRepositoryChngLogLabel.Text = "Select A Repository";
            // 
            // SelectRepoChngLogComboBox
            // 
            this.SelectRepoChngLogComboBox.FormattingEnabled = true;
            this.SelectRepoChngLogComboBox.Location = new System.Drawing.Point(172, 10);
            this.SelectRepoChngLogComboBox.Name = "SelectRepoChngLogComboBox";
            this.SelectRepoChngLogComboBox.Size = new System.Drawing.Size(121, 21);
            this.SelectRepoChngLogComboBox.TabIndex = 0;
            this.SelectRepoChngLogComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // LiveHelpTab
            // 
            this.LiveHelpTab.Controls.Add(this.DisconnectButton);
            this.LiveHelpTab.Controls.Add(this.PleaseWaitLabel);
            this.LiveHelpTab.Controls.Add(this.Advertencia);
            this.LiveHelpTab.Controls.Add(this.Rules);
            this.LiveHelpTab.Controls.Add(this.label4);
            this.LiveHelpTab.Controls.Add(this.label3);
            this.LiveHelpTab.Controls.Add(this.label2);
            this.LiveHelpTab.Controls.Add(this.label1);
            this.LiveHelpTab.Controls.Add(this.BotonAlerta);
            this.LiveHelpTab.Controls.Add(this.ConnectButton);
            this.LiveHelpTab.Controls.Add(this.TypeHereLabel);
            this.LiveHelpTab.Controls.Add(this.ChatMessageBox);
            this.LiveHelpTab.Controls.Add(this.ChatUsersBox);
            this.LiveHelpTab.Controls.Add(this.ChatDisplayBox);
            this.LiveHelpTab.Location = new System.Drawing.Point(4, 22);
            this.LiveHelpTab.Name = "LiveHelpTab";
            this.LiveHelpTab.Size = new System.Drawing.Size(365, 334);
            this.LiveHelpTab.TabIndex = 2;
            this.LiveHelpTab.Text = "Live Help";
            this.LiveHelpTab.UseVisualStyleBackColor = true;
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisconnectButton.ForeColor = System.Drawing.Color.Maroon;
            this.DisconnectButton.Location = new System.Drawing.Point(248, 270);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(105, 58);
            this.DisconnectButton.TabIndex = 17;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Visible = false;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // PleaseWaitLabel
            // 
            this.PleaseWaitLabel.AutoSize = true;
            this.PleaseWaitLabel.BackColor = System.Drawing.Color.Transparent;
            this.PleaseWaitLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PleaseWaitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PleaseWaitLabel.ForeColor = System.Drawing.Color.Orange;
            this.PleaseWaitLabel.Location = new System.Drawing.Point(234, 304);
            this.PleaseWaitLabel.Name = "PleaseWaitLabel";
            this.PleaseWaitLabel.Size = new System.Drawing.Size(119, 24);
            this.PleaseWaitLabel.TabIndex = 7;
            this.PleaseWaitLabel.Text = "Please wait...";
            this.PleaseWaitLabel.Visible = false;
            // 
            // Advertencia
            // 
            this.Advertencia.Image = global::MadCow.Properties.Resources.warning;
            this.Advertencia.Location = new System.Drawing.Point(271, 170);
            this.Advertencia.Name = "Advertencia";
            this.Advertencia.Size = new System.Drawing.Size(51, 49);
            this.Advertencia.TabIndex = 16;
            this.Advertencia.TabStop = false;
            // 
            // Rules
            // 
            this.Rules.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Rules.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rules.Location = new System.Drawing.Point(11, 122);
            this.Rules.Multiline = true;
            this.Rules.Name = "Rules";
            this.Rules.Size = new System.Drawing.Size(335, 122);
            this.Rules.TabIndex = 15;
            this.Rules.Text = "Rules:\r\nRead before asking; http://goo.gl/zyIKu\r\nFAQ/wiki: http://goo.gl/K5tDU\r\nN" +
    "o Spam\r\nNo Racism\r\nNo Stupid questions\r\nNo  Public servers posting\r\nAsking for a" +
    " \'crack\' == BAN.";
            this.Rules.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(105, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(241, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Someone might be able to resolve your questions.\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(239, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "clic Connect to proceed to IRC Mooege channel.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "or your getting errors in Mooege or Madcow,";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "If you still haven\'t figured out how MadCow works";
            // 
            // BotonAlerta
            // 
            this.BotonAlerta.Image = global::MadCow.Properties.Resources.help_button2;
            this.BotonAlerta.Location = new System.Drawing.Point(14, 15);
            this.BotonAlerta.Name = "BotonAlerta";
            this.BotonAlerta.Size = new System.Drawing.Size(77, 77);
            this.BotonAlerta.TabIndex = 10;
            this.BotonAlerta.TabStop = false;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Enabled = false;
            this.ConnectButton.Location = new System.Drawing.Point(14, 286);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(339, 42);
            this.ConnectButton.TabIndex = 9;
            this.ConnectButton.Text = "Connect (DISABLED TILL BUG FIXING)";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // TypeHereLabel
            // 
            this.TypeHereLabel.AutoSize = true;
            this.TypeHereLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TypeHereLabel.ForeColor = System.Drawing.Color.DimGray;
            this.TypeHereLabel.Location = new System.Drawing.Point(11, 252);
            this.TypeHereLabel.Name = "TypeHereLabel";
            this.TypeHereLabel.Size = new System.Drawing.Size(70, 13);
            this.TypeHereLabel.TabIndex = 8;
            this.TypeHereLabel.Text = "Type Here;";
            this.TypeHereLabel.Visible = false;
            // 
            // ChatMessageBox
            // 
            this.ChatMessageBox.AcceptsTab = true;
            this.ChatMessageBox.BackColor = System.Drawing.Color.White;
            this.ChatMessageBox.Location = new System.Drawing.Point(14, 270);
            this.ChatMessageBox.Multiline = true;
            this.ChatMessageBox.Name = "ChatMessageBox";
            this.ChatMessageBox.Size = new System.Drawing.Size(228, 58);
            this.ChatMessageBox.TabIndex = 6;
            this.ChatMessageBox.Visible = false;
            this.ChatMessageBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            this.ChatMessageBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // ChatUsersBox
            // 
            this.ChatUsersBox.BackColor = System.Drawing.Color.White;
            this.ChatUsersBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ChatUsersBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChatUsersBox.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.ChatUsersBox.HideSelection = false;
            this.ChatUsersBox.Location = new System.Drawing.Point(248, 15);
            this.ChatUsersBox.Multiline = true;
            this.ChatUsersBox.Name = "ChatUsersBox";
            this.ChatUsersBox.ReadOnly = true;
            this.ChatUsersBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatUsersBox.Size = new System.Drawing.Size(105, 219);
            this.ChatUsersBox.TabIndex = 5;
            this.ChatUsersBox.Visible = false;
            this.ChatUsersBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textBox3_MouseMove);
            // 
            // ChatDisplayBox
            // 
            this.ChatDisplayBox.BackColor = System.Drawing.Color.White;
            this.ChatDisplayBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.ChatDisplayBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChatDisplayBox.ForeColor = System.Drawing.Color.Black;
            this.ChatDisplayBox.HideSelection = false;
            this.ChatDisplayBox.Location = new System.Drawing.Point(14, 15);
            this.ChatDisplayBox.MaxLength = 250;
            this.ChatDisplayBox.Multiline = true;
            this.ChatDisplayBox.Name = "ChatDisplayBox";
            this.ChatDisplayBox.ReadOnly = true;
            this.ChatDisplayBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatDisplayBox.Size = new System.Drawing.Size(228, 219);
            this.ChatDisplayBox.TabIndex = 1;
            this.ChatDisplayBox.Visible = false;
            // 
            // ChangelogDownloader
            // 
            this.ChangelogDownloader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker6_DoWork);
            // 
            // MadCowTrayIcon
            // 
            this.MadCowTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("MadCowTrayIcon.Icon")));
            this.MadCowTrayIcon.Text = "MadCow";
            this.MadCowTrayIcon.Visible = true;
            this.MadCowTrayIcon.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // MpqPathBrowserDialog
            // 
            this.MpqPathBrowserDialog.Description = "Select the MPQ destination.";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(736, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.madCowToolStripMenuItem,
            this.mooegeToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // madCowToolStripMenuItem
            // 
            this.madCowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableTrayToolStripMenuItem,
            this.enableTrayNotificationsToolStripMenuItem,
            this.rememberLastRepositoryToolStripMenuItem,
            this.desktopShortcutToolStripMenuItem,
            this.compileAsDebugToolStripMenuItem});
            this.madCowToolStripMenuItem.Name = "madCowToolStripMenuItem";
            this.madCowToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.madCowToolStripMenuItem.Text = "Mad&Cow";
            // 
            // enableTrayToolStripMenuItem
            // 
            this.enableTrayToolStripMenuItem.CheckOnClick = true;
            this.enableTrayToolStripMenuItem.Name = "enableTrayToolStripMenuItem";
            this.enableTrayToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.enableTrayToolStripMenuItem.Text = "Enable Tray";
            this.enableTrayToolStripMenuItem.CheckedChanged += new System.EventHandler(this.enableTrayToolStripMenuItem_CheckedChanged);
            // 
            // enableTrayNotificationsToolStripMenuItem
            // 
            this.enableTrayNotificationsToolStripMenuItem.CheckOnClick = true;
            this.enableTrayNotificationsToolStripMenuItem.Name = "enableTrayNotificationsToolStripMenuItem";
            this.enableTrayNotificationsToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.enableTrayNotificationsToolStripMenuItem.Text = "Enable Tray Notifications";
            this.enableTrayNotificationsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.enableTrayNotificationsToolStripMenuItem_CheckedChanged);
            // 
            // rememberLastRepositoryToolStripMenuItem
            // 
            this.rememberLastRepositoryToolStripMenuItem.CheckOnClick = true;
            this.rememberLastRepositoryToolStripMenuItem.Name = "rememberLastRepositoryToolStripMenuItem";
            this.rememberLastRepositoryToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.rememberLastRepositoryToolStripMenuItem.Text = "Remember Last Repository";
            this.rememberLastRepositoryToolStripMenuItem.CheckedChanged += new System.EventHandler(this.rememberLastRepositoryToolStripMenuItem_CheckedChanged);
            // 
            // desktopShortcutToolStripMenuItem
            // 
            this.desktopShortcutToolStripMenuItem.CheckOnClick = true;
            this.desktopShortcutToolStripMenuItem.Name = "desktopShortcutToolStripMenuItem";
            this.desktopShortcutToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.desktopShortcutToolStripMenuItem.Text = "Desktop Shortcut";
            this.desktopShortcutToolStripMenuItem.CheckedChanged += new System.EventHandler(this.desktopShortcutToolStripMenuItem_CheckedChanged);
            // 
            // mooegeToolStripMenuItem
            // 
            this.mooegeToolStripMenuItem.Enabled = false;
            this.mooegeToolStripMenuItem.Name = "mooegeToolStripMenuItem";
            this.mooegeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mooegeToolStripMenuItem.Text = "&Mooege";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // compileAsDebugToolStripMenuItem
            // 
            this.compileAsDebugToolStripMenuItem.CheckOnClick = true;
            this.compileAsDebugToolStripMenuItem.Name = "compileAsDebugToolStripMenuItem";
            this.compileAsDebugToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.compileAsDebugToolStripMenuItem.Text = "Compile Mooege as Debug";
            this.compileAsDebugToolStripMenuItem.Click += new System.EventHandler(this.compileAsDebugToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 389);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.Tabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MadCow By Wesko";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.AutoUpdateValue)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.RemoteServerTab.ResumeLayout(false);
            this.RemoteServerTab.PerformLayout();
            this.RemoteServerGrpBox.ResumeLayout(false);
            this.RemoteServerGrpBox.PerformLayout();
            this.ServerControlTab.ResumeLayout(false);
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
            this.UpdatesTab.ResumeLayout(false);
            this.UpdatesTab.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.Tabs.ResumeLayout(false);
            this.MooegeTab.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.HelpTab.ResumeLayout(false);
            this.HelpTab.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.OutPutTab.ResumeLayout(false);
            this.OutPutTab.PerformLayout();
            this.ChaneglogTab.ResumeLayout(false);
            this.ChaneglogTab.PerformLayout();
            this.LiveHelpTab.ResumeLayout(false);
            this.LiveHelpTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Advertencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BotonAlerta)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox EnableAutoUpdateBox;
        private System.Windows.Forms.NumericUpDown AutoUpdateValue;
        private System.Windows.Forms.Button LaunchServerButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button RemoteServerLaunchButton;
        private System.Windows.Forms.TabPage RemoteServerTab;
        private System.Windows.Forms.TabPage ServerControlTab;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.LinkLabel RestoreDefaultsLabel;
        private System.Windows.Forms.CheckBox NATcheckBox;
        private System.Windows.Forms.Label PublicServerIpLabel;
        private System.Windows.Forms.TextBox PublicServerIp;
        private System.Windows.Forms.Label GameServerPortLabel;
        private System.Windows.Forms.Label GameServerIpLabel;
        private System.Windows.Forms.TextBox GameServerPort;
        private System.Windows.Forms.TextBox GameServerIp;
        private System.Windows.Forms.Label BnetServerPortLabel;
        private System.Windows.Forms.Label BnetServerIpLabel;
        private System.Windows.Forms.TextBox BnetServerPort;
        private System.Windows.Forms.TextBox BnetServerIp;
        private System.Windows.Forms.GroupBox RemoteServerGrpBox;
        private System.Windows.Forms.Label RemotePort;
        private System.Windows.Forms.Label RemoteHost;
        private System.Windows.Forms.TextBox remotePortTxtBox;
        private System.Windows.Forms.TextBox remoteHostTxtBox;
        private System.Windows.Forms.Timer DownloadSpeedTimer;
        private System.Windows.Forms.Label RemoteServerTip2;
        private System.Windows.Forms.TabPage HelpTab;
        private System.Windows.Forms.Button FindDiabloButton;
        private System.Windows.Forms.TextBox Diablo3UserPathSelection;
        private System.Windows.Forms.Button ValidateRepoButton;
        private System.Windows.Forms.OpenFileDialog DiabloPathFileDialog;
        private System.Windows.Forms.Label RepositoryHintLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button UpdateMooegeButton;
        private System.ComponentModel.BackgroundWorker DownloadRepository;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ProgressBar DownloadRepoBar;
        private System.Windows.Forms.ToolTip ToolTips;
        private System.Windows.Forms.Label AutoUpdateTimerLabel;
        public System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ResetRepoFolder;
        private System.Windows.Forms.Label ResetMooegeReposLabel;
        private System.Windows.Forms.Label RemoteServerTip;
        private System.Windows.Forms.Label MotdLabel;
        private System.Windows.Forms.TextBox MotdTxtBox;
        private System.ComponentModel.BackgroundWorker VerifyDiablo3Version;
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
        private System.Windows.Forms.Label DownloadMpqLabel;
        private System.Windows.Forms.Button DownloadMPQSButton;
        public System.Windows.Forms.ProgressBar DownloadMPQSprogressBar;
        private System.Windows.Forms.Label DownloadFileNameLabel;
        private System.Windows.Forms.Label DownloadSpeedLabel;
        private System.ComponentModel.BackgroundWorker ErrorFilesDownloaders;
        private System.ComponentModel.BackgroundWorker ValidateRepository;
        public System.Windows.Forms.TextBox ConsoleOutputTxtBox;
        public System.Windows.Forms.ProgressBar generalProgressBar;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage OutPutTab;
        private System.Windows.Forms.TabPage ChaneglogTab;
        private System.Windows.Forms.TextBox ChangeLogTxtBox;
        private System.Windows.Forms.Label SelectRepositoryChngLogLabel;
        private System.Windows.Forms.ComboBox SelectRepoChngLogComboBox;
        private System.ComponentModel.BackgroundWorker ChangelogDownloader;
        public System.Windows.Forms.NotifyIcon MadCowTrayIcon;
        public System.Windows.Forms.Button PlayDiabloButton;
        public System.Windows.Forms.Button CopyMPQButton;
        public System.Windows.Forms.ComboBox BranchComboBox;
        public System.Windows.Forms.ComboBox comboBox1;
        public System.ComponentModel.BackgroundWorker DownloadSelectedMpqs;
        private System.Windows.Forms.Label BranchSelectionLabel;
        public System.Windows.Forms.Label LastPlayedRepoReminderLabel;
        public System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage UpdatesTab;
        private System.Windows.Forms.Button UpdateMadcowButton;
        private System.Windows.Forms.Label AutoUpdateMadcowLabel;
        private System.Windows.Forms.FolderBrowserDialog MpqPathBrowserDialog;
        private System.Windows.Forms.TabPage MooegeTab;
        private System.Windows.Forms.GroupBox groupBox9;
        internal System.Windows.Forms.CheckedListBox SettingsCheckedListBox;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button BrowseMPQPathButton;
        public System.Windows.Forms.TextBox MPQDestTextBox;
        public System.Windows.Forms.Label mpqDestinationDisableLabel;
        private System.Windows.Forms.LinkLabel DownloadNetLabel;
        private System.Windows.Forms.Label ProblemsCompilingLabel;
        private System.Windows.Forms.TabPage LiveHelpTab;
        public System.Windows.Forms.TextBox ChatDisplayBox;
        public System.Windows.Forms.TextBox ChatUsersBox;
        public System.Windows.Forms.TextBox ChatMessageBox;
        public System.Windows.Forms.Label PleaseWaitLabel;
        public System.Windows.Forms.Label TypeHereLabel;
        public System.Windows.Forms.Button ConnectButton;
        public System.Windows.Forms.PictureBox BotonAlerta;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox Rules;
        public System.Windows.Forms.PictureBox Advertencia;
        public System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem madCowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableTrayNotificationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rememberLastRepositoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desktopShortcutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mooegeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compileAsDebugToolStripMenuItem;

    }
}