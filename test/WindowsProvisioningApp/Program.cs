using System;
using System.Windows.Forms;
using WindowsProvisioningApp;

namespace WindowsProvisioningApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // This should match your Form class name
        }
    }
}
