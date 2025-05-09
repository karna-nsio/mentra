using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using letstrydotnetemp.DTOs;
using letstrydotnetemp.Models;

namespace letstrydotnetemp.Services
{
    public interface ITaskService
    {
        Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto createTaskDto, Guid mentorId);
        Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync();
        Task<TaskResponseDto> GetTaskByIdAsync(Guid taskId);
        Task<TaskResponseDto> UpdateTaskStatusAsync(Guid taskId, UpdateTaskStatusDto updateTaskStatusDto, Guid assistantId);
        Task<IEnumerable<TaskResponseDto>> GetAssignedTasksAsync(Guid userId);
        Task<IEnumerable<TaskResponseDto>> GetCreatedTasksAsync(Guid userId);
    }
} 