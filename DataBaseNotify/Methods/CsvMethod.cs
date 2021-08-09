using DataBaseNotify.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseNotify.Methods
{
    public class CsvMethod
    {
        public string MyWorkPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

        public void Save_Csv(string DeviceName, MessageModule message)
        {
            if (!Directory.Exists($"{MyWorkPath}\\Report"))
                Directory.CreateDirectory($"{MyWorkPath}\\Report");
            if (!Directory.Exists($"{MyWorkPath}\\Report\\{DateTime.Now:yyyy}"))
                Directory.CreateDirectory($"{MyWorkPath}\\Report\\{DateTime.Now:yyyy}");
            string ReportPath = $"{MyWorkPath}\\Report\\{DateTime.Now:yyyy}\\Alarm_{DateTime.Now:MM}.csv";
            if (!File.Exists(ReportPath))
            {
                string Title = "時間,設備名稱,點位名稱,描述";
                StreamWriter WriteTitle = new StreamWriter(ReportPath, false, Encoding.Default);
                WriteTitle.WriteLine(Title);
                WriteTitle.Close();
            }
            string Data = $"{DateTime.Now:yyyy/MM/dd HH:mm:ss},{DeviceName},{message.FieldName},{message.Description}";
            StreamWriter WriteData = new StreamWriter(ReportPath, true, Encoding.Default);
            WriteData.WriteLine(Data);
            WriteData.Close();
        }
    }
}
