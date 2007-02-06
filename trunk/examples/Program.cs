using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

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
            
            ExampleSelection es = new ExampleSelection();

            Assembly me = Assembly.GetCallingAssembly();
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