using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using letstrydotnetemp.DTOs;
using letstrydotnetemp.Models;
using letstrydotnetemp.Repositories;

namespace letstrydotnetemp.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto createTaskDto, Guid mentorId)
        {
            var task = new Models.Task
            {
                TaskId = Guid.NewGuid(),
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                CreatedBy = mentorId,
                AssignedTo = createTaskDto.AssignedTo,
                StartTime = createTaskDto.StartTime,
                EndTime = createTaskDto.EndTime,
                CurrentStatus = Models.TaskStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            var createdTask = await _taskRepository.CreateTaskAsync(task);

            return new TaskResponseDto
            {
                TaskId = createdTask.TaskId,
                Title = createdTask.Title,
                Description = createdTask.Description,
                CreatedBy = createdTask.CreatedBy,
                AssignedTo = createdTask.AssignedTo,
                StartTime = createdTask.StartTime,
                EndTime = createdTask.EndTime,
                CurrentStatus = createdTask.CurrentStatus,
                CreatedAt = createdTask.CreatedAt
            };
        }

        public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            return tasks.Select(t => new TaskResponseDto
            {
                TaskId = t.TaskId,
                Title = t.Title,
                Description = t.Description,
                CreatedBy = t.CreatedBy,
                AssignedTo = t.AssignedTo,
                StartTime = t.StartTime,
                EndTime = t.EndTime,
                CurrentStatus = t.CurrentStatus,
                CreatedAt = t.CreatedAt
            });
        }

        public async Task<TaskResponseDto> GetTaskByIdAsync(Guid taskId)
        {
            var task = await _taskRepository.GetTaskByIdAsync(taskId);
            if (task == null)
                return null;

            return new TaskResponseDto
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                CreatedBy = task.CreatedBy,
                AssignedTo = task.AssignedTo,
                StartTime = task.StartTime,
                EndTime = task.EndTime,
                CurrentStatus = task.CurrentStatus,
                CreatedAt = task.CreatedAt
            };
        }

        public async Task<TaskResponseDto> UpdateTaskStatusAsync(Guid taskId, UpdateTaskStatusDto updateTaskStatusDto, Guid assistantId)
        {
            var task = await _taskRepository.GetTaskByIdAsync(taskId);
            if (task == null || task.AssignedTo != assistantId)
                return null;

            task.CurrentStatus = updateTaskStatusDto.NewStatus;
            await _taskRepository.UpdateTaskStatusAsync(task);

            return new TaskResponseDto
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                CreatedBy = task.CreatedBy,
                AssignedTo = task.AssignedTo,
                StartTime = task.StartTime,
                EndTime = task.EndTime,
                CurrentStatus = task.CurrentStatus,
                CreatedAt = task.CreatedAt
            };
        }

        public async Task<IEnumerable<TaskResponseDto>> GetAssignedTasksAsync(Guid userId)
        {
            var tasks = await _taskRepository.GetTasksByAssignedToAsync(userId);
            return tasks.Select(t => new TaskResponseDto
            {
                TaskId = t.TaskId,
                Title = t.Title,
                Description = t.Description,
                CreatedBy = t.CreatedBy,
                AssignedTo = t.AssignedTo,
                StartTime = t.StartTime,
                EndTime = t.EndTime,
                CurrentStatus = t.CurrentStatus,
                CreatedAt = t.CreatedAt
            });
        }

        public async Task<IEnumerable<TaskResponseDto>> GetCreatedTasksAsync(Guid userId)
        {
            var tasks = await _taskRepository.GetTasksByCreatedByAsync(userId);
            return tasks.Select(t => new TaskResponseDto
            {
                TaskId = t.TaskId,
                Title = t.Title,
                Description = t.Description,
                CreatedBy = t.CreatedBy,
                AssignedTo = t.AssignedTo,
                StartTime = t.StartTime,
                EndTime = t.EndTime,
                CurrentStatus = t.CurrentStatus,
                CreatedAt = t.CreatedAt
            });
        }
    }
} 