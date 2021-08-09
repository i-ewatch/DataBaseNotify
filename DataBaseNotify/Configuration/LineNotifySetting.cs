namespace DataBaseNotify.Configuration
{
    /// <summary>
    /// Line推播
    /// </summary>
    public  class LineNotifySetting
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
        /// Line權杖
        /// </summary>
        public string LineToken { get; set; }
    }
}
