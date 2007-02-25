using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using OpenCVDotNet;

namespace OpenCVDotNet.Examples
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // make sure all OpenCV errors are thrown as exceptions.
            CVUtils.ErrorsToExceptions();

            ExampleSelection es = new ExampleSelection();

            Assembly me = Assembly.GetExecutingAssembly();
            foreach (Type t in me.GetTypes())
            {
                if (t.IsSubclassOf(typeof(Form)) && t != typeof(ExampleSelection))
                {
                    Form f = (Form) me.CreateInstance(t.FullName);
                    es.AddForm(f);
                }
            }

            es.ShowDialog();
        }
    }
}