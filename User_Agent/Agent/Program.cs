﻿using System;
using System.Windows.Forms;

namespace DeviceInfoApp  // ✅ Ensure this matches the namespace in Form1.cs
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form1());  // ✅ Ensure Form1 is correctly referenced
        }
    }
}
