using DataBaseNotify.Enums;
using DataBaseNotify.Methods;
using DataBaseNotify.Modules;
using LineNotifyLibrary;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace DataBaseNotify.Components
{
    public partial class LineComponent : Field4Component
    {
        public LineComponent(Queue<MessageModule> messages)
        {
            InitializeComponent();
            LineNotifyMessage = messages;
        }

        public LineComponent(IContainer container)
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
                LineNotifySettings = InitialMethod.Load_LineNotify();
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
                        while (LineNotifyMessage.Count > 0)//有訊息發送
                        {
                            MessageModule messageModule = LineNotifyMessage.Dequeue();
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
                                                            foreach (var Lineitem in group.Line)
                                                            {
                                                                var line = LineNotifySettings.SingleOrDefault(g => g.Index == Lineitem);
                                                                if (line != null)
                                                                {
                                                                    using (LineNotifyClass lineNotify = new LineNotifyClass(line.LineToken))
                                                                    {
                                                                        lineNotify.LineNotifyFunction($"時間:{messageModule.DateTime:yyyy/MM/dd HH:mm:ss}，{messageModule.Description}");
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
                                                            foreach (var Lineitem in group.Line)
                                                            {
                                                                var line = LineNotifySettings.SingleOrDefault(g => g.Index == Lineitem);
                                                                if (line != null)
                                                                {
                                                                    using (LineNotifyClass lineNotify = new LineNotifyClass(line.LineToken))
                                                                    {
                                                                        lineNotify.LineNotifyFunction($"時間:{messageModule.DateTime:yyyy/MM/dd HH:mm:ss}，{messageModule.Description}");
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
                                                            foreach (var Lineitem in group.Line)
                                                            {
                                                                var line = LineNotifySettings.SingleOrDefault(g => g.Index == Lineitem);
                                                                if (line != null)
                                                                {
                                                                    using (LineNotifyClass lineNotify = new LineNotifyClass(line.LineToken))
                                                                    {
                                                                        lineNotify.LineNotifyFunction($"時間:{messageModule.DateTime:yyyy/MM/dd HH:mm:ss}，{messageModule.Description}");
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
                        Log.Error(ex, "Line Notify錯誤");
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
