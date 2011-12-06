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
            this.label18 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.UpdateMooegeServerButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.ValidateMPQButton = new System.Windows.Forms.Button();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RedownloadMPQButton = new System.Windows.Forms.Button();
            this.ResetRepoFolder = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            txtConsole = new System.Windows.Forms.TextBox();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.AutoUpdateValue)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // EnableAutoUpdateBox
            // 
            this.EnableAutoUpdateBox.AutoSize = true;
            this.EnableAutoUpdateBox.Location = new System.Drawing.Point(16, 45);
            this.EnableAutoUpdateBox.Name = "EnableAutoUpdateBox";
            this.EnableAutoUpdateBox.Size = new System.Drawing.Size(59, 17);
            this.EnableAutoUpdateBox.TabIndex = 3;
            this.EnableAutoUpdateBox.Text = "Enable";
            this.EnableAutoUpdateBox.UseVisualStyleBackColor = true;
            this.EnableAutoUpdateBox.CheckedChanged += new System.EventHandler(this.AutoUpdate_CheckedChanged);
            // 
            // AutoUpdateValue
            // 
            this.AutoUpdateValue.Location = new System.Drawing.Point(16, 19);
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
            this.PlayDiabloButton.Location = new System.Drawing.Point(16, 306);
            this.PlayDiabloButton.Name = "PlayDiabloButton";
            this.PlayDiabloButton.Size = new System.Drawing.Size(94, 43);
            this.PlayDiabloButton.TabIndex = 7;
            this.PlayDiabloButton.Text = "Play Diablo";
            this.PlayDiabloButton.UseVisualStyleBackColor = false;
            this.PlayDiabloButton.Click += new System.EventHandler(this.PlayDiablo_Click);
            // 
            // LaunchServerButton
            // 
            this.LaunchServerButton.Location = new System.Drawing.Point(195, 178);
            this.LaunchServerButton.Name = "LaunchServerButton";
            this.LaunchServerButton.Size = new System.Drawing.Size(100, 23);
            this.LaunchServerButton.TabIndex = 9;
            this.LaunchServerButton.Text = "Launch Server";
            this.LaunchServerButton.UseVisualStyleBackColor = true;
            this.LaunchServerButton.Click += new System.EventHandler(this.LaunchServer_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.AutoUpdateValue);
            this.groupBox2.Controls.Add(this.EnableAutoUpdateBox);
            this.groupBox2.Location = new System.Drawing.Point(229, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(112, 68);
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
            this.tabPage3.Size = new System.Drawing.Size(345, 278);
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
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(39, 61);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(264, 156);
            this.label12.TabIndex = 1;
            this.label12.Text = resources.GetString("label12.Text");
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.UpdateMooegeServerButton);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.LaunchServerButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(345, 278);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Server Control";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 233);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 13);
            this.label18.TabIndex = 13;
            this.label18.Text = "MOTD";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(9, 252);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(330, 20);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = "Welcome to mooege development server!";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(44, 218);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(242, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "To Activate Validate Repository(Will be Changing)";
            // 
            // UpdateMooegeServerButton
            // 
            this.UpdateMooegeServerButton.Location = new System.Drawing.Point(38, 178);
            this.UpdateMooegeServerButton.Name = "UpdateMooegeServerButton";
            this.UpdateMooegeServerButton.Size = new System.Drawing.Size(128, 23);
            this.UpdateMooegeServerButton.TabIndex = 10;
            this.UpdateMooegeServerButton.Text = "Update Mooege Server";
            this.UpdateMooegeServerButton.UseVisualStyleBackColor = true;
            this.UpdateMooegeServerButton.Click += new System.EventHandler(this.UpdateMooegeServer_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.linkLabel1);
            this.groupBox5.Controls.Add(this.linkLabel2);
            this.groupBox5.Controls.Add(this.checkBox3);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.textBox9);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.textBox10);
            this.groupBox5.Controls.Add(this.textBox11);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.textBox12);
            this.groupBox5.Controls.Add(this.textBox13);
            this.groupBox5.Location = new System.Drawing.Point(3, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(339, 166);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Local Server";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.DimGray;
            this.linkLabel1.Location = new System.Drawing.Point(143, 143);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(99, 13);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Refresh from config";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RefreshFromConfig_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkColor = System.Drawing.Color.DimGray;
            this.linkLabel2.Location = new System.Drawing.Point(242, 143);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(86, 13);
            this.linkLabel2.TabIndex = 11;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Restore Defaults";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RestoreDefault_LinkClicked);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Enabled = false;
            this.checkBox3.Location = new System.Drawing.Point(12, 139);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(90, 17);
            this.checkBox3.TabIndex = 10;
            this.checkBox3.Text = "NAT Enabled";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 120);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "Public Server IP:";
            // 
            // textBox9
            // 
            this.textBox9.Enabled = false;
            this.textBox9.Location = new System.Drawing.Point(96, 117);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(196, 20);
            this.textBox9.TabIndex = 8;
            this.textBox9.TextChanged += new System.EventHandler(this.textBox9_TextChanged);
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
            // textBox10
            // 
            this.textBox10.Enabled = false;
            this.textBox10.Location = new System.Drawing.Point(230, 81);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(100, 20);
            this.textBox10.TabIndex = 5;
            this.textBox10.TextChanged += new System.EventHandler(this.textBox10_TextChanged);
            // 
            // textBox11
            // 
            this.textBox11.Enabled = false;
            this.textBox11.Location = new System.Drawing.Point(11, 82);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(196, 20);
            this.textBox11.TabIndex = 4;
            this.textBox11.TextChanged += new System.EventHandler(this.textBox11_TextChanged);
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
            // textBox12
            // 
            this.textBox12.Enabled = false;
            this.textBox12.Location = new System.Drawing.Point(228, 31);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(100, 20);
            this.textBox12.TabIndex = 1;
            this.textBox12.TextChanged += new System.EventHandler(this.textBox12_TextChanged);
            // 
            // textBox13
            // 
            this.textBox13.Enabled = false;
            this.textBox13.Location = new System.Drawing.Point(11, 31);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(196, 20);
            this.textBox13.TabIndex = 0;
            this.textBox13.TextChanged += new System.EventHandler(this.textBox13_TextChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.FindDiabloButton);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.ValidateRepoButton);
            this.tabPage1.Controls.Add(this.Diablo3UserPathSelection);
            this.tabPage1.Controls.Add(this.UpdateMooegeButton);
            this.tabPage1.Controls.Add(this.CopyMPQButton);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(345, 278);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Updates";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 68);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Repository";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "https://github.com/mooege/mooege",
            "https://github.com/Egris/mooege",
            "https://github.com/DarkLotus/mooege",
            "https://github.com/angerwin/d3sharp",
            "https://github.com/Farmy/mooege",
            "https://github.com/mdz444/mooege"});
            this.comboBox1.Location = new System.Drawing.Point(3, 18);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(188, 21);
            this.comboBox1.TabIndex = 17;
            this.comboBox1.Text = "https://github.com/mooege/mooege";
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(194, 19);
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
            this.pictureBox1.Location = new System.Drawing.Point(194, 19);
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
            this.label4.Location = new System.Drawing.Point(0, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 26);
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
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Location = new System.Drawing.Point(9, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(316, 17);
            this.panel2.TabIndex = 17;
            // 
            // progressBar1
            // 
            this.progressBar1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.progressBar1.Location = new System.Drawing.Point(5, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(308, 11);
            this.progressBar1.Step = 20;
            this.progressBar1.TabIndex = 0;
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
            this.tabControl1.Size = new System.Drawing.Size(353, 304);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label19);
            this.tabPage4.Controls.Add(this.ValidateMPQButton);
            this.tabPage4.Controls.Add(this.progressBar3);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.RedownloadMPQButton);
            this.tabPage4.Controls.Add(this.ResetRepoFolder);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(345, 278);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Help";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(31, 114);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(151, 26);
            this.label19.TabIndex = 8;
            this.label19.Text = "Checks MD5 sums from a pool\r\nTesting Purposes";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ValidateMPQButton
            // 
            this.ValidateMPQButton.Enabled = false;
            this.ValidateMPQButton.Location = new System.Drawing.Point(236, 109);
            this.ValidateMPQButton.Name = "ValidateMPQButton";
            this.ValidateMPQButton.Size = new System.Drawing.Size(90, 23);
            this.ValidateMPQButton.TabIndex = 7;
            this.ValidateMPQButton.Text = "Validate MPQs";
            this.ValidateMPQButton.UseVisualStyleBackColor = true;
            this.ValidateMPQButton.Click += new System.EventHandler(this.ValidateMPQs_Click);
            // 
            // progressBar3
            // 
            this.progressBar3.Location = new System.Drawing.Point(230, 80);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(100, 13);
            this.progressBar3.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Reset Mooege Repository Folder";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 39);
            this.label3.TabIndex = 4;
            this.label3.Text = "Did Mooege receive\r\n\"Couldn\'t Find Catalog File: CoreToc.Dat\" \r\nError?";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RedownloadMPQButton
            // 
            this.RedownloadMPQButton.Enabled = false;
            this.RedownloadMPQButton.Location = new System.Drawing.Point(229, 40);
            this.RedownloadMPQButton.Name = "RedownloadMPQButton";
            this.RedownloadMPQButton.Size = new System.Drawing.Size(104, 34);
            this.RedownloadMPQButton.TabIndex = 3;
            this.RedownloadMPQButton.Text = "Redownload 7841 MPQ";
            this.RedownloadMPQButton.UseVisualStyleBackColor = true;
            this.RedownloadMPQButton.Click += new System.EventHandler(this.ReDownloadMPQ_Click);
            // 
            // ResetRepoFolder
            // 
            this.ResetRepoFolder.Location = new System.Drawing.Point(236, 6);
            this.ResetRepoFolder.Name = "ResetRepoFolder";
            this.ResetRepoFolder.Size = new System.Drawing.Size(90, 23);
            this.ResetRepoFolder.TabIndex = 2;
            this.ResetRepoFolder.Text = "Reset Folder";
            this.ResetRepoFolder.UseVisualStyleBackColor = true;
            this.ResetRepoFolder.Click += new System.EventHandler(this.ResetRepoFolder_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label12);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(345, 278);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "Credits";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 14;
            this.label1.UseMnemonic = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 333);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 15;
            // 
            // txtConsole
            // 
            txtConsole.Location = new System.Drawing.Point(372, 22);
            txtConsole.Multiline = true;
            txtConsole.Name = "txtConsole";
            txtConsole.ReadOnly = true;
            txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtConsole.Size = new System.Drawing.Size(334, 282);
            txtConsole.TabIndex = 16;
            txtConsole.TextChanged += new System.EventHandler(txtConsole_TextChanged);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 352);
            this.Controls.Add(txtConsole);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.PlayDiabloButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MadCow By Wesko";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AutoUpdateValue)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
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
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox EnableAutoUpdateBox;
        private System.Windows.Forms.NumericUpDown AutoUpdateValue;
        private System.Windows.Forms.Button PlayDiabloButton;
        private System.Windows.Forms.Button LaunchServerButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button RemoteServerButton;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button UpdateMooegeServerButton;
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
        private System.Windows.Forms.Button CopyMPQButton;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button RedownloadMPQButton;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ProgressBar progressBar3;
        public System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button ValidateMPQButton;
        private System.Windows.Forms.LinkLabel linkLabel1;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        public static System.Windows.Forms.TextBox txtConsole;

    }
}