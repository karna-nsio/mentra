using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using letstrydotnetemp.DTOs;
using letstrydotnetemp.Models;
using letstrydotnetemp.Services;

namespace letstrydotnetemp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<ActionResult<TaskResponseDto>> CreateTask(CreateTaskDto createTaskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Trim string inputs
            createTaskDto.Title = createTaskDto.Title?.Trim();
            createTaskDto.Description = createTaskDto.Description?.Trim();
            createTaskDto.Priority = createTaskDto.Priority?.Trim();

            // Validate due date if provided
            if (createTaskDto.DueDate.HasValue && createTaskDto.DueDate.Value < DateTime.UtcNow)
            {
                ModelState.AddModelError("DueDate", "Due date cannot be in the past");
                return BadRequest(ModelState);
            }

            // TODO: Get mentorId from authenticated user
            var mentorId = createTaskDto.CreatedBy;
            var task = await _taskService.CreateTaskAsync(createTaskDto, mentorId);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.TaskId }, task);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskResponseDto>> GetTaskById(Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<TaskResponseDto>> UpdateTaskStatus(Guid id, UpdateTaskStatusDto updateTaskStatusDto)
        {
            // TODO: Get assistantId from authenticated user
            var assistantId = Guid.NewGuid(); // Temporary until authentication is implemented
            var task = await _taskService.UpdateTaskStatusAsync(id, updateTaskStatusDto, assistantId);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpGet("assigned/{userId}")]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetAssignedTasks(Guid userId)
        {
            var tasks = await _taskService.GetAssignedTasksAsync(userId);
            return Ok(tasks);
        }

        [HttpGet("created/{userId}")]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetCreatedTasks(Guid userId)
        {
            var tasks = await _taskService.GetCreatedTasksAsync(userId);
            return Ok(tasks);
        }
    }
} 