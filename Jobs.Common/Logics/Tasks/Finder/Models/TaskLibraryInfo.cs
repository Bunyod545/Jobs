using System.IO;

namespace Jobs.Common.Logics.Tasks.Finder.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskLibraryInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string LibraryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LibraryPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        public TaskLibraryInfo(FileInfo file)
        {
            LibraryName = file.Name;
            LibraryPath = file.FullName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return LibraryName;
        }
    }
}
