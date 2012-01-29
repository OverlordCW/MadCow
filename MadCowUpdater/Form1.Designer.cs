namespace MadCowUpdater
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
            this.NoUpdateLabel = new System.Windows.Forms.Label();
            this.UpdateFound = new System.Windows.Forms.Label();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.UpdateComplete = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.UpdatingLabel = new System.Windows.Forms.Label();
            this.DownloadingLabel = new System.Windows.Forms.Label();
            this.UncompressingLabel = new System.Windows.Forms.Label();
            this.CompilingLabel = new System.Windows.Forms.Label();
            this.CopyingLabel = new System.Windows.Forms.Label();
            this.CopySuccessDot = new System.Windows.Forms.PictureBox();
            this.CompilingSuccessDot = new System.Windows.Forms.PictureBox();
            this.UncompressSuccessDot = new System.Windows.Forms.PictureBox();
            this.DownloadSuccessDot = new System.Windows.Forms.PictureBox();
            this.NoUpdateCross = new System.Windows.Forms.PictureBox();
            this.UpdateFoundTick = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CopySuccessDot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompilingSuccessDot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UncompressSuccessDot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadSuccessDot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NoUpdateCross)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateFoundTick)).BeginInit();
            this.SuspendLayout();
            // 
            // NoUpdateLabel
            // 
            resources.ApplyResources(this.NoUpdateLabel, "NoUpdateLabel");
            this.NoUpdateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.NoUpdateLabel.Name = "NoUpdateLabel";
            // 
            // UpdateFound
            // 
            resources.ApplyResources(this.UpdateFound, "UpdateFound");
            this.UpdateFound.ForeColor = System.Drawing.Color.Green;
            this.UpdateFound.Name = "UpdateFound";
            // 
            // UpdateButton
            // 
            resources.ApplyResources(this.UpdateButton, "UpdateButton");
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // UpdateComplete
            // 
            resources.ApplyResources(this.UpdateComplete, "UpdateComplete");
            this.UpdateComplete.ForeColor = System.Drawing.Color.ForestGreen;
            this.UpdateComplete.Name = "UpdateComplete";
            // 
            // timer1
            // 
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // SearchLabel
            // 
            resources.ApplyResources(this.SearchLabel, "SearchLabel");
            this.SearchLabel.Name = "SearchLabel";
            // 
            // UpdatingLabel
            // 
            resources.ApplyResources(this.UpdatingLabel, "UpdatingLabel");
            this.UpdatingLabel.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.UpdatingLabel.Name = "UpdatingLabel";
            // 
            // DownloadingLabel
            // 
            resources.ApplyResources(this.DownloadingLabel, "DownloadingLabel");
            this.DownloadingLabel.Name = "DownloadingLabel";
            // 
            // UncompressingLabel
            // 
            resources.ApplyResources(this.UncompressingLabel, "UncompressingLabel");
            this.UncompressingLabel.Name = "UncompressingLabel";
            // 
            // CompilingLabel
            // 
            resources.ApplyResources(this.CompilingLabel, "CompilingLabel");
            this.CompilingLabel.Name = "CompilingLabel";
            // 
            // CopyingLabel
            // 
            resources.ApplyResources(this.CopyingLabel, "CopyingLabel");
            this.CopyingLabel.Name = "CopyingLabel";
            // 
            // CopySuccessDot
            // 
            this.CopySuccessDot.Image = global::MadCowUpdater.Properties.Resources.green_dot;
            resources.ApplyResources(this.CopySuccessDot, "CopySuccessDot");
            this.CopySuccessDot.Name = "CopySuccessDot";
            this.CopySuccessDot.TabStop = false;
            // 
            // CompilingSuccessDot
            // 
            this.CompilingSuccessDot.Image = global::MadCowUpdater.Properties.Resources.green_dot;
            resources.ApplyResources(this.CompilingSuccessDot, "CompilingSuccessDot");
            this.CompilingSuccessDot.Name = "CompilingSuccessDot";
            this.CompilingSuccessDot.TabStop = false;
            // 
            // UncompressSuccessDot
            // 
            this.UncompressSuccessDot.Image = global::MadCowUpdater.Properties.Resources.green_dot;
            resources.ApplyResources(this.UncompressSuccessDot, "UncompressSuccessDot");
            this.UncompressSuccessDot.Name = "UncompressSuccessDot";
            this.UncompressSuccessDot.TabStop = false;
            // 
            // DownloadSuccessDot
            // 
            this.DownloadSuccessDot.Image = global::MadCowUpdater.Properties.Resources.green_dot;
            resources.ApplyResources(this.DownloadSuccessDot, "DownloadSuccessDot");
            this.DownloadSuccessDot.Name = "DownloadSuccessDot";
            this.DownloadSuccessDot.TabStop = false;
            // 
            // NoUpdateCross
            // 
            this.NoUpdateCross.Image = global::MadCowUpdater.Properties.Resources.cross_error;
            resources.ApplyResources(this.NoUpdateCross, "NoUpdateCross");
            this.NoUpdateCross.Name = "NoUpdateCross";
            this.NoUpdateCross.TabStop = false;
            // 
            // UpdateFoundTick
            // 
            this.UpdateFoundTick.Image = global::MadCowUpdater.Properties.Resources.green_tick;
            resources.ApplyResources(this.UpdateFoundTick, "UpdateFoundTick");
            this.UpdateFoundTick.Name = "UpdateFoundTick";
            this.UpdateFoundTick.TabStop = false;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.CopySuccessDot);
            this.Controls.Add(this.CompilingSuccessDot);
            this.Controls.Add(this.UncompressSuccessDot);
            this.Controls.Add(this.DownloadSuccessDot);
            this.Controls.Add(this.CopyingLabel);
            this.Controls.Add(this.CompilingLabel);
            this.Controls.Add(this.UncompressingLabel);
            this.Controls.Add(this.DownloadingLabel);
            this.Controls.Add(this.UpdateComplete);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.UpdateFoundTick);
            this.Controls.Add(this.UpdatingLabel);
            this.Controls.Add(this.NoUpdateCross);
            this.Controls.Add(this.SearchLabel);
            this.Controls.Add(this.UpdateFound);
            this.Controls.Add(this.NoUpdateLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CopySuccessDot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompilingSuccessDot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UncompressSuccessDot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DownloadSuccessDot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NoUpdateCross)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateFoundTick)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        public System.Windows.Forms.Label NoUpdateLabel;
        public System.Windows.Forms.Label UpdateFound;
        public System.Windows.Forms.PictureBox UpdateFoundTick;
        public System.Windows.Forms.Button UpdateButton;
        public System.Windows.Forms.PictureBox NoUpdateCross;
        public System.Windows.Forms.Label UpdateComplete;
        private System.Windows.Forms.Label SearchLabel;
        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Label UpdatingLabel;
        public System.Windows.Forms.Label DownloadingLabel;
        public System.Windows.Forms.Label UncompressingLabel;
        public System.Windows.Forms.Label CompilingLabel;
        public System.Windows.Forms.Label CopyingLabel;
        public System.Windows.Forms.PictureBox DownloadSuccessDot;
        public System.Windows.Forms.PictureBox UncompressSuccessDot;
        public System.Windows.Forms.PictureBox CompilingSuccessDot;
        public System.Windows.Forms.PictureBox CopySuccessDot;


    }
}

