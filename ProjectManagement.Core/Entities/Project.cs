using System;
using System.Collections.Generic;

namespace ProjectManagement.Core.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();    
    }
}