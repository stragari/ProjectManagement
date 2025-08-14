using System;

namespace ProjectManagement.Core.Entities
{
    public class ProjectTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}