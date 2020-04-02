using System.Windows;
using System.Windows.Documents;
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
            HighlightWordInRichTextBox(msg, Color.FromRgb(0, 255, 120));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Information(string msg)
        {
            HighlightWordInRichTextBox(msg, Color.FromRgb(255, 255, 255));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Warning(string msg)
        {
            HighlightWordInRichTextBox(msg, Color.FromRgb(255,255,0));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Error(string msg)
        {
            HighlightWordInRichTextBox(msg, Color.FromRgb(255,0,0));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <param name="color"></param>
        public void HighlightWordInRichTextBox(string word, Color color)
        {
            if (word == null)
                return;

            View.Dispatcher.Invoke(() =>
            {
                var paragraph = new Paragraph();
                paragraph.Foreground = new SolidColorBrush(color);
                paragraph.Inlines.Add(word);
                paragraph.Margin = new Thickness(0, 0, 0, 2);

                View.RichTextBox.Document.Blocks.Add(paragraph);
                View.RichTextBox.ScrollToEnd();
            });
        }
    }
}