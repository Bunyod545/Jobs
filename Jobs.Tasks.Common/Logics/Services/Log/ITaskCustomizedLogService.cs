using System;
using System.Windows.Controls;

namespace Jobs.Tasks.Common.Logics.Services.Log
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaskCustomizedLogService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logAction"></param>
        void Show(Action<RichTextBox> logAction);
    }
}