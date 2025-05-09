using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using letstrydotnetemp.Data;
using letstrydotnetemp.Models;

namespace letstrydotnetemp.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Models.Task> CreateTaskAsync(Models.Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<IEnumerable<Models.Task>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Models.Task> GetTaskByIdAsync(Guid taskId)
        {
            return await _context.Tasks.FindAsync(taskId);
        }

        public async Task<IEnumerable<Models.Task>> GetTasksByAssignedToAsync(Guid userId)
        {
            return await _context.Tasks
                .Where(t => t.AssignedTo == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Models.Task>> GetTasksByCreatedByAsync(Guid userId)
        {
            return await _context.Tasks
                .Where(t => t.CreatedBy == userId)
                .ToListAsync();
        }

        public async Task<Models.Task> UpdateTaskStatusAsync(Models.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
} 