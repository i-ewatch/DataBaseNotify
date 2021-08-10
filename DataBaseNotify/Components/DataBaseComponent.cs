using DataBaseNotify.Configuration;
using DataBaseNotify.Methods;
using DataBaseNotify.Modules;
using DataBaseNotify.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataBaseNotify.Components
{
    public partial class DataBaseComponent : Field4Component
    {
        public DataBaseComponent(HomeUserControl homeUserControl, List<PointSetting> pointSettings, My_SQL_Method my_SQL_Method, MS_SQL_Method mS_SQL_Method)
        {
            InitializeComponent();
            My_SQL_Method = my_SQL_Method;
            MS_SQL_Method = mS_SQL_Method;
            HomeUserControl = homeUserControl;
            PointSettings = pointSettings;
        }

        public DataBaseComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void AfterMyWorkStateChanged(object sender, EventArgs e)
        {
            if (myWorkState)
            {
                int ai = 0;
                int di = 0;
                int enumses = 0;
                SystemSetting = InitialMethod.Load_System();
                PointSettings = InitialMethod.Load_Point();
                foreach (var DateBaseitem in PointSettings)
                {
                    foreach (var DataSheetitem in DateBaseitem.DataSheets)
                    {
                        ai = ai + DataSheetitem.AIs.Count;
                        di = di + DataSheetitem.DIs.Count;
                        enumses = enumses + DataSheetitem.Enumses.Count;
                    }
                    AIs = new int[ai];
                    for (int i = 0; i < AIs.Length; i++)
                    {
                        AIs[i] = 0;
                    }
                    DIs = new bool[di];
                    Enumses = new int[enumses];
                    for (int i = 0; i < Enumses.Length; i++)
                    {
                        Enumses[i] = 0;
                    }
                }
                ComponentThread = new Thread(Analysis);
                ComponentThread.Start();
            }
            else
            {
                if (ComponentThread != null)
                {
                    ComponentThread.Abort();
                }
            }
        }
        public void Analysis()
        {
            while (myWorkState)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(ComponentTime);
                if (timeSpan.TotalMilliseconds > 1000)
                {
                    int AIIndex = 0;
                    int DIIndex = 0;
                    int EnumsesIndex = 0;
                    if (!myWorkState)
                    {
                        break;
                    }
                    if (MS_SQL_Method != null)
                    {
                        foreach (var DateBaseitem in PointSettings)
                        {
                            foreach (var DataSheetitem in DateBaseitem.DataSheets)
                            {
                                foreach (var item in DataSheetitem.AIs)
                                {
                                    if (!myWorkState)
                                    {
                                        break;
                                    }
                                    double? data = MS_SQL_Method.Search_Data(DateBaseitem.DataBaseName, DataSheetitem.DataSheetName, item.FieldName, DataSheetitem.TimeName, DataSheetitem.TimeTypeEnum);
                                    if (item.MaxLimit > item.MinLimit && data.HasValue)
                                    {
                                        #region 高於上限值
                                        if (data.Value > item.MaxLimit && item.MaxFlag)
                                        {
                                            if (AIs[AIIndex] != 1)
                                            {
                                                AIs[AIIndex] = 1;
                                                string PointName = item.FieldName;
                                                if (item.ShowName != "")
                                                {
                                                    PointName = item.ShowName;
                                                }
                                                MessageModule message = new MessageModule()
                                                {
                                                    PointTypeEnum = 0,
                                                    DataBaseNum = DateBaseitem.DataBaseNum,
                                                    DataSheetNum = DataSheetitem.DataSheetNum,
                                                    FieldNum = item.FieldNum,
                                                    DateTime = DateTime.Now,
                                                    FieldName = PointName,
                                                    Description = $"資料庫:{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName} 的 {PointName} 點位，高於上限值:{item.MaxLimit}，目前數值: {data.Value.ToString("0.##")}"
                                                };
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelePhoneMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.LineFlag)
                                                {
                                                    HomeUserControl.LineNotifyMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelegramMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                CsvMethod.Save_Csv($"{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName}", message);
                                            }
                                        }
                                        #endregion
                                        #region 低於下限值
                                        else if (data.Value < item.MinLimit && item.MinFlag)
                                        {
                                            if (AIs[AIIndex] != 2)
                                            {
                                                AIs[AIIndex] = 2;
                                                string PointName = item.FieldName;
                                                if (item.ShowName != "")
                                                {
                                                    PointName = item.ShowName;
                                                }
                                                MessageModule message = new MessageModule()
                                                {
                                                    PointTypeEnum = 0,
                                                    DataBaseNum = DateBaseitem.DataBaseNum,
                                                    DataSheetNum = DataSheetitem.DataSheetNum,
                                                    FieldNum = item.FieldNum,
                                                    DateTime = DateTime.Now,
                                                    FieldName = PointName,
                                                    Description = $"資料庫:{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName} 的 {PointName} 點位，低於下限值:{item.MaxLimit}，目前數值: {data.Value.ToString("0.##")}"
                                                };
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelePhoneMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.LineFlag)
                                                {
                                                    HomeUserControl.LineNotifyMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelegramMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                CsvMethod.Save_Csv($"{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName}", message);
                                            }
                                        }
                                        #endregion
                                        #region 正常
                                        else
                                        {
                                            if (item.ResetFlag && AIs[AIIndex] != 0)
                                            {
                                                AIs[AIIndex] = 0;
                                                string PointName = item.FieldName;
                                                if (item.ShowName != "")
                                                {
                                                    PointName = item.ShowName;
                                                }
                                                MessageModule message = new MessageModule()
                                                {
                                                    PointTypeEnum = 0,
                                                    DataBaseNum = DateBaseitem.DataBaseNum,
                                                    DataSheetNum = DataSheetitem.DataSheetNum,
                                                    FieldNum = item.FieldNum,
                                                    DateTime = DateTime.Now,
                                                    FieldName = PointName,
                                                    Description = $"資料庫:{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName} 的 {PointName} 點位，上限值:{item.MaxLimit}、下限值:{item.MinLimit}，目前數值: {data.Value.ToString("0.##")}"
                                                };
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelePhoneMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.LineFlag)
                                                {
                                                    HomeUserControl.LineNotifyMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelegramMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                CsvMethod.Save_Csv($"{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName}", message);
                                            }
                                        }
                                        #endregion
                                    }
                                    AIIndex++;
                                    Thread.Sleep(10);
                                }
                                foreach (var item in DataSheetitem.DIs)
                                {
                                    if (!myWorkState)
                                    {
                                        break;
                                    }
                                    double? data = MS_SQL_Method.Search_Data(DateBaseitem.DataBaseName, DataSheetitem.DataSheetName, item.FieldName, DataSheetitem.TimeName, DataSheetitem.TimeTypeEnum);
                                    if (data.HasValue)
                                    {
                                        #region 異常觸發
                                        if (item.GeneralFlag != Convert.ToBoolean(data.Value))
                                        {
                                            if (!DIs[DIIndex])
                                            {
                                                string sendmessage = "異常觸發";
                                                string PointName = item.FieldName;
                                                if (item.ShowName != "")
                                                {
                                                    PointName = item.ShowName;
                                                }
                                                if (item.Message != "")
                                                {
                                                    sendmessage = item.Message;
                                                }
                                                MessageModule message = new MessageModule()
                                                {
                                                    PointTypeEnum = 1,
                                                    DataBaseNum = DateBaseitem.DataBaseNum,
                                                    DataSheetNum = DataSheetitem.DataSheetNum,
                                                    FieldNum = item.FieldNum,
                                                    DateTime = DateTime.Now,
                                                    FieldName = PointName,
                                                    Description = $"資料庫:{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName} 的 {PointName} 點位，{sendmessage}"
                                                };
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelePhoneMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.LineFlag)
                                                {
                                                    HomeUserControl.LineNotifyMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelegramMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                CsvMethod.Save_Csv($"{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName}", message);
                                                DIs[DIIndex] = true;
                                            }
                                        }
                                        #endregion
                                        #region 正常
                                        else
                                        {
                                            if (DIs[DIIndex])
                                            {
                                                if (item.ResetFlag)
                                                {
                                                    string PointName = item.FieldName;
                                                    if (item.ShowName != "")
                                                    {
                                                        PointName = item.ShowName;
                                                    }
                                                    MessageModule message = new MessageModule()
                                                    {
                                                        PointTypeEnum = 1,
                                                        DataBaseNum = DateBaseitem.DataBaseNum,
                                                        DataSheetNum = DataSheetitem.DataSheetNum,
                                                        FieldNum = item.FieldNum,
                                                        DateTime = DateTime.Now,
                                                        FieldName = PointName,
                                                        Description = $"資料庫:{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName} 的 {PointName} 點位，恢復正常"
                                                    };
                                                    if (SystemSetting.TelegramFlag)
                                                    {
                                                        HomeUserControl.TelePhoneMessage.Enqueue(message);
                                                        Thread.Sleep(80);
                                                    }
                                                    if (SystemSetting.LineFlag)
                                                    {
                                                        HomeUserControl.LineNotifyMessage.Enqueue(message);
                                                        Thread.Sleep(80);
                                                    }
                                                    if (SystemSetting.TelegramFlag)
                                                    {
                                                        HomeUserControl.TelegramMessage.Enqueue(message);
                                                        Thread.Sleep(80);
                                                    }
                                                    CsvMethod.Save_Csv($"{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName}", message);
                                                }
                                                DIs[DIIndex] = false;
                                            }
                                        }
                                        #endregion
                                    }
                                    DIIndex++;
                                    Thread.Sleep(10);
                                }
                                foreach (var item in DataSheetitem.Enumses)
                                {
                                    if (!myWorkState)
                                    {
                                        break;
                                    }
                                    double? data = MS_SQL_Method.Search_Data(DateBaseitem.DataBaseName, DataSheetitem.DataSheetName, item.FieldName, DataSheetitem.TimeName, DataSheetitem.TimeTypeEnum);
                                    if (data.HasValue)
                                    {
                                        #region 類型轉換觸發
                                        string[] describe = item.EnumsDescribe.Trim().Split(',');
                                        Dictionary<int, string> describepairs = new Dictionary<int, string>();
                                        foreach (var describeitem in describe)
                                        {
                                            string[] value = describeitem.Split('=');
                                            describepairs.Add(Convert.ToInt32(value[0].Trim()), value[1].Trim());
                                        }
                                        if (describepairs.ContainsKey(Convert.ToInt32(data.Value)))
                                        {
                                            if (Enumses[EnumsesIndex] != Convert.ToInt32(data.Value))
                                            {
                                                Enumses[EnumsesIndex] = Convert.ToInt32(data.Value);
                                                string PointName = item.FieldName;
                                                if (item.ShowName != "")
                                                {
                                                    PointName = item.ShowName;
                                                }
                                                MessageModule message = new MessageModule()
                                                {
                                                    PointTypeEnum = 2,
                                                    DataBaseNum = DateBaseitem.DataBaseNum,
                                                    DataSheetNum = DataSheetitem.DataSheetNum,
                                                    FieldNum = item.FieldNum,
                                                    DateTime = DateTime.Now,
                                                    FieldName = PointName,
                                                    Description = $"資料庫:{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName} 的 {PointName} 點位，{describepairs[Enumses[EnumsesIndex]]}"
                                                };
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelePhoneMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.LineFlag)
                                                {
                                                    HomeUserControl.LineNotifyMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelegramMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                CsvMethod.Save_Csv($"{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName}", message);
                                            }
                                        }
                                        #endregion
                                    }
                                    EnumsesIndex++;
                                    Thread.Sleep(10);
                                }
                            }
                        }
                    }
                    else if (My_SQL_Method != null)
                    {
                        foreach (var DateBaseitem in PointSettings)
                        {
                            foreach (var DataSheetitem in DateBaseitem.DataSheets)
                            {
                                foreach (var item in DataSheetitem.AIs)
                                {
                                    if (!myWorkState)
                                    {
                                        break;
                                    }
                                    double? data = My_SQL_Method.Search_Data(DateBaseitem.DataBaseName, DataSheetitem.DataSheetName, item.FieldName, DataSheetitem.TimeName, DataSheetitem.TimeTypeEnum);
                                    if (item.MaxLimit > item.MinLimit && data.HasValue)
                                    {
                                        #region 高於上限值
                                        if (data > item.MaxLimit && item.MaxFlag)
                                        {
                                            if (AIs[AIIndex] != 1)
                                            {
                                                AIs[AIIndex] = 1;
                                                string PointName = item.FieldName;
                                                if (item.ShowName != "")
                                                {
                                                    PointName = item.ShowName;
                                                }
                                                MessageModule message = new MessageModule()
                                                {
                                                    PointTypeEnum = 0,
                                                    DataBaseNum = DateBaseitem.DataBaseNum,
                                                    DataSheetNum = DataSheetitem.DataSheetNum,
                                                    FieldNum = item.FieldNum,
                                                    DateTime = DateTime.Now,
                                                    FieldName = PointName,
                                                    Description = $"資料庫:{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName} 的 {PointName} 點位，高於上限值:{item.MaxLimit}，目前數值: {data.Value.ToString("0.##")}"
                                                };
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelePhoneMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.LineFlag)
                                                {
                                                    HomeUserControl.LineNotifyMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelegramMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                CsvMethod.Save_Csv($"{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName}", message);
                                            }
                                        }
                                        #endregion
                                        #region 低於下限值
                                        else if (data < item.MinLimit && item.MinFlag)
                                        {
                                            if (AIs[AIIndex] != 2)
                                            {
                                                AIs[AIIndex] = 2;
                                                string PointName = item.FieldName;
                                                if (item.ShowName != "")
                                                {
                                                    PointName = item.ShowName;
                                                }
                                                MessageModule message = new MessageModule()
                                                {
                                                    PointTypeEnum = 0,
                                                    DataBaseNum = DateBaseitem.DataBaseNum,
                                                    DataSheetNum = DataSheetitem.DataSheetNum,
                                                    FieldNum = item.FieldNum,
                                                    DateTime = DateTime.Now,
                                                    FieldName = PointName,
                                                    Description = $"資料庫:{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName} 的 {PointName} 點位，低於下限值:{item.MaxLimit}，目前數值: {data.Value.ToString("0.##")}"
                                                };
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelePhoneMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.LineFlag)
                                                {
                                                    HomeUserControl.LineNotifyMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelegramMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                CsvMethod.Save_Csv($"{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName}", message);
                                            }
                                        }
                                        #endregion
                                        #region 正常
                                        else if (item.ResetFlag)
                                        {
                                            if (AIs[AIIndex] != 0)
                                            {
                                                AIs[AIIndex] = 0;
                                                string PointName = item.FieldName;
                                                if (item.ShowName != "")
                                                {
                                                    PointName = item.ShowName;
                                                }
                                                MessageModule message = new MessageModule()
                                                {
                                                    PointTypeEnum = 0,
                                                    DataBaseNum = DateBaseitem.DataBaseNum,
                                                    DataSheetNum = DataSheetitem.DataSheetNum,
                                                    FieldNum = item.FieldNum,
                                                    DateTime = DateTime.Now,
                                                    FieldName = PointName,
                                                    Description = $"資料庫:{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName} 的 {PointName} 點位，上限值:{item.MaxLimit}、下限值:{item.MinLimit}，目前數值: {data.Value.ToString("0.##")}"
                                                };
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelePhoneMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.LineFlag)
                                                {
                                                    HomeUserControl.LineNotifyMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelegramMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                CsvMethod.Save_Csv($"{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName}", message);
                                            }
                                        }
                                        #endregion
                                    }
                                    AIIndex++;
                                    Thread.Sleep(10);
                                }
                                foreach (var item in DataSheetitem.DIs)
                                {
                                    if (!myWorkState)
                                    {
                                        break;
                                    }
                                    double? data = My_SQL_Method.Search_Data(DateBaseitem.DataBaseName, DataSheetitem.DataSheetName, item.FieldName, DataSheetitem.TimeName, DataSheetitem.TimeTypeEnum);
                                    if (data.HasValue)
                                    {
                                        #region 異常觸發
                                        if (item.GeneralFlag != Convert.ToBoolean(data.Value))
                                        {
                                            if (!DIs[DIIndex])
                                            {
                                                string sendmessage = "異常觸發";
                                                string PointName = item.FieldName;
                                                if (item.ShowName != "")
                                                {
                                                    PointName = item.ShowName;
                                                }
                                                if (item.Message != "")
                                                {
                                                    sendmessage = item.Message;
                                                }
                                                MessageModule message = new MessageModule()
                                                {
                                                    PointTypeEnum = 1,
                                                    DataBaseNum = DateBaseitem.DataBaseNum,
                                                    DataSheetNum = DataSheetitem.DataSheetNum,
                                                    FieldNum = item.FieldNum,
                                                    DateTime = DateTime.Now,
                                                    FieldName = PointName,
                                                    Description = $"資料庫:{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName} 的 {PointName} 點位，{sendmessage}"
                                                };
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelePhoneMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.LineFlag)
                                                {
                                                    HomeUserControl.LineNotifyMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelegramMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                CsvMethod.Save_Csv($"{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName}", message);
                                                DIs[DIIndex] = true;
                                            }
                                        }
                                        #endregion
                                        #region 正常
                                        else
                                        {
                                            if (DIs[DIIndex])
                                            {
                                                if (item.ResetFlag)
                                                {
                                                    string PointName = item.FieldName;
                                                    if (item.ShowName != "")
                                                    {
                                                        PointName = item.ShowName;
                                                    }
                                                    MessageModule message = new MessageModule()
                                                    {
                                                        PointTypeEnum = 1,
                                                        DataBaseNum = DateBaseitem.DataBaseNum,
                                                        DataSheetNum = DataSheetitem.DataSheetNum,
                                                        FieldNum = item.FieldNum,
                                                        DateTime = DateTime.Now,
                                                        FieldName = PointName,
                                                        Description = $"資料庫:{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName} 的 {PointName} 點位，恢復正常"
                                                    };
                                                    if (SystemSetting.TelegramFlag)
                                                    {
                                                        HomeUserControl.TelePhoneMessage.Enqueue(message);
                                                        Thread.Sleep(80);
                                                    }
                                                    if (SystemSetting.LineFlag)
                                                    {
                                                        HomeUserControl.LineNotifyMessage.Enqueue(message);
                                                        Thread.Sleep(80);
                                                    }
                                                    if (SystemSetting.TelegramFlag)
                                                    {
                                                        HomeUserControl.TelegramMessage.Enqueue(message);
                                                        Thread.Sleep(80);
                                                    }
                                                    CsvMethod.Save_Csv($"{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName}", message);
                                                }
                                                DIs[DIIndex] = false;
                                            }
                                        }
                                        #endregion
                                    }
                                    DIIndex++;
                                    Thread.Sleep(10);
                                }
                                foreach (var item in DataSheetitem.Enumses)
                                {
                                    if (!myWorkState)
                                    {
                                        break;
                                    }
                                    double? data = My_SQL_Method.Search_Data(DateBaseitem.DataBaseName, DataSheetitem.DataSheetName, item.FieldName, DataSheetitem.TimeName, DataSheetitem.TimeTypeEnum);
                                    if (data.HasValue)
                                    {
                                        #region 類型轉換觸發
                                        string[] describe = item.EnumsDescribe.Trim().Split(',');
                                        Dictionary<int, string> describepairs = new Dictionary<int, string>();
                                        foreach (var describeitem in describe)
                                        {
                                            string[] value = describeitem.Split('=');
                                            describepairs.Add(Convert.ToInt32(value[0]), value[1]);
                                        }
                                        if (describepairs.ContainsKey(Convert.ToInt32(data.Value)))
                                        {
                                            if (Enumses[EnumsesIndex] != Convert.ToInt32(data.Value))
                                            {
                                                Enumses[EnumsesIndex] = Convert.ToInt32(data.Value);
                                                string PointName = item.FieldName;
                                                if (item.ShowName != "")
                                                {
                                                    PointName = item.ShowName;
                                                }
                                                MessageModule message = new MessageModule()
                                                {
                                                    PointTypeEnum = 2,
                                                    DataBaseNum = DateBaseitem.DataBaseNum,
                                                    DataSheetNum = DataSheetitem.DataSheetNum,
                                                    FieldNum = item.FieldNum,
                                                    DateTime = DateTime.Now,
                                                    FieldName = PointName,
                                                    Description = $"資料庫:{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName} 的 {PointName} 點位，{describepairs[Enumses[EnumsesIndex]]}"
                                                };
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelePhoneMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.LineFlag)
                                                {
                                                    HomeUserControl.LineNotifyMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                if (SystemSetting.TelegramFlag)
                                                {
                                                    HomeUserControl.TelegramMessage.Enqueue(message);
                                                    Thread.Sleep(80);
                                                }
                                                CsvMethod.Save_Csv($"{DateBaseitem.DataBaseName}.{DataSheetitem.DataSheetName}", message);
                                            }
                                        }
                                        #endregion
                                    }
                                    EnumsesIndex++;
                                    Thread.Sleep(10);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Thread.Sleep(80);
                }
            }
        }
    }
}
