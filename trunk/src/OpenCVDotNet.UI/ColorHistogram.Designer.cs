namespace OpenCVDotNet.UI
{
    partial class ColorHistogram
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bluePanel = new System.Windows.Forms.PictureBox();
            this.greenPanel = new System.Windows.Forms.PictureBox();
            this.redPanel = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bluePanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.redPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.bluePanel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.greenPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.redPanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(263, 97);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // bluePanel
            // 
            this.bluePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bluePanel.Location = new System.Drawing.Point(1, 65);
            this.bluePanel.Margin = new System.Windows.Forms.Padding(0);
            this.bluePanel.Name = "bluePanel";
            this.bluePanel.Size = new System.Drawing.Size(261, 31);
            this.bluePanel.TabIndex = 16;
            this.bluePanel.TabStop = false;
            // 
            // greenPanel
            // 
            this.greenPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.greenPanel.Location = new System.Drawing.Point(1, 33);
            this.greenPanel.Margin = new System.Windows.Forms.Padding(0);
            this.greenPanel.Name = "greenPanel";
            this.greenPanel.Size = new System.Drawing.Size(261, 31);
            this.greenPanel.TabIndex = 15;
            this.greenPanel.TabStop = false;
            // 
            // redPanel
            // 
            this.redPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.redPanel.Location = new System.Drawing.Point(1, 1);
            this.redPanel.Margin = new System.Windows.Forms.Padding(0);
            this.redPanel.Name = "redPanel";
            this.redPanel.Size = new System.Drawing.Size(261, 31);
            this.redPanel.TabIndex = 14;
            this.redPanel.TabStop = false;
            // 
            // ColorHistogram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ColorHistogram";
            this.Size = new System.Drawing.Size(263, 97);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bluePanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.greenPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.redPanel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox bluePanel;
        private System.Windows.Forms.PictureBox greenPanel;
        private System.Windows.Forms.PictureBox redPanel;
    }
}
