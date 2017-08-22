using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;
using AriaNet;
using AriaNet.Attributes;
using BgetWpf.Model;

namespace BgetWpf.Controller
{
    public class TaskHandler
    {
        // Task ID is key and name is value
        public Dictionary<string,string> CurrentTaskIdList { get; set; }

        private AriaManager _AriaManager;

        public TaskHandler()
        {

            _AriaManager = Properties.Settings.Default.UseExternalAria
                ? new AriaManager(Properties.Settings.Default.ExternalRpc)
                : new AriaManager();
        }

        public async Task<List<BgetTaskBinding>> UpdateTaskInfo()
        {
            var taskStatusList = new List<BgetTaskBinding> ();

            foreach (var taskInfo in CurrentTaskIdList)
            {
                var status = await _AriaManager.GetStatus(taskInfo.Key);
                taskStatusList.Add(new BgetTaskBinding()
                {
                    TaskID = taskInfo.Key,

                    TaskProgressValue =
                        $"{Convert.ToDouble(status.CompletedLength) / Convert.ToDouble(status.TotalLength)}%",

                    TaskStatusColor = _GetColorFormText(status.Status),
                    TaskStatusText = status.Status,
                    TaskTitle = taskInfo.Value
                });
            }

            return taskStatusList;
        }

        private SolidColorBrush _GetColorFormText(string status)
        {
            switch (status)
            {
                case "active":
                {
                    return new SolidColorBrush(Colors.YellowGreen);
                }
                case "waiting":
                {
                    return new SolidColorBrush(Colors.Yellow);
                }
                case "paused":
                {
                    return  new SolidColorBrush(Colors.Blue);
                }
                case "complete":
                {
                    return new SolidColorBrush(Colors.Green);
                }
                case "error":
                {
                    return new SolidColorBrush(Colors.Red);
                }
                case "removed":
                {
                    return new SolidColorBrush(Colors.Gray);
                }
                default:
                {
                    return new SolidColorBrush(Colors.Black);
                }
            }
        }
    }
}
