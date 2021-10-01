﻿namespace SevenZipTestForms
{
    partial class FormMain
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
            this.pb_CompressProgress = new System.Windows.Forms.ProgressBar();
            this.pb_CompressWork = new System.Windows.Forms.ProgressBar();
            this.b_Compress = new System.Windows.Forms.Button();
            this.l_CompressProgress = new System.Windows.Forms.Label();
            this.tb_CompressDirectory = new System.Windows.Forms.TextBox();
            this.l_CompressDirectory = new System.Windows.Forms.Label();
            this.b_Browse = new System.Windows.Forms.Button();
            this.gb_Settings = new System.Windows.Forms.GroupBox();
            this.chb_Volumes = new System.Windows.Forms.CheckBox();
            this.nup_VolumeSize = new System.Windows.Forms.NumericUpDown();
            this.l_Volumes = new System.Windows.Forms.Label();
            this.chb_Sfx = new System.Windows.Forms.CheckBox();
            this.l_Method = new System.Windows.Forms.Label();
            this.cb_Method = new System.Windows.Forms.ComboBox();
            this.l_CompressionLevel = new System.Windows.Forms.Label();
            this.trb_Level = new System.Windows.Forms.TrackBar();
            this.l_Format = new System.Windows.Forms.Label();
            this.cb_Format = new System.Windows.Forms.ComboBox();
            this.fbd_Directory = new System.Windows.Forms.FolderBrowserDialog();
            this.b_BrowseOut = new System.Windows.Forms.Button();
            this.l_CompressOutput = new System.Windows.Forms.Label();
            this.tb_CompressOutput = new System.Windows.Forms.TextBox();
            this.sfd_Archive = new System.Windows.Forms.SaveFileDialog();
            this.tbc_Main = new System.Windows.Forms.TabControl();
            this.tp_Extract = new System.Windows.Forms.TabPage();
            this.buttonToStreamDic = new System.Windows.Forms.Button();
            this.tb_Messages = new System.Windows.Forms.TextBox();
            this.pb_ExtractProgress = new System.Windows.Forms.ProgressBar();
            this.b_ExtractBrowseArchive = new System.Windows.Forms.Button();
            this.pb_ExtractWork = new System.Windows.Forms.ProgressBar();
            this.l_ExtractArchiveName = new System.Windows.Forms.Label();
            this.b_Extract = new System.Windows.Forms.Button();
            this.tb_ExtractArchive = new System.Windows.Forms.TextBox();
            this.l_ExtractProgress = new System.Windows.Forms.Label();
            this.tb_ExtractDirectory = new System.Windows.Forms.TextBox();
            this.b_ExtractBrowseDirectory = new System.Windows.Forms.Button();
            this.l_ExtractDirectory = new System.Windows.Forms.Label();
            this.tp_Compress = new System.Windows.Forms.TabPage();
            this.ofd_Archive = new System.Windows.Forms.OpenFileDialog();
            this.gb_Settings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nup_VolumeSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trb_Level)).BeginInit();
            this.tbc_Main.SuspendLayout();
            this.tp_Extract.SuspendLayout();
            this.tp_Compress.SuspendLayout();
            this.SuspendLayout();
            // 
            // pb_CompressProgress
            // 
            this.pb_CompressProgress.Location = new System.Drawing.Point(12, 44);
            this.pb_CompressProgress.Margin = new System.Windows.Forms.Padding(6);
            this.pb_CompressProgress.Name = "pb_CompressProgress";
            this.pb_CompressProgress.Size = new System.Drawing.Size(390, 30);
            this.pb_CompressProgress.TabIndex = 0;
            // 
            // pb_CompressWork
            // 
            this.pb_CompressWork.Location = new System.Drawing.Point(12, 84);
            this.pb_CompressWork.Margin = new System.Windows.Forms.Padding(6);
            this.pb_CompressWork.MarqueeAnimationSpeed = 25;
            this.pb_CompressWork.Name = "pb_CompressWork";
            this.pb_CompressWork.Size = new System.Drawing.Size(390, 30);
            this.pb_CompressWork.TabIndex = 1;
            // 
            // b_Compress
            // 
            this.b_Compress.Location = new System.Drawing.Point(414, 44);
            this.b_Compress.Margin = new System.Windows.Forms.Padding(6);
            this.b_Compress.Name = "b_Compress";
            this.b_Compress.Size = new System.Drawing.Size(132, 70);
            this.b_Compress.TabIndex = 2;
            this.b_Compress.Text = "Compress";
            this.b_Compress.UseVisualStyleBackColor = true;
            this.b_Compress.Click += new System.EventHandler(this.b_Compress_Click);
            // 
            // l_CompressProgress
            // 
            this.l_CompressProgress.AutoSize = true;
            this.l_CompressProgress.Location = new System.Drawing.Point(12, 14);
            this.l_CompressProgress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.l_CompressProgress.Name = "l_CompressProgress";
            this.l_CompressProgress.Size = new System.Drawing.Size(106, 24);
            this.l_CompressProgress.TabIndex = 3;
            this.l_CompressProgress.Text = "Progress";
            // 
            // tb_CompressDirectory
            // 
            this.tb_CompressDirectory.Location = new System.Drawing.Point(12, 154);
            this.tb_CompressDirectory.Margin = new System.Windows.Forms.Padding(6);
            this.tb_CompressDirectory.Name = "tb_CompressDirectory";
            this.tb_CompressDirectory.Size = new System.Drawing.Size(386, 35);
            this.tb_CompressDirectory.TabIndex = 4;
            // 
            // l_CompressDirectory
            // 
            this.l_CompressDirectory.AutoSize = true;
            this.l_CompressDirectory.Location = new System.Drawing.Point(12, 124);
            this.l_CompressDirectory.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.l_CompressDirectory.Name = "l_CompressDirectory";
            this.l_CompressDirectory.Size = new System.Drawing.Size(262, 24);
            this.l_CompressDirectory.TabIndex = 5;
            this.l_CompressDirectory.Text = "Directory to compress";
            // 
            // b_Browse
            // 
            this.b_Browse.Location = new System.Drawing.Point(414, 154);
            this.b_Browse.Margin = new System.Windows.Forms.Padding(6);
            this.b_Browse.Name = "b_Browse";
            this.b_Browse.Size = new System.Drawing.Size(132, 36);
            this.b_Browse.TabIndex = 6;
            this.b_Browse.Text = "Browse";
            this.b_Browse.UseVisualStyleBackColor = true;
            this.b_Browse.Click += new System.EventHandler(this.b_Browse_Click);
            // 
            // gb_Settings
            // 
            this.gb_Settings.Controls.Add(this.chb_Volumes);
            this.gb_Settings.Controls.Add(this.nup_VolumeSize);
            this.gb_Settings.Controls.Add(this.l_Volumes);
            this.gb_Settings.Controls.Add(this.chb_Sfx);
            this.gb_Settings.Controls.Add(this.l_Method);
            this.gb_Settings.Controls.Add(this.cb_Method);
            this.gb_Settings.Controls.Add(this.l_CompressionLevel);
            this.gb_Settings.Controls.Add(this.trb_Level);
            this.gb_Settings.Controls.Add(this.l_Format);
            this.gb_Settings.Controls.Add(this.cb_Format);
            this.gb_Settings.Location = new System.Drawing.Point(12, 290);
            this.gb_Settings.Margin = new System.Windows.Forms.Padding(6);
            this.gb_Settings.Name = "gb_Settings";
            this.gb_Settings.Padding = new System.Windows.Forms.Padding(6);
            this.gb_Settings.Size = new System.Drawing.Size(534, 262);
            this.gb_Settings.TabIndex = 7;
            this.gb_Settings.TabStop = false;
            this.gb_Settings.Text = "Settings";
            // 
            // chb_Volumes
            // 
            this.chb_Volumes.AutoSize = true;
            this.chb_Volumes.Location = new System.Drawing.Point(368, 102);
            this.chb_Volumes.Margin = new System.Windows.Forms.Padding(6);
            this.chb_Volumes.Name = "chb_Volumes";
            this.chb_Volumes.Size = new System.Drawing.Size(126, 28);
            this.chb_Volumes.TabIndex = 10;
            this.chb_Volumes.Text = "Volumes";
            this.chb_Volumes.UseVisualStyleBackColor = true;
            // 
            // nup_VolumeSize
            // 
            this.nup_VolumeSize.Location = new System.Drawing.Point(368, 144);
            this.nup_VolumeSize.Margin = new System.Windows.Forms.Padding(6);
            this.nup_VolumeSize.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.nup_VolumeSize.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nup_VolumeSize.Name = "nup_VolumeSize";
            this.nup_VolumeSize.Size = new System.Drawing.Size(150, 35);
            this.nup_VolumeSize.TabIndex = 7;
            this.nup_VolumeSize.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // l_Volumes
            // 
            this.l_Volumes.AutoSize = true;
            this.l_Volumes.Location = new System.Drawing.Point(362, 190);
            this.l_Volumes.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.l_Volumes.Name = "l_Volumes";
            this.l_Volumes.Size = new System.Drawing.Size(130, 24);
            this.l_Volumes.TabIndex = 8;
            this.l_Volumes.Text = "bytes each";
            // 
            // chb_Sfx
            // 
            this.chb_Sfx.AutoSize = true;
            this.chb_Sfx.Location = new System.Drawing.Point(32, 220);
            this.chb_Sfx.Margin = new System.Windows.Forms.Padding(6);
            this.chb_Sfx.Name = "chb_Sfx";
            this.chb_Sfx.Size = new System.Drawing.Size(222, 28);
            this.chb_Sfx.TabIndex = 6;
            this.chb_Sfx.Text = "Self-extraction";
            this.chb_Sfx.UseVisualStyleBackColor = true;
            // 
            // l_Method
            // 
            this.l_Method.AutoSize = true;
            this.l_Method.Location = new System.Drawing.Point(278, 40);
            this.l_Method.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.l_Method.Name = "l_Method";
            this.l_Method.Size = new System.Drawing.Size(82, 24);
            this.l_Method.TabIndex = 5;
            this.l_Method.Text = "Method";
            // 
            // cb_Method
            // 
            this.cb_Method.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Method.FormattingEnabled = true;
            this.cb_Method.Location = new System.Drawing.Point(368, 36);
            this.cb_Method.Margin = new System.Windows.Forms.Padding(6);
            this.cb_Method.Name = "cb_Method";
            this.cb_Method.Size = new System.Drawing.Size(136, 32);
            this.cb_Method.TabIndex = 4;
            // 
            // l_CompressionLevel
            // 
            this.l_CompressionLevel.AutoSize = true;
            this.l_CompressionLevel.Location = new System.Drawing.Point(26, 102);
            this.l_CompressionLevel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.l_CompressionLevel.Name = "l_CompressionLevel";
            this.l_CompressionLevel.Size = new System.Drawing.Size(214, 24);
            this.l_CompressionLevel.TabIndex = 3;
            this.l_CompressionLevel.Text = "Compression level";
            // 
            // trb_Level
            // 
            this.trb_Level.Location = new System.Drawing.Point(24, 132);
            this.trb_Level.Margin = new System.Windows.Forms.Padding(6);
            this.trb_Level.Name = "trb_Level";
            this.trb_Level.Size = new System.Drawing.Size(306, 90);
            this.trb_Level.TabIndex = 2;
            this.trb_Level.Scroll += new System.EventHandler(this.trb_Level_Scroll);
            // 
            // l_Format
            // 
            this.l_Format.AutoSize = true;
            this.l_Format.Location = new System.Drawing.Point(18, 40);
            this.l_Format.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.l_Format.Name = "l_Format";
            this.l_Format.Size = new System.Drawing.Size(82, 24);
            this.l_Format.TabIndex = 1;
            this.l_Format.Text = "Format";
            // 
            // cb_Format
            // 
            this.cb_Format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Format.FormattingEnabled = true;
            this.cb_Format.Location = new System.Drawing.Point(108, 36);
            this.cb_Format.Margin = new System.Windows.Forms.Padding(6);
            this.cb_Format.Name = "cb_Format";
            this.cb_Format.Size = new System.Drawing.Size(150, 32);
            this.cb_Format.TabIndex = 0;
            // 
            // b_BrowseOut
            // 
            this.b_BrowseOut.Location = new System.Drawing.Point(414, 242);
            this.b_BrowseOut.Margin = new System.Windows.Forms.Padding(6);
            this.b_BrowseOut.Name = "b_BrowseOut";
            this.b_BrowseOut.Size = new System.Drawing.Size(132, 36);
            this.b_BrowseOut.TabIndex = 10;
            this.b_BrowseOut.Text = "Browse";
            this.b_BrowseOut.UseVisualStyleBackColor = true;
            this.b_BrowseOut.Click += new System.EventHandler(this.b_BrowseOut_Click);
            // 
            // l_CompressOutput
            // 
            this.l_CompressOutput.AutoSize = true;
            this.l_CompressOutput.Location = new System.Drawing.Point(12, 212);
            this.l_CompressOutput.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.l_CompressOutput.Name = "l_CompressOutput";
            this.l_CompressOutput.Size = new System.Drawing.Size(214, 24);
            this.l_CompressOutput.TabIndex = 9;
            this.l_CompressOutput.Text = "Archive file name";
            // 
            // tb_CompressOutput
            // 
            this.tb_CompressOutput.Location = new System.Drawing.Point(12, 242);
            this.tb_CompressOutput.Margin = new System.Windows.Forms.Padding(6);
            this.tb_CompressOutput.Name = "tb_CompressOutput";
            this.tb_CompressOutput.Size = new System.Drawing.Size(386, 35);
            this.tb_CompressOutput.TabIndex = 8;
            // 
            // sfd_Archive
            // 
            this.sfd_Archive.Title = "Save the archive to...";
            // 
            // tbc_Main
            // 
            this.tbc_Main.Controls.Add(this.tp_Extract);
            this.tbc_Main.Controls.Add(this.tp_Compress);
            this.tbc_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbc_Main.Location = new System.Drawing.Point(0, 0);
            this.tbc_Main.Margin = new System.Windows.Forms.Padding(6);
            this.tbc_Main.Name = "tbc_Main";
            this.tbc_Main.SelectedIndex = 0;
            this.tbc_Main.Size = new System.Drawing.Size(774, 686);
            this.tbc_Main.TabIndex = 7;
            // 
            // tp_Extract
            // 
            this.tp_Extract.Controls.Add(this.buttonToStreamDic);
            this.tp_Extract.Controls.Add(this.tb_Messages);
            this.tp_Extract.Controls.Add(this.pb_ExtractProgress);
            this.tp_Extract.Controls.Add(this.b_ExtractBrowseArchive);
            this.tp_Extract.Controls.Add(this.pb_ExtractWork);
            this.tp_Extract.Controls.Add(this.l_ExtractArchiveName);
            this.tp_Extract.Controls.Add(this.b_Extract);
            this.tp_Extract.Controls.Add(this.tb_ExtractArchive);
            this.tp_Extract.Controls.Add(this.l_ExtractProgress);
            this.tp_Extract.Controls.Add(this.tb_ExtractDirectory);
            this.tp_Extract.Controls.Add(this.b_ExtractBrowseDirectory);
            this.tp_Extract.Controls.Add(this.l_ExtractDirectory);
            this.tp_Extract.Location = new System.Drawing.Point(8, 39);
            this.tp_Extract.Margin = new System.Windows.Forms.Padding(6);
            this.tp_Extract.Name = "tp_Extract";
            this.tp_Extract.Padding = new System.Windows.Forms.Padding(6);
            this.tp_Extract.Size = new System.Drawing.Size(758, 639);
            this.tp_Extract.TabIndex = 0;
            this.tp_Extract.Text = "Extract";
            this.tp_Extract.UseVisualStyleBackColor = true;
            // 
            // buttonToStreamDic
            // 
            this.buttonToStreamDic.Location = new System.Drawing.Point(582, 38);
            this.buttonToStreamDic.Margin = new System.Windows.Forms.Padding(6);
            this.buttonToStreamDic.Name = "buttonToStreamDic";
            this.buttonToStreamDic.Size = new System.Drawing.Size(152, 66);
            this.buttonToStreamDic.TabIndex = 22;
            this.buttonToStreamDic.Text = "ExtractToDic";
            this.buttonToStreamDic.UseVisualStyleBackColor = true;
            this.buttonToStreamDic.Click += new System.EventHandler(this.buttonToStreamDic_Click);
            // 
            // tb_Messages
            // 
            this.tb_Messages.Location = new System.Drawing.Point(12, 280);
            this.tb_Messages.Margin = new System.Windows.Forms.Padding(6);
            this.tb_Messages.Multiline = true;
            this.tb_Messages.Name = "tb_Messages";
            this.tb_Messages.ReadOnly = true;
            this.tb_Messages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_Messages.Size = new System.Drawing.Size(1622, 628);
            this.tb_Messages.TabIndex = 21;
            // 
            // pb_ExtractProgress
            // 
            this.pb_ExtractProgress.Location = new System.Drawing.Point(6, 36);
            this.pb_ExtractProgress.Margin = new System.Windows.Forms.Padding(6);
            this.pb_ExtractProgress.Name = "pb_ExtractProgress";
            this.pb_ExtractProgress.Size = new System.Drawing.Size(390, 30);
            this.pb_ExtractProgress.TabIndex = 11;
            // 
            // b_ExtractBrowseArchive
            // 
            this.b_ExtractBrowseArchive.Location = new System.Drawing.Point(408, 232);
            this.b_ExtractBrowseArchive.Margin = new System.Windows.Forms.Padding(6);
            this.b_ExtractBrowseArchive.Name = "b_ExtractBrowseArchive";
            this.b_ExtractBrowseArchive.Size = new System.Drawing.Size(132, 36);
            this.b_ExtractBrowseArchive.TabIndex = 20;
            this.b_ExtractBrowseArchive.Text = "Browse";
            this.b_ExtractBrowseArchive.UseVisualStyleBackColor = true;
            this.b_ExtractBrowseArchive.Click += new System.EventHandler(this.b_ExtractBrowseArchive_Click);
            // 
            // pb_ExtractWork
            // 
            this.pb_ExtractWork.Location = new System.Drawing.Point(6, 76);
            this.pb_ExtractWork.Margin = new System.Windows.Forms.Padding(6);
            this.pb_ExtractWork.MarqueeAnimationSpeed = 25;
            this.pb_ExtractWork.Name = "pb_ExtractWork";
            this.pb_ExtractWork.Size = new System.Drawing.Size(390, 30);
            this.pb_ExtractWork.TabIndex = 12;
            // 
            // l_ExtractArchiveName
            // 
            this.l_ExtractArchiveName.AutoSize = true;
            this.l_ExtractArchiveName.Location = new System.Drawing.Point(6, 204);
            this.l_ExtractArchiveName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.l_ExtractArchiveName.Name = "l_ExtractArchiveName";
            this.l_ExtractArchiveName.Size = new System.Drawing.Size(214, 24);
            this.l_ExtractArchiveName.TabIndex = 19;
            this.l_ExtractArchiveName.Text = "Archive file name";
            // 
            // b_Extract
            // 
            this.b_Extract.Location = new System.Drawing.Point(408, 36);
            this.b_Extract.Margin = new System.Windows.Forms.Padding(6);
            this.b_Extract.Name = "b_Extract";
            this.b_Extract.Size = new System.Drawing.Size(132, 70);
            this.b_Extract.TabIndex = 13;
            this.b_Extract.Text = "Extract";
            this.b_Extract.UseVisualStyleBackColor = true;
            this.b_Extract.Click += new System.EventHandler(this.b_Extract_Click);
            // 
            // tb_ExtractArchive
            // 
            this.tb_ExtractArchive.Location = new System.Drawing.Point(6, 232);
            this.tb_ExtractArchive.Margin = new System.Windows.Forms.Padding(6);
            this.tb_ExtractArchive.Name = "tb_ExtractArchive";
            this.tb_ExtractArchive.Size = new System.Drawing.Size(386, 35);
            this.tb_ExtractArchive.TabIndex = 18;
            // 
            // l_ExtractProgress
            // 
            this.l_ExtractProgress.AutoSize = true;
            this.l_ExtractProgress.Location = new System.Drawing.Point(6, 6);
            this.l_ExtractProgress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.l_ExtractProgress.Name = "l_ExtractProgress";
            this.l_ExtractProgress.Size = new System.Drawing.Size(106, 24);
            this.l_ExtractProgress.TabIndex = 14;
            this.l_ExtractProgress.Text = "Progress";
            // 
            // tb_ExtractDirectory
            // 
            this.tb_ExtractDirectory.Location = new System.Drawing.Point(6, 144);
            this.tb_ExtractDirectory.Margin = new System.Windows.Forms.Padding(6);
            this.tb_ExtractDirectory.Name = "tb_ExtractDirectory";
            this.tb_ExtractDirectory.Size = new System.Drawing.Size(386, 35);
            this.tb_ExtractDirectory.TabIndex = 15;
            // 
            // b_ExtractBrowseDirectory
            // 
            this.b_ExtractBrowseDirectory.Location = new System.Drawing.Point(408, 144);
            this.b_ExtractBrowseDirectory.Margin = new System.Windows.Forms.Padding(6);
            this.b_ExtractBrowseDirectory.Name = "b_ExtractBrowseDirectory";
            this.b_ExtractBrowseDirectory.Size = new System.Drawing.Size(132, 36);
            this.b_ExtractBrowseDirectory.TabIndex = 17;
            this.b_ExtractBrowseDirectory.Text = "Browse";
            this.b_ExtractBrowseDirectory.UseVisualStyleBackColor = true;
            this.b_ExtractBrowseDirectory.Click += new System.EventHandler(this.b_ExtractBrowseDirectory_Click);
            // 
            // l_ExtractDirectory
            // 
            this.l_ExtractDirectory.AutoSize = true;
            this.l_ExtractDirectory.Location = new System.Drawing.Point(6, 114);
            this.l_ExtractDirectory.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.l_ExtractDirectory.Name = "l_ExtractDirectory";
            this.l_ExtractDirectory.Size = new System.Drawing.Size(202, 24);
            this.l_ExtractDirectory.TabIndex = 16;
            this.l_ExtractDirectory.Text = "Output directory";
            // 
            // tp_Compress
            // 
            this.tp_Compress.Controls.Add(this.pb_CompressProgress);
            this.tp_Compress.Controls.Add(this.b_BrowseOut);
            this.tp_Compress.Controls.Add(this.pb_CompressWork);
            this.tp_Compress.Controls.Add(this.l_CompressOutput);
            this.tp_Compress.Controls.Add(this.b_Compress);
            this.tp_Compress.Controls.Add(this.tb_CompressOutput);
            this.tp_Compress.Controls.Add(this.l_CompressProgress);
            this.tp_Compress.Controls.Add(this.gb_Settings);
            this.tp_Compress.Controls.Add(this.tb_CompressDirectory);
            this.tp_Compress.Controls.Add(this.b_Browse);
            this.tp_Compress.Controls.Add(this.l_CompressDirectory);
            this.tp_Compress.Location = new System.Drawing.Point(8, 39);
            this.tp_Compress.Margin = new System.Windows.Forms.Padding(6);
            this.tp_Compress.Name = "tp_Compress";
            this.tp_Compress.Padding = new System.Windows.Forms.Padding(6);
            this.tp_Compress.Size = new System.Drawing.Size(758, 639);
            this.tp_Compress.TabIndex = 1;
            this.tp_Compress.Text = "Compress";
            this.tp_Compress.UseVisualStyleBackColor = true;
            // 
            // ofd_Archive
            // 
            this.ofd_Archive.Title = "Open an archive...";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 686);
            this.Controls.Add(this.tbc_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SevenZipSharp Windows Forms Demonstration";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.gb_Settings.ResumeLayout(false);
            this.gb_Settings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nup_VolumeSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trb_Level)).EndInit();
            this.tbc_Main.ResumeLayout(false);
            this.tp_Extract.ResumeLayout(false);
            this.tp_Extract.PerformLayout();
            this.tp_Compress.ResumeLayout(false);
            this.tp_Compress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pb_CompressProgress;
        private System.Windows.Forms.ProgressBar pb_CompressWork;
        private System.Windows.Forms.Button b_Compress;
        private System.Windows.Forms.Label l_CompressProgress;
        private System.Windows.Forms.TextBox tb_CompressDirectory;
        private System.Windows.Forms.Label l_CompressDirectory;
        private System.Windows.Forms.Button b_Browse;
        private System.Windows.Forms.GroupBox gb_Settings;
        private System.Windows.Forms.Label l_Format;
        private System.Windows.Forms.ComboBox cb_Format;
        private System.Windows.Forms.FolderBrowserDialog fbd_Directory;
        private System.Windows.Forms.Label l_CompressionLevel;
        private System.Windows.Forms.TrackBar trb_Level;
        private System.Windows.Forms.Label l_Method;
        private System.Windows.Forms.ComboBox cb_Method;
        private System.Windows.Forms.CheckBox chb_Sfx;
        private System.Windows.Forms.Button b_BrowseOut;
        private System.Windows.Forms.Label l_CompressOutput;
        private System.Windows.Forms.TextBox tb_CompressOutput;
        private System.Windows.Forms.SaveFileDialog sfd_Archive;
        private System.Windows.Forms.TabControl tbc_Main;
        private System.Windows.Forms.TabPage tp_Extract;
        private System.Windows.Forms.TabPage tp_Compress;
        private System.Windows.Forms.TextBox tb_Messages;
        private System.Windows.Forms.ProgressBar pb_ExtractProgress;
        private System.Windows.Forms.Button b_ExtractBrowseArchive;
        private System.Windows.Forms.ProgressBar pb_ExtractWork;
        private System.Windows.Forms.Label l_ExtractArchiveName;
        private System.Windows.Forms.Button b_Extract;
        private System.Windows.Forms.TextBox tb_ExtractArchive;
        private System.Windows.Forms.Label l_ExtractProgress;
        private System.Windows.Forms.TextBox tb_ExtractDirectory;
        private System.Windows.Forms.Button b_ExtractBrowseDirectory;
        private System.Windows.Forms.Label l_ExtractDirectory;
        private System.Windows.Forms.OpenFileDialog ofd_Archive;
        private System.Windows.Forms.NumericUpDown nup_VolumeSize;
        private System.Windows.Forms.Label l_Volumes;
        private System.Windows.Forms.CheckBox chb_Volumes;
        private System.Windows.Forms.Button buttonToStreamDic;
    }
}

