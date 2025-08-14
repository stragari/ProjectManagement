using ProjectManagement.Core.Ports;
using ProjectManagement.Core.Entities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Guid> CreateProjectAsync(string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentNullException("The name project not be null or empty", nameof(projectName));
            }

            var newProject = new ProjectManagement.Core.Entities.Project
            {
                Id = Guid.NewGuid(),
                Name = projectName,
                CreationDate = DateTime.Now,
            };

            await _projectRepository.AddAsync(newProject);

            return newProject.Id;
        }

        public async Task<List<ProjectManagement.Core.Entities.Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllAsync();
        }

        public async Task AddTaskToProjectAsync(Guid projectId, string taskTitle)
        {
            if (string.IsNullOrWhiteSpace(taskTitle))
            {
                throw new ArgumentException("The title of the task not be null or empty", nameof(taskTitle));
            }

            var newTask = new ProjectManagement.Core.Entities.ProjectTask
            {
                Id = Guid.NewGuid(),
                Title = taskTitle,
            };

            await _projectRepository.AddTaskAsync(projectId, newTask);
        }

        public async Task<Project> GetProjectByIdAsync(Guid id)
        {
            return await _projectRepository.GetByIdAsync(id);
        }

        public async Task MarkTaskAsCompletedAsync(Guid projectId, Guid taskId)
        {
            await _projectRepository.MarkTaskAsCompletedAsync(projectId, taskId);
        }

    }
}