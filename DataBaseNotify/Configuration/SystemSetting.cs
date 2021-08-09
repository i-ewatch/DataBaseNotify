namespace DataBaseNotify.Configuration
{
    public class SystemSetting
    {
        /// <summary>
        /// 簡訊功能旗標
        /// </summary>
        public bool TelephoneFlag { get; set; }
        /// <summary>
        /// 簡訊COMPort
        /// </summary>
        public string TelephoneCOM { get; set; }
        /// <summary>
        /// Line功能旗標
        /// </summary>
        public bool LineFlag { get; set; }
        /// <summary>
        /// Telegram功能旗標
        /// </summary>
        public bool TelegramFlag { get; set; }
        /// <summary>
        /// 自動通訊功能
        /// </summary>
        public bool AutoActionFlag { get; set; }
    }
}
