namespace ComputerVision
{
    partial class Options
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
            this.thresholdScroll = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.alphaScroll = new System.Windows.Forms.TrackBar();
            this.thresholdValue = new System.Windows.Forms.Label();
            this.alphaValue = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Button();
            this.monochrome = new System.Windows.Forms.CheckBox();
            this.cvPanel = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdScroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alphaScroll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cvPanel)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // thresholdScroll
            // 
            this.thresholdScroll.Location = new System.Drawing.Point(13, 28);
            this.thresholdScroll.Minimum = -10;
            this.thresholdScroll.Name = "thresholdScroll";
            this.thresholdScroll.Size = new System.Drawing.Size(264, 45);
            this.thresholdScroll.TabIndex = 0;
            this.thresholdScroll.Scroll += new System.EventHandler(this.thresholdScroll_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Threshold:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Alpha:";
            // 
            // alphaScroll
            // 
            this.alphaScroll.Location = new System.Drawing.Point(13, 96);
            this.alphaScroll.Maximum = 100;
            this.alphaScroll.Name = "alphaScroll";
            this.alphaScroll.Size = new System.Drawing.Size(264, 45);
            this.alphaScroll.TabIndex = 3;
            this.alphaScroll.Scroll += new System.EventHandler(this.alphaScroll_Scroll);
            // 
            // thresholdValue
            // 
            this.thresholdValue.AutoSize = true;
            this.thresholdValue.Location = new System.Drawing.Point(73, 12);
            this.thresholdValue.Name = "thresholdValue";
            this.thresholdValue.Size = new System.Drawing.Size(22, 13);
            this.thresholdValue.TabIndex = 5;
            this.thresholdValue.Text = "0.1";
            // 
            // alphaValue
            // 
            this.alphaValue.AutoSize = true;
            this.alphaValue.Location = new System.Drawing.Point(53, 80);
            this.alphaValue.Name = "alphaValue";
            this.alphaValue.Size = new System.Drawing.Size(28, 13);
            this.alphaValue.TabIndex = 6;
            this.alphaValue.Text = "0.05";
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(13, 280);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 7;
            this.close.Text = "&Close";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // monochrome
            // 
            this.monochrome.AutoSize = true;
            this.monochrome.Checked = true;
            this.monochrome.CheckState = System.Windows.Forms.CheckState.Checked;
            this.monochrome.Location = new System.Drawing.Point(13, 159);
            this.monochrome.Name = "monochrome";
            this.monochrome.Size = new System.Drawing.Size(88, 17);
            this.monochrome.TabIndex = 8;
            this.monochrome.Text = "Monochrome";
            this.monochrome.UseVisualStyleBackColor = true;
            // 
            // cvPanel
            // 
            this.cvPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cvPanel.Location = new System.Drawing.Point(308, 10);
            this.cvPanel.Name = "cvPanel";
            this.cvPanel.Size = new System.Drawing.Size(347, 317);
            this.cvPanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cvPanel.TabIndex = 9;
            this.cvPanel.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.thresholdScroll);
            this.panel1.Controls.Add(this.close);
            this.panel1.Controls.Add(this.monochrome);
            this.panel1.Controls.Add(this.alphaScroll);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.alphaValue);
            this.panel1.Controls.Add(this.thresholdValue);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(298, 317);
            this.panel1.TabIndex = 10;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 337);
            this.Controls.Add(this.cvPanel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Options";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Background Subtraction";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Options_FormClosing);
            this.Load += new System.EventHandler(this.Options_Load);
            ((System.ComponentModel.ISupportInitialize)(this.thresholdScroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alphaScroll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cvPanel)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar thresholdScroll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar alphaScroll;
        private System.Windows.Forms.Label thresholdValue;
        private System.Windows.Forms.Label alphaValue;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.CheckBox monochrome;
        private System.Windows.Forms.PictureBox cvPanel;
        private System.Windows.Forms.Panel panel1;
    }
}