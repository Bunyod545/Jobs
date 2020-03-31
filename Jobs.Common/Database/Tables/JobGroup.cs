﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Jobs.Common.Database.Tables
{
    /// <summary>
    /// 
    /// </summary>
    public class JobGroup
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
        public List<Job> Jobs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<JobGroup> ChildGroups { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public JobGroup()
        {
            Jobs = new List<Job>();
            ChildGroups = new List<JobGroup>();
        }
    }
}