using System;
using letstrydotnetemp.Models;

namespace letstrydotnetemp.DTOs
{
    public class CreateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid AssignedTo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class UpdateTaskStatusDto
    {
        public letstrydotnetemp.Models.TaskStatus NewStatus { get; set; }
    }

    public class TaskResponseDto
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid AssignedTo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public letstrydotnetemp.Models.TaskStatus CurrentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 