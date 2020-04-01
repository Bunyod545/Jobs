using System.Windows.Media;
using Jobs.Manager.Views.Tasks;
using Jobs.Tasks.Common.Logics.Services.Log;

namespace Jobs.Manager.Logics.Services.Log
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskLogService : ITaskLogService
    {
        /// <summary>
        /// 
        /// </summary>
        public TasksView View { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public TaskLogService(TasksView view)
        {
            View = view;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Success(string msg)
        {
            View.HighlightWordInRichTextBox(msg, Color.FromRgb(0, 255, 120));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Information(string msg)
        {
            View.HighlightWordInRichTextBox(msg, Color.FromRgb(255, 255, 255));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Warning(string msg)
        {
            View.HighlightWordInRichTextBox(msg, Color.FromRgb(255,255,0));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Error(string msg)
        {
            View.HighlightWordInRichTextBox(msg, Color.FromRgb(255,0,0));
        }
    }
}