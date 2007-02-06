using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenCVDotNet.Examples
{
    public partial class ExampleSelection : Form
    {
        public ExampleSelection()
        {
            InitializeComponent();
        }

        public void AddForm(Form f)
        {
            formList.Items.Add(new FormItem(f));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button1_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormItem fi = formList.SelectedItem as FormItem;
            fi.Run();

        }
    }

    class FormItem
    {
        Form f;

        public FormItem(Form f)
        {
            this.f = f;
        }

        public override string ToString()
        {
            return f.Text;
        }

        public void Run()
        {
            f.WindowState = FormWindowState.Maximized;
            f.ShowDialog();
        }
    }
}