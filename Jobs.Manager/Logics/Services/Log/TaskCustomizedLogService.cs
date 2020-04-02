using System;
using System.Windows.Controls;
using System.Windows.Threading;
using Jobs.Manager.Views.Tasks;
using Jobs.Tasks.Common.Logics.Services.Log;

namespace Jobs.Manager.Logics.Services.Log
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskCustomizedLogService : ITaskCustomizedLogService
    {
        /// <summary>
        /// 
        /// </summary>
        public TasksView View { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public TaskCustomizedLogService(TasksView view)
        {
            View = view;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logAction"></param>
        public void Show(Action<RichTextBox> logAction)
        {
            View.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() => logAction?.Invoke(View.RichTextBox)));
        }
    }
}
