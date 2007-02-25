namespace OpenCVDotNet.Examples
{
    partial class MaskedHistogramTracker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaskedHistogramTracker));
            this.maskPicture = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.openButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripSeparator();
            this.playButton = new System.Windows.Forms.ToolStripButton();
            this.pauseButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.resetStatsButton = new System.Windows.Forms.ToolStripButton();
            this.maskOverlay = new System.Windows.Forms.PictureBox();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.noMaskHist = new OpenCVDotNet.UI.ColorHistogram();
            this.meanShiftNoMask = new OpenCVDotNet.UI.SelectPictureBox();
            this.maskHistogram = new OpenCVDotNet.UI.ColorHistogram();
            this.meanShift = new OpenCVDotNet.UI.SelectPictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bpNoMask = new System.Windows.Forms.PictureBox();
            this.bpWithMask = new System.Windows.Forms.PictureBox();
            this.vot = new OpenCVDotNet.UI.ValueOverTime();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lastNoMask = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.avgNoMask = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lastWithMask = new System.Windows.Forms.Label();
            this.avgWithMask = new System.Windows.Forms.Label();
            this.binsLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.histBins = new System.Windows.Forms.TrackBar();
            this.thresholdLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ffThresh = new System.Windows.Forms.TrackBar();
            this.includeNeautral = new System.Windows.Forms.CheckBox();
            this.videoPlayer = new OpenCVDotNet.UI.VideoPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.maskPicture)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maskOverlay)).BeginInit();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meanShiftNoMask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meanShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bpNoMask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bpWithMask)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.histBins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ffThresh)).BeginInit();
            this.SuspendLayout();
            // 
            // maskPicture
            // 
            this.maskPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maskPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.maskPicture.Location = new System.Drawing.Point(11, 14);
            this.maskPicture.Name = "maskPicture";
            this.maskPicture.Size = new System.Drawing.Size(171, 138);
            this.maskPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.maskPicture.TabIndex = 4;
            this.maskPicture.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openButton,
            this.toolStripButton2,
            this.playButton,
            this.pauseButton,
            this.toolStripSeparator1,
            this.resetStatsButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(229, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // openButton
            // 
            this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openButton.Image = ((System.Drawing.Image)(resources.GetObject("openButton.Image")));
            this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(49, 22);
            this.openButton.Text = "&Open...";
            this.openButton.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(6, 25);
            // 
            // playButton
            // 
            this.playButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.playButton.Image = ((System.Drawing.Image)(resources.GetObject("playButton.Image")));
            this.playButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(31, 22);
            this.playButton.Text = "&Play";
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.pauseButton.Image = ((System.Drawing.Image)(resources.GetObject("pauseButton.Image")));
            this.pauseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(40, 22);
            this.pauseButton.Text = "P&ause";
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // resetStatsButton
            // 
            this.resetStatsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.resetStatsButton.Image = ((System.Drawing.Image)(resources.GetObject("resetStatsButton.Image")));
            this.resetStatsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resetStatsButton.Name = "resetStatsButton";
            this.resetStatsButton.Size = new System.Drawing.Size(85, 22);
            this.resetStatsButton.Text = "Reset Statistics";
            this.resetStatsButton.Click += new System.EventHandler(this.resetStatsButton_Click);
            // 
            // maskOverlay
            // 
            this.maskOverlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maskOverlay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.maskOverlay.Location = new System.Drawing.Point(11, 158);
            this.maskOverlay.Name = "maskOverlay";
            this.maskOverlay.Size = new System.Drawing.Size(171, 138);
            this.maskOverlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.maskOverlay.TabIndex = 6;
            this.maskOverlay.TabStop = false;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tableLayoutPanel3);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panel3);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(674, 614);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(674, 639);
            this.toolStripContainer1.TabIndex = 10;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.pictureBox5, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.pictureBox2, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.noMaskHist, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.meanShiftNoMask, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.maskHistogram, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.meanShift, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.bpNoMask, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.bpWithMask, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.vot, 0, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(480, 614);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.SystemColors.Desktop;
            this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox5.Location = new System.Drawing.Point(239, 256);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(2, 247);
            this.pictureBox5.TabIndex = 11;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Desktop;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(239, 509);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(2, 58);
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // noMaskHist
            // 
            this.noMaskHist.BinsPerChannel = 100;
            this.noMaskHist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noMaskHist.Location = new System.Drawing.Point(247, 509);
            this.noMaskHist.Name = "noMaskHist";
            this.noMaskHist.Size = new System.Drawing.Size(230, 58);
            this.noMaskHist.TabIndex = 7;
            // 
            // meanShiftNoMask
            // 
            this.meanShiftNoMask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meanShiftNoMask.Location = new System.Drawing.Point(247, 3);
            this.meanShiftNoMask.Name = "meanShiftNoMask";
            this.meanShiftNoMask.ReadOnly = false;
            this.meanShiftNoMask.SelectionRect = new System.Drawing.Rectangle(10, 10, 100, 100);
            this.meanShiftNoMask.ShowCross = true;
            this.meanShiftNoMask.ShowSelection = true;
            this.meanShiftNoMask.Size = new System.Drawing.Size(230, 247);
            this.meanShiftNoMask.TabIndex = 1;
            this.meanShiftNoMask.TabStop = false;
            this.meanShiftNoMask.SelectionChanged += new System.EventHandler(this.meanShiftNoMask_SelectionChanged);
            // 
            // maskHistogram
            // 
            this.maskHistogram.BinsPerChannel = 100;
            this.maskHistogram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maskHistogram.Location = new System.Drawing.Point(3, 509);
            this.maskHistogram.Name = "maskHistogram";
            this.maskHistogram.Size = new System.Drawing.Size(230, 58);
            this.maskHistogram.TabIndex = 2;
            // 
            // meanShift
            // 
            this.meanShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meanShift.Location = new System.Drawing.Point(3, 3);
            this.meanShift.Name = "meanShift";
            this.meanShift.ReadOnly = false;
            this.meanShift.SelectionRect = new System.Drawing.Rectangle(10, 10, 100, 100);
            this.meanShift.ShowCross = false;
            this.meanShift.ShowSelection = true;
            this.meanShift.Size = new System.Drawing.Size(230, 247);
            this.meanShift.TabIndex = 0;
            this.meanShift.TabStop = false;
            this.meanShift.SelectionChanged += new System.EventHandler(this.output_SelectionChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Desktop;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(239, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(2, 247);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // bpNoMask
            // 
            this.bpNoMask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bpNoMask.Location = new System.Drawing.Point(247, 256);
            this.bpNoMask.Name = "bpNoMask";
            this.bpNoMask.Size = new System.Drawing.Size(230, 247);
            this.bpNoMask.TabIndex = 10;
            this.bpNoMask.TabStop = false;
            // 
            // bpWithMask
            // 
            this.bpWithMask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bpWithMask.Location = new System.Drawing.Point(3, 256);
            this.bpWithMask.Name = "bpWithMask";
            this.bpWithMask.Size = new System.Drawing.Size(230, 247);
            this.bpWithMask.TabIndex = 10;
            this.bpWithMask.TabStop = false;
            // 
            // vot
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.vot, 3);
            this.vot.DefaultColorGroup = System.Drawing.Color.Blue;
            this.vot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vot.Location = new System.Drawing.Point(3, 573);
            this.vot.Name = "vot";
            this.vot.Size = new System.Drawing.Size(474, 38);
            this.vot.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.binsLabel);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.histBins);
            this.panel3.Controls.Add(this.thresholdLabel);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.ffThresh);
            this.panel3.Controls.Add(this.includeNeautral);
            this.panel3.Controls.Add(this.maskPicture);
            this.panel3.Controls.Add(this.maskOverlay);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(480, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(194, 614);
            this.panel3.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lastNoMask);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.avgNoMask);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lastWithMask);
            this.groupBox1.Controls.Add(this.avgWithMask);
            this.groupBox1.Location = new System.Drawing.Point(13, 467);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 116);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Average Area";
            // 
            // lastNoMask
            // 
            this.lastNoMask.AutoSize = true;
            this.lastNoMask.ForeColor = System.Drawing.Color.Red;
            this.lastNoMask.Location = new System.Drawing.Point(78, 87);
            this.lastNoMask.Name = "lastNoMask";
            this.lastNoMask.Size = new System.Drawing.Size(52, 13);
            this.lastNoMask.TabIndex = 18;
            this.lastNoMask.Text = "[average]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(11, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "No Mask:";
            // 
            // avgNoMask
            // 
            this.avgNoMask.AutoSize = true;
            this.avgNoMask.ForeColor = System.Drawing.Color.Red;
            this.avgNoMask.Location = new System.Drawing.Point(78, 70);
            this.avgNoMask.Name = "avgNoMask";
            this.avgNoMask.Size = new System.Drawing.Size(52, 13);
            this.avgNoMask.TabIndex = 16;
            this.avgNoMask.Text = "[average]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(11, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "With Mask:";
            // 
            // lastWithMask
            // 
            this.lastWithMask.AutoSize = true;
            this.lastWithMask.ForeColor = System.Drawing.Color.Navy;
            this.lastWithMask.Location = new System.Drawing.Point(78, 42);
            this.lastWithMask.Name = "lastWithMask";
            this.lastWithMask.Size = new System.Drawing.Size(52, 13);
            this.lastWithMask.TabIndex = 14;
            this.lastWithMask.Text = "[average]";
            // 
            // avgWithMask
            // 
            this.avgWithMask.AutoSize = true;
            this.avgWithMask.ForeColor = System.Drawing.Color.Navy;
            this.avgWithMask.Location = new System.Drawing.Point(78, 25);
            this.avgWithMask.Name = "avgWithMask";
            this.avgWithMask.Size = new System.Drawing.Size(52, 13);
            this.avgWithMask.TabIndex = 14;
            this.avgWithMask.Text = "[average]";
            // 
            // binsLabel
            // 
            this.binsLabel.AutoSize = true;
            this.binsLabel.Location = new System.Drawing.Point(106, 401);
            this.binsLabel.Name = "binsLabel";
            this.binsLabel.Size = new System.Drawing.Size(35, 13);
            this.binsLabel.TabIndex = 13;
            this.binsLabel.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 401);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Histogram bins:";
            // 
            // histBins
            // 
            this.histBins.LargeChange = 10;
            this.histBins.Location = new System.Drawing.Point(3, 417);
            this.histBins.Maximum = 255;
            this.histBins.Minimum = 1;
            this.histBins.Name = "histBins";
            this.histBins.Size = new System.Drawing.Size(152, 45);
            this.histBins.TabIndex = 11;
            this.histBins.Value = 30;
            this.histBins.Scroll += new System.EventHandler(this.histBins_Scroll);
            // 
            // thresholdLabel
            // 
            this.thresholdLabel.AutoSize = true;
            this.thresholdLabel.Location = new System.Drawing.Point(106, 336);
            this.thresholdLabel.Name = "thresholdLabel";
            this.thresholdLabel.Size = new System.Drawing.Size(35, 13);
            this.thresholdLabel.TabIndex = 10;
            this.thresholdLabel.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 336);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Flood fill threshold:";
            // 
            // ffThresh
            // 
            this.ffThresh.LargeChange = 1;
            this.ffThresh.Location = new System.Drawing.Point(3, 352);
            this.ffThresh.Maximum = 20;
            this.ffThresh.Name = "ffThresh";
            this.ffThresh.Size = new System.Drawing.Size(152, 45);
            this.ffThresh.TabIndex = 8;
            this.ffThresh.Value = 2;
            this.ffThresh.Scroll += new System.EventHandler(this.ffThresh_Scroll);
            // 
            // includeNeautral
            // 
            this.includeNeautral.AutoSize = true;
            this.includeNeautral.Location = new System.Drawing.Point(11, 311);
            this.includeNeautral.Name = "includeNeautral";
            this.includeNeautral.Size = new System.Drawing.Size(134, 17);
            this.includeNeautral.TabIndex = 7;
            this.includeNeautral.Text = "Include &Neautral Pixels";
            this.includeNeautral.UseVisualStyleBackColor = true;
            this.includeNeautral.CheckedChanged += new System.EventHandler(this.includeNeautral_CheckedChanged);
            // 
            // videoPlayer
            // 
            this.videoPlayer.Loop = true;
            this.videoPlayer.PictureBox = this.meanShift;
            this.videoPlayer.NextFrame += new OpenCVDotNet.UI.NextFrameEventHandler(this.videoPlayer_NextFrame);
            // 
            // MaskedHistogramTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 639);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "MaskedHistogramTracker";
            this.Text = "Masked Histogram Tracker";
            this.Load += new System.EventHandler(this.SmartHistograms_Load);
            ((System.ComponentModel.ISupportInitialize)(this.maskPicture)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maskOverlay)).EndInit();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meanShiftNoMask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meanShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bpNoMask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bpWithMask)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.histBins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ffThresh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OpenCVDotNet.UI.SelectPictureBox meanShift;
        private OpenCVDotNet.UI.ColorHistogram maskHistogram;
        private System.Windows.Forms.PictureBox maskPicture;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton playButton;
        private System.Windows.Forms.ToolStripButton pauseButton;
        private System.Windows.Forms.PictureBox maskOverlay;
        private OpenCVDotNet.UI.ColorHistogram noMaskHist;
        private OpenCVDotNet.UI.VideoPlayer videoPlayer;
        private OpenCVDotNet.UI.SelectPictureBox meanShiftNoMask;
        private System.Windows.Forms.ToolStripButton openButton;
        private System.Windows.Forms.ToolStripSeparator toolStripButton2;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox includeNeautral;
        private System.Windows.Forms.Label thresholdLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar ffThresh;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox bpNoMask;
        private System.Windows.Forms.PictureBox bpWithMask;
        private System.Windows.Forms.Label binsLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar histBins;
        private OpenCVDotNet.UI.ValueOverTime vot;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label avgNoMask;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label avgWithMask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton resetStatsButton;
        private System.Windows.Forms.Label lastNoMask;
        private System.Windows.Forms.Label lastWithMask;
    }
}