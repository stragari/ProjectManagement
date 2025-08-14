// ProjectManagement.Core/Ports/IProjectRepository.cs
using ProjectManagement.Core.Entities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Ports
{
    // Output port
    // Coreonly knows this interface, not the implementation
    public interface IProjectRepository
    {
        Task<Project> GetByIdAsync(Guid id);
        Task AddAsync(Project project);
        //Task<IEnumerable<Project>> GetAllAsync();
        Task<List<Project>> GetAllAsync();

        Task AddTaskAsync(Guid projectId, ProjectTask task);
        Task MarkTaskAsCompletedAsync(Guid projectId, Guid taskId);
    }

}