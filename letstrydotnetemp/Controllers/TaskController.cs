using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using letstrydotnetemp.DTOs;
using letstrydotnetemp.Models;
using letstrydotnetemp.Data;

namespace letstrydotnetemp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<TaskResponseDto>> CreateTask(CreateTaskDto createTaskDto)
        {
            var task = new Models.Task
            {
                TaskId = Guid.NewGuid(),
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                CreatedBy = createTaskDto.CreatedBy,
                AssignedTo = createTaskDto.AssignedTo,
                StartTime = createTaskDto.StartTime,
                EndTime = createTaskDto.EndTime,
                CurrentStatus = Models.TaskStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var responseDto = new TaskResponseDto
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

            return CreatedAtAction(nameof(GetTaskById), new { id = task.TaskId }, responseDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetAllTasks()
        {
            var tasks = new List<TaskResponseDto>
            {
                new TaskResponseDto
                {
                    TaskId = Guid.NewGuid(),
                    Title = "Complete Project Documentation",
                    Description = "Write comprehensive documentation for the current project",
                    CreatedBy = Guid.NewGuid(),
                    AssignedTo = Guid.NewGuid(),
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddDays(7),
                    CurrentStatus = Models.TaskStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                },
                new TaskResponseDto
                {
                    TaskId = Guid.NewGuid(),
                    Title = "Code Review",
                    Description = "Review pull requests for the main branch",
                    CreatedBy = Guid.NewGuid(),
                    AssignedTo = Guid.NewGuid(),
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddDays(2),
                    CurrentStatus = Models.TaskStatus.Doing,
                    CreatedAt = DateTime.UtcNow.AddDays(-1)
                },
                new TaskResponseDto
                {
                    TaskId = Guid.NewGuid(),
                    Title = "Bug Fix Implementation",
                    Description = "Fix reported bugs in the authentication module",
                    CreatedBy = Guid.NewGuid(),
                    AssignedTo = Guid.NewGuid(),
                    StartTime = DateTime.UtcNow.AddDays(-2),
                    EndTime = DateTime.UtcNow.AddDays(1),
                    CurrentStatus = Models.TaskStatus.Done,
                    CreatedAt = DateTime.UtcNow.AddDays(-3)
                }
            };

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponseDto>> GetTaskById(Guid id)
        {
            // TODO: Implement get task by id logic
            return Ok(id);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<TaskResponseDto>> UpdateTaskStatus(Guid id, UpdateTaskStatusDto updateTaskStatusDto)
        {
            // TODO: Implement update task status logic
            throw new NotImplementedException();
        }

        [HttpGet("assigned/{userId}")]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetAssignedTasks(Guid userId)
        {
            // TODO: Implement get assigned tasks logic
            throw new NotImplementedException();
        }

        [HttpGet("created/{userId}")]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetCreatedTasks(Guid userId)
        {
            // TODO: Implement get created tasks logic
            throw new NotImplementedException();
        }
    }
} 