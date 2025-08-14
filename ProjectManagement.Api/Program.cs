using MongoDB.Driver;
using ProjectManagement.Core.Ports;
using ProjectManagement.Core.UseCases;
using ProjectManagement.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add the Swagger service to generate the API specification.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
// These are the services.Add...
builder.Services.AddControllers();

// 1. MongoDB Connection (infraestructure detail)
builder.Services.AddSingleton<IMongoClient>(
    new MongoClient(builder.Configuration.GetConnectionString("MongoDB")));

MongoDbConfiguration.Configure();

// 2. Repository registration (output adapter)
// When IProjectRepository is requested, the container will provide a MongoProjectRepository.
// builder.Services.AddSingleton<IProjectRepository>(
//     new MongoProjectRepository(builder.Services.GetRequired<IMongoClient>()));
builder.Services.AddSingleton<IProjectRepository, MongoProjectRepository>();

// 3. Core service registration (input port)
// When IProjectService is requested, the container will provide a ProjectService.
// The container will automatically handle injecting the IProjectRepository.
builder.Services.AddScoped<IProjectService, ProjectService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Use the Swagger middleware to serve the JSON specification.
    app.UseSwagger();
    
    // Use the SwaggerUI middleware to serve the interactive user interface.
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
