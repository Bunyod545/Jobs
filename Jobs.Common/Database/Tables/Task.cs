using RestSharp;

namespace Jobs.Common.Database.Tables
{
    /// <summary>
    /// 
    /// </summary>
    public class Task
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TaskLibraryPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TaskClassName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JsonObject TaskData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
