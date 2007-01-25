namespace Histograms
{
    partial class Histograms
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bluePanel = new System.Windows.Forms.PictureBox();
            this.greenPanel = new System.Windows.Forms.PictureBox();
            this.redCheck = new System.Windows.Forms.CheckBox();
            this.greenCheck = new System.Windows.Forms.CheckBox();
            this.blueCheck = new System.Windows.Forms.CheckBox();
            this.redPanel = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binSize = new System.Windows.Forms.ToolStripTextBox();
            this.status = new System.Windows.Forms.StatusStrip();
            this.statusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.originalImage = new OpenCVDotNet.UI.SelectPictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bluePanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.redPanel)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originalImage)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.bluePanel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.greenPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.redCheck, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.greenCheck, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.blueCheck, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.redPanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 456);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(698, 113);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // bluePanel
            // 
            this.bluePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bluePanel.Location = new System.Drawing.Point(1, 75);
            this.bluePanel.Margin = new System.Windows.Forms.Padding(0);
            this.bluePanel.Name = "bluePanel";
            this.bluePanel.Size = new System.Drawing.Size(675, 37);
            this.bluePanel.TabIndex = 16;
            this.bluePanel.TabStop = false;
            // 
            // greenPanel
            // 
            this.greenPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.greenPanel.Location = new System.Drawing.Point(1, 38);
            this.greenPanel.Margin = new System.Windows.Forms.Padding(0);
            this.greenPanel.Name = "greenPanel";
            this.greenPanel.Size = new System.Drawing.Size(675, 36);
            this.greenPanel.TabIndex = 15;
            this.greenPanel.TabStop = false;
            // 
            // redCheck
            // 
            this.redCheck.AutoSize = true;
            this.redCheck.Checked = true;
            this.redCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.redCheck.Location = new System.Drawing.Point(680, 4);
            this.redCheck.Name = "redCheck";
            this.redCheck.Size = new System.Drawing.Size(14, 14);
            this.redCheck.TabIndex = 11;
            this.redCheck.UseVisualStyleBackColor = true;
            this.redCheck.CheckedChanged += new System.EventHandler(this.redCheck_CheckedChanged);
            // 
            // greenCheck
            // 
            this.greenCheck.AutoSize = true;
            this.greenCheck.Checked = true;
            this.greenCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.greenCheck.Location = new System.Drawing.Point(680, 41);
            this.greenCheck.Name = "greenCheck";
            this.greenCheck.Size = new System.Drawing.Size(14, 14);
            this.greenCheck.TabIndex = 12;
            this.greenCheck.UseVisualStyleBackColor = true;
            this.greenCheck.CheckedChanged += new System.EventHandler(this.blueCheck_CheckedChanged);
            // 
            // blueCheck
            // 
            this.blueCheck.AutoSize = true;
            this.blueCheck.Checked = true;
            this.blueCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.blueCheck.Location = new System.Drawing.Point(680, 78);
            this.blueCheck.Name = "blueCheck";
            this.blueCheck.Size = new System.Drawing.Size(14, 14);
            this.blueCheck.TabIndex = 13;
            this.blueCheck.UseVisualStyleBackColor = true;
            this.blueCheck.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // redPanel
            // 
            this.redPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.redPanel.Location = new System.Drawing.Point(1, 1);
            this.redPanel.Margin = new System.Windows.Forms.Padding(0);
            this.redPanel.Name = "redPanel";
            this.redPanel.Size = new System.Drawing.Size(675, 36);
            this.redPanel.TabIndex = 14;
            this.redPanel.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.binSize});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(698, 25);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openVideoToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 21);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.openToolStripMenuItem.Text = "&Open Image...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openVideoToolStripMenuItem
            // 
            this.openVideoToolStripMenuItem.Name = "openVideoToolStripMenuItem";
            this.openVideoToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.openVideoToolStripMenuItem.Text = "Open &Video...";
            this.openVideoToolStripMenuItem.Click += new System.EventHandler(this.openVideoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(142, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // binSize
            // 
            this.binSize.Name = "binSize";
            this.binSize.Size = new System.Drawing.Size(100, 21);
            this.binSize.Text = "255";
            this.binSize.ToolTipText = "Numbre of Bins";
            this.binSize.TextChanged += new System.EventHandler(this.binSize_TextChanged);
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBar});
            this.status.Location = new System.Drawing.Point(0, 569);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(698, 22);
            this.status.TabIndex = 9;
            // 
            // statusBar
            // 
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(38, 17);
            this.statusBar.Text = "Ready";
            // 
            // originalImage
            // 
            this.originalImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.originalImage.Location = new System.Drawing.Point(0, 25);
            this.originalImage.Name = "originalImage";
            this.originalImage.Size = new System.Drawing.Size(698, 431);
            this.originalImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.originalImage.TabIndex = 10;
            this.originalImage.TabStop = false;
            this.originalImage.Click += new System.EventHandler(this.originalImage_Click);
            // 
            // Histograms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 591);
            this.Controls.Add(this.originalImage);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.status);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Histograms";
            this.Text = "Color Histograms";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SizeChanged += new System.EventHandler(this.Histograms_SizeChanged);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bluePanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.redPanel)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originalImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox binSize;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel statusBar;
        private System.Windows.Forms.CheckBox blueCheck;
        private System.Windows.Forms.CheckBox redCheck;
        private System.Windows.Forms.CheckBox greenCheck;
        private System.Windows.Forms.ToolStripMenuItem openVideoToolStripMenuItem;
        private OpenCVDotNet.UI.SelectPictureBox originalImage;
        private System.Windows.Forms.PictureBox redPanel;
        private System.Windows.Forms.PictureBox bluePanel;
        private System.Windows.Forms.PictureBox greenPanel;
    }
}

