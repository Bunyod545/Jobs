using System.Windows;

namespace Jobs.Manager.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class DeleteHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsAreYouSure()
        {
            var deleteResult = MessageBox.Show("Are you sure delete?", "Delete confirm", MessageBoxButton.YesNo);
            return deleteResult == MessageBoxResult.Yes;
        }
    }
}
