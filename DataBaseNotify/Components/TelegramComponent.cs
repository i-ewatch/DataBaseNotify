using DataBaseNotify.Enums;
using DataBaseNotify.Methods;
using DataBaseNotify.Modules;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TelegramLibrary;

namespace DataBaseNotify.Components
{
    public partial class TelegramComponent : Field4Component
    {
        public TelegramComponent(Queue<MessageModule> messages)
        {
            InitializeComponent();
            TelegramMessage = messages;
        }

        public TelegramComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        protected override void AfterMyWorkStateChanged(object sender, EventArgs e)
        {
            if (myWorkState)
            {
                PointSettings = InitialMethod.Load_Point();
                GroupSettings = InitialMethod.Load_Group();
                TelegramBotSettings = InitialMethod.Load_Telegram();
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
        private void Analysis()
        {
            while (myWorkState)
            {
                TimeSpan timeSpan = DateTime.Now.Subtract(ComponentTime);
                if (timeSpan.TotalMilliseconds > 1000)
                {
                    try
                    {
                        while (TelegramMessage.Count > 0) //有訊息發送
                        {
                            MessageModule messageModule = TelegramMessage.Dequeue();
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
                                                            foreach (var Telegramitem in group.Telegram)
                                                            {
                                                                var telegram = TelegramBotSettings.SingleOrDefault(g => g.Index == Telegramitem);
                                                                if (telegram != null)
                                                                {
                                                                    using (TelegramBotClass telegramBot = new TelegramBotClass(telegram.URL, telegram.Chart_ID))
                                                                    {
                                                                        telegramBot.Send_Message($"時間:{messageModule.DateTime:yyyy/MM/dd HH:mm:ss}，{messageModule.Description}");
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
                                                            foreach (var Telegramitem in group.Telegram)
                                                            {
                                                                var telegram = TelegramBotSettings.SingleOrDefault(g => g.Index == Telegramitem);
                                                                if (telegram != null)
                                                                {
                                                                    using (TelegramBotClass telegramBot = new TelegramBotClass(telegram.URL, telegram.Chart_ID))
                                                                    {
                                                                        telegramBot.Send_Message($"時間:{messageModule.DateTime:yyyy/MM/dd HH:mm:ss}，{messageModule.Description}");
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
                                                            foreach (var Telegramitem in group.Telegram)
                                                            {
                                                                var telegram = TelegramBotSettings.SingleOrDefault(g => g.Index == Telegramitem);
                                                                if (telegram != null)
                                                                {
                                                                    using (TelegramBotClass telegramBot = new TelegramBotClass(telegram.URL, telegram.Chart_ID))
                                                                    {
                                                                        telegramBot.Send_Message($"時間:{messageModule.DateTime:yyyy/MM/dd HH:mm:ss}，{messageModule.Description}");
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
                        Log.Error(ex, "Telegram Bot錯誤");
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
