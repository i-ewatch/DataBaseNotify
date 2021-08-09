using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseNotify.Configuration
{
    public class DataBaseSetting
    {
        /// <summary>
        /// 資料庫類型
        /// <para>0 = MS_SQL</para>
        /// <para>1 = MY_SQL</para>
        /// </summary>
        public int DataBaseTypeEnum { get; set; }
        /// <summary>
        /// 資料庫IP
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 資料庫帳號
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 資料庫密碼
        /// </summary>
        public string Password { get; set; }
    }
}
