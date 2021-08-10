using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DataBaseNotify
{
    static class Program
    {
        /// <summary>
        /// 初始路徑
        /// </summary>
        private static string MyWorkPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
  
            if (File.Exists($"{MyWorkPath}\\stf\\DataBase.json") && File.Exists($"{MyWorkPath}\\stf\\NotifyVisible.json"))
            {
                Application.Run(new Form1());
            }
            else
            {
                Application.Run(new WizardForm());
            }
        }
    }
}