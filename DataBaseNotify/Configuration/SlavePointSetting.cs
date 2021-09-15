using System.Collections.Generic;

namespace DataBaseNotify.Configuration
{
    public class SlavePointSetting
    {
        /// <summary>
        /// Slave通訊類型
        /// </summary>
        public int SlaveComponentTypeEnum { get; set; }
        /// <summary>
        /// Slave通訊 IP/COM
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Slave通訊 Port/Rate
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Slave ID資訊
        /// </summary>
        public List<SlaveIDSetting> SlaveIDSettings { get; set; } = new List<SlaveIDSetting>();
    }
    public class SlaveIDSetting
    {
        /// <summary>
        /// Slave ID
        /// </summary>
        public int SlaveID { get; set; }
        /// <summary>
        /// 資料庫資訊
        /// </summary>
        public List<SlaveDataBase> SlaveDataBases { get; set; } = new List<SlaveDataBase>();
    }
    public class SlaveDataBase
    {
        /// <summary>
        /// 連線旗標
        /// </summary>
        public bool Connection { get; set; }
        /// <summary>
        /// 資料庫名稱
        /// </summary>
        public string DataBaseName { get; set; }
        /// <summary>
        /// 資料庫編號
        /// </summary>
        public int DataBaseNum { get; set; }
        /// <summary>
        /// 資料表資訊
        /// </summary>
        public List<SlaveDataSheet> SlaveDataSheets { get; set; } = new List<SlaveDataSheet>();
    }
    public class SlaveDataSheet
    {
        /// <summary>
        /// 資料表名稱
        /// </summary>
        public string DataSheetName { get; set; }
        /// <summary>
        /// 時間欄位
        /// </summary>
        public string TimeName { get; set; }
        /// <summary>
        /// 時間欄位類型
        /// </summary>
        public string TimeTypeEnum { get; set; }
        /// <summary>
        /// 資料表編號
        /// </summary>
        public int DataSheetNum { get; set; }
        /// <summary>
        /// Fun1 狀態 讀/寫 資訊
        /// </summary>
        //public List<Fun1> Fun1s { get; set; } = new List<Fun1>();
        /// <summary>
        /// Fun2 狀態 讀 資訊
        /// </summary>
        public List<Fun2> Fun2s { get; set; } = new List<Fun2>();
        /// <summary>
        /// Fun3 狀態 讀/寫 資訊
        /// </summary>
        //public List<Fun3> Fun3s { get; set; } = new List<Fun3>();
        /// <summary>
        /// Fun4 狀態 讀 資訊
        /// </summary>
        public List<Fun4> Fun4s { get; set; } = new List<Fun4>();
    }
    public class Fun1
    {
        /// <summary>
        /// 通訊位址
        /// </summary>
        public ushort Address { get; set; }
        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 點位名稱
        /// </summary>
        public string PointStr { get; set; }
        /// <summary>
        /// 高位元描述
        /// </summary>
        public string HStr { get; set; } = "ON";
        /// <summary>
        /// 低位元描述
        /// </summary>
        public string LStr { get; set; } = "OFF";
    }
    public class Fun2
    {
        /// <summary>
        /// 通訊位址
        /// </summary>
        public ushort Address { get; set; }
        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 點位名稱
        /// </summary>
        public string PointStr { get; set; }
        /// <summary>
        /// 高位元描述
        /// </summary>
        public string HStr { get; set; } = "ON";
        /// <summary>
        /// 低位元描述
        /// </summary>
        public string LStr { get; set; } = "OFF";
    }
    public class Fun3
    {
        /// <summary>
        /// 通訊位址
        /// </summary>
        public ushort Address { get; set; }
        /// <summary>
        /// 分析類型
        /// </summary>
        public int AnalysisTypeEnum { get; set; }
        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 點位名稱
        /// </summary>
        public string PointStr { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 增益
        /// </summary>
        public int Gain { get; set; }
    }
    public class Fun4
    {
        /// <summary>
        /// 通訊位址
        /// </summary>
        public ushort Address { get; set; }
        /// <summary>
        /// 分析類型
        /// </summary>
        public int AnalysisTypeEnum { get; set; }
        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 點位名稱
        /// </summary>
        public string PointStr { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 增益
        /// </summary>
        public int Gain { get; set; }
    }
}
