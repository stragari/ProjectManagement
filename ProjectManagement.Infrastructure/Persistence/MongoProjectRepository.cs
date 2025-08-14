using MongoDB.Driver;
using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Ports;

using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Persistence
{
    public class MongoProjectRepository : IProjectRepository
    {
        private readonly IMongoCollection<Project> _projects;

        public MongoProjectRepository(IMongoClient client)
        {
            var database = client.GetDatabase("ProjectManagementDB");
            _projects = database.GetCollection<Project>("Projects");
        }

        public async Task<Project> GetByIdAsync(Guid id)
        {
            // Query to MongoDB
            return await _projects.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Project project)
        {
            await _projects.InsertOneAsync(project);
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _projects.Find(p => true).ToListAsync();
        }

        public async Task AddTaskAsync(Guid projectId, ProjectTask task)
        {
            var filter = Builders<Project>.Filter.Eq(p => p.Id, projectId);
            // var update = Builders<Project>.Update.Push(p => p.Tasks, task);
            // var update = Builders<Project>.Update.Push(nameof(Project.Tasks), task);
            var update = Builders<Project>.Update.Push("Tasks", task);

            await _projects.UpdateOneAsync(filter, update);
        }

        public async Task MarkTaskAsCompletedAsync(Guid projectId, Guid taskId)
        {
            var filter = Builders<Project>.Filter.Eq(p => p.Id, projectId) &
                         Builders<Project>.Filter.ElemMatch(p => p.Tasks, t => t.Id == taskId);

            var update = Builders<Project>.Update.Set("Tasks.$.IsCompleted", true);

            await _projects.UpdateOneAsync(filter, update);
        }
    
    }
}