namespace DataBaseNotify.Modules
{
    public class SlaveModule
    {
        /// <summary>
        /// 點位
        /// </summary>
        public int Address { get; set; }
        /// <summary>
        /// 資料庫欄位
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 內容(點位名稱、需告知 Function)
        /// </summary>
        public string PointStr { get; set; }
        /// <summary>
        /// 類型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 單位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 增益
        /// </summary>
        public int Gain { get; set; }
        /// <summary>
        /// 高位元描述
        /// </summary>
        public string HStr { get; set; }
        /// <summary>
        /// 低位元描述
        /// </summary>
        public string LStr { get; set; }
    }
}
