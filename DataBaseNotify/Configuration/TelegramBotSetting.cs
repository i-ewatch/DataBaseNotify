namespace DataBaseNotify.Configuration
{
    /// <summary>
    /// TelegramBot推播
    /// </summary>
    public class TelegramBotSetting
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
        /// 機器人 API網址
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 機器人 傳送訊息ID (群組 = 有負號， 個人 = 沒有負號)
        /// </summary>
        public string Chart_ID { get; set; }
    }
}
