namespace DataBaseNotify.Configuration
{
    public class GroupSetting
    {
        /// <summary>
        /// 唯一值編號
        /// </summary>
        public string Index { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 簡訊資訊
        /// </summary>
        public string[] TelePhone { get; set; }
        /// <summary>
        /// Line資訊
        /// </summary>
        public string[] Line { get; set; }
        /// <summary>
        /// Telegram資訊
        /// </summary>
        public string[] Telegram { get; set; }
    }
}
