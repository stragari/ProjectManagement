using ProjectManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases
{
    // Input port, define bussisnes logic
    public interface IProjectService
    {
        Task<Guid> CreateProjectAsync(string projectName);
        Task<List<ProjectManagement.Core.Entities.Project>> GetAllProjectsAsync();
        Task AddTaskToProjectAsync(Guid projectId, string taskTitle);
        Task<Project> GetProjectByIdAsync(Guid id);
        Task MarkTaskAsCompletedAsync(Guid projectId, Guid taskId);
    
    }
}