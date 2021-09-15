using DataBaseNotify.Enums;
using DataBaseNotify.Methods;
using DataBaseNotify.Modules;
using Serilog;
using SMSLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace DataBaseNotify.Components
{
    public partial class SMSComponent : Field4Component
    {
        public SMSComponent(Queue<MessageModule> messages)
        {
            InitializeComponent();
            TelePhoneMessage = messages;
        }

        public SMSComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void AfterMyWorkStateChanged(object sender, EventArgs e)
        {
            if (myWorkState)
            {
                SystemSetting = InitialMethod.Load_System();
                PointSettings = InitialMethod.Load_Point();
                GroupSettings = InitialMethod.Load_Group();
                TelePhoneSettings = InitialMethod.Load_Telephone();
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
        /// <summary>
        /// 分析
        /// </summary>
        protected void Analysis()
        {
            while (myWorkState)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(ComponentTime);
                if (timeSpan.TotalMilliseconds > 1000)
                {
                    try
                    {
                        while (TelePhoneMessage.Count > 0)
                        {
                            MessageModule messageModule = TelePhoneMessage.Dequeue();
                            PointTypeEnum pointTypeEnum = (PointTypeEnum)messageModule.PointTypeEnum;
                            switch (pointTypeEnum)
                            {
                                case PointTypeEnum.AI:
                                    {
                                        var point = PointSettings.SingleOrDefault(g => g.DataBaseNum == messageModule.DataBaseNum);
                                        if (point != null)
                                        {
                                            var Table = point.DataSheets.SingleOrDefault(g => g.DataSheetNum == messageModule.DataSheetNum);
                                            if (Table != null)
                                            {
                                                var data = Table.AIs.SingleOrDefault(g => g.FieldNum == messageModule.FieldNum);
                                                if (data != null)
                                                {
                                                    foreach (var groupitem in data.Group)
                                                    {
                                                        var group = GroupSettings.SingleOrDefault(g => g.Index == groupitem);
                                                        if (group != null)
                                                        {
                                                            foreach (var Telephoneitem in group.TelePhone)
                                                            {
                                                                var telephone = TelePhoneSettings.SingleOrDefault(g => g.Index == Telephoneitem);
                                                                if (telephone != null)
                                                                {
                                                                    using (SMSClass SMS = new SMSClass(Rs232, SystemSetting.TelephoneCOM))
                                                                    {
                                                                        SMS.SMS_Send(telephone.PhoneNumber, $"時間:{messageModule.DateTime:yyyy/MM/dd HH:mm:ss}，{messageModule.Description}");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case PointTypeEnum.DI:
                                    {
                                        var point = PointSettings.SingleOrDefault(g => g.DataBaseNum == messageModule.DataBaseNum);
                                        if (point != null)
                                        {
                                            var Table = point.DataSheets.SingleOrDefault(g => g.DataSheetNum == messageModule.DataSheetNum);
                                            if (Table != null)
                                            {
                                                var data = Table.DIs.SingleOrDefault(g => g.FieldNum == messageModule.FieldNum);
                                                if (data != null)
                                                {
                                                    foreach (var groupitem in data.Group)
                                                    {
                                                        var group = GroupSettings.SingleOrDefault(g => g.Index == groupitem);
                                                        if (group != null)
                                                        {
                                                            foreach (var Telephoneitem in group.TelePhone)
                                                            {
                                                                var telephone = TelePhoneSettings.SingleOrDefault(g => g.Index == Telephoneitem);
                                                                if (telephone != null)
                                                                {
                                                                    using (SMSClass SMS = new SMSClass(Rs232, SystemSetting.TelephoneCOM))
                                                                    {
                                                                        SMS.SMS_Send(telephone.PhoneNumber, $"時間:{messageModule.DateTime:yyyy/MM/dd HH:mm:ss}，{messageModule.Description}");
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case PointTypeEnum.Enums:
                                    {
                                        var point = PointSettings.SingleOrDefault(g => g.DataBaseNum == messageModule.DataBaseNum);
                                        if (point != null)
                                        {
                                            var Table = point.DataSheets.SingleOrDefault(g => g.DataSheetNum == messageModule.DataSheetNum);
                                            if (Table != null)
                                            {
                                                var data = Table.Enumses.SingleOrDefault(g => g.FieldNum == messageModule.FieldNum);
                                                if (data != null)
                                                {
                                                    foreach (var groupitem in data.Group)
                                                    {
                                                        var group = GroupSettings.SingleOrDefault(g => g.Index == groupitem);
                                                        if (group != null)
                                                        {
                                                            foreach (var Telephoneitem in group.TelePhone)
                                                            {
                                                                var telephone = TelePhoneSettings.SingleOrDefault(g => g.Index == Telephoneitem);
                                                                if (telephone != null)
                                                                {
                                                                    using (SMSClass SMS = new SMSClass(Rs232, SystemSetting.TelephoneCOM))
                                                                    {
                                                                        SMS.SMS_Send(telephone.PhoneNumber, $"時間:{messageModule.DateTime:yyyy/MM/dd HH:mm:ss}，{messageModule.Description}");
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                            Thread.Sleep(80);
                        }
                        ComponentTime = DateTime.Now;
                    }
                    catch (ThreadAbortException) { }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "SMS 簡訊錯誤");
                        ComponentTime = DateTime.Now;
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
