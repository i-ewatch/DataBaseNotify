using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseNotify.Modules
{
    public class MessageModule
    {
        /// <summary>
        /// 點位類型
        /// <para>0 = AI</para>
        /// <para>1 = DI</para>
        /// <para>2 = Enums</para>
        /// </summary>
        public int PointTypeEnum { get; set; }
        /// <summary>
        /// 資料庫編號
        /// </summary>
        public int DataBaseNum { get; set; }
        /// <summary>
        /// 資料表編號
        /// </summary>
        public int DataSheetNum { get; set; }
        /// <summary>
        /// 欄位編號
        /// </summary>
        public int FieldNum { get; set; }
        /// <summary>
        /// 時間
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// 點位名稱
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
