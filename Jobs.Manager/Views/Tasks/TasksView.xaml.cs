using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using Jobs.Common.Database;
using Jobs.Common.Database.Tables;
using Jobs.Common.Logics.Jobs;
using Jobs.Manager.Logics.Services.Log;
using Jobs.Tasks.Common.Logics.Services.Log;
using RestSharp;
using Task = Jobs.Common.Database.Tables.Task;
using ThreadTask = System.Threading.Tasks.Task;

namespace Jobs.Manager.Views.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TasksView
    {
        /// <summary>
        /// 
        /// </summary>
        public Job Job { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TasksView()
        {
            InitializeComponent();

            //var groups = JobsDatabase.JobGroups.Include(i => i.Jobs).FindAll().ToList();
            //var jobs = JobsDatabase.Jobs.FindAll().ToList();
            //var tasks = JobsDatabase.Tasks.FindAll().ToList();
            //    .ToList().FirstOrDefault();
            //Job = group.Jobs.FirstOrDefault();
            //var task = Job.Tasks.FirstOrDefault();

            //TasksListBox.Items.Add(task);
            //Job.Tasks.Add(task);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExecute_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var jobExecuter = new JobExecuter(Job);
            jobExecuter.Container.Register<ITaskLogService>(new TaskLogService(this));
            jobExecuter.Initialize();
            ExecuteButton.IsEnabled = false;

            ThreadTask.Run(() => jobExecuter.Execute()).ContinueWith(OnExecuteFinished);
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnExecuteFinished(Task<bool> result)
        {
            Dispatcher.Invoke(() => { ExecuteButton.IsEnabled = true; });
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

            Dispatcher.Invoke(() =>
            {
                var paragraph = new Paragraph();
                paragraph.Foreground = new SolidColorBrush(color);
                paragraph.Inlines.Add(word);
                paragraph.Margin = new Thickness(0, 0, 0, 2);

                RichTextBox.Document.Blocks.Add(paragraph);
                RichTextBox.ScrollToEnd();
            });
        }
    }
}
