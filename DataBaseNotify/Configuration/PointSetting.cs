using System.Collections.Generic;

namespace DataBaseNotify.Configuration
{
    public class PointSetting
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
        public List<DataSheet> DataSheets { get; set; } = new List<DataSheet>();

    }
    public class DataSheet
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
        /// AI資訊
        /// </summary>
        public List<AI> AIs { get; set; } = new List<AI>();
        /// <summary>
        /// DI資訊
        /// </summary>
        public List<DI> DIs { get; set; } = new List<DI>();
        /// <summary>
        /// 類型資訊
        /// </summary>
        public List<Enumses> Enumses { get; set; } = new List<Enumses>();
    }
    public class AI
    {
        /// <summary>
        /// 告警旗標
        /// </summary>
        public bool AlarmFlag { get; set; }
        /// <summary>
        /// 上限告警
        /// </summary>
        public bool MaxFlag { get; set; }
        /// <summary>
        /// 上限告警
        /// </summary>
        public bool ResetFlag { get; set; }
        /// <summary>
        /// 下限告警
        /// </summary>
        public bool MinFlag { get; set; }
        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 發報名稱
        /// </summary>
        public string ShowName { get; set; }
        /// <summary>
        /// 點位編號
        /// </summary>
        public int FieldNum { get; set; }
        /// <summary>
        /// 上限值
        /// </summary>
        public float MaxLimit { get; set; }
        /// <summary>
        /// 下限值
        /// </summary>
        public float MinLimit { get; set; }
        /// <summary>
        /// 群組資訊
        /// </summary>
        public string[] Group { get; set; }
    }
    public class DI
    {
        /// <summary>
        /// 告警旗標
        /// </summary>
        public bool AlarmFlag { get; set; }
        /// <summary>
        /// 上限告警
        /// </summary>
        public bool ResetFlag { get; set; }
        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 發報名稱
        /// </summary>
        public string ShowName { get; set; }
        /// <summary>
        /// 傳送訊息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 欄位編號
        /// </summary>
        public int FieldNum { get; set; }
        /// <summary>
        /// 標準其標
        /// </summary>
        public bool GeneralFlag { get; set; }
        /// <summary>
        /// 群組資訊
        /// </summary>
        public string[] Group { get; set; }
    }
    public class Enumses
    {
        /// <summary>
        /// 告警旗標
        /// </summary>
        public bool AlarmFlag { get; set; }
        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 發報名稱
        /// </summary>
        public string ShowName { get; set; }
        /// <summary>
        /// 類型描述
        /// </summary>
        public string EnumsDescribe { get; set; }
        /// <summary>
        /// 欄位編號
        /// </summary>
        public int FieldNum { get; set; }
        /// <summary>
        /// 群組資訊
        /// </summary>
        public string[] Group { get; set; }
    }
}
