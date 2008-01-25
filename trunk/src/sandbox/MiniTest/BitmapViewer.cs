using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    public partial class BitmapViewer : Form
    {
        
        public BitmapViewer(Image image)
        {
            InitializeComponent();
            pictureBox1.Image = image;
        }

        private void BitmapViewer_Load(object sender, EventArgs e)
        {

        }
    }
}