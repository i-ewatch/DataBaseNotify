using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseNotify.Configuration
{
    public class NotifyVisible
    {
        /// <summary>
        /// 簡訊機顯示旗標
        /// </summary>
        public bool TelePhoneFlag { get; set; }
        /// <summary>
        /// Line顯示旗標
        /// </summary>
        public bool LineNotifyFlag { get; set; }
        /// <summary>
        /// Telegram顯示旗標
        /// </summary>
        public bool TelegramFlag { get; set; }
    }
}
