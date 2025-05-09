using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using letstrydotnetemp.Models;

namespace letstrydotnetemp.Repositories
{
    public interface ITaskRepository
    {
        Task<Models.Task> CreateTaskAsync(Models.Task task);
        Task<IEnumerable<Models.Task>> GetAllTasksAsync();
        Task<Models.Task> GetTaskByIdAsync(Guid taskId);
        Task<IEnumerable<Models.Task>> GetTasksByAssignedToAsync(Guid userId);
        Task<IEnumerable<Models.Task>> GetTasksByCreatedByAsync(Guid userId);
        Task<Models.Task> UpdateTaskStatusAsync(Models.Task task);
    }
} 