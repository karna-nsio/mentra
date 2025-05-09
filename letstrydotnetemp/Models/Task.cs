using System;

namespace letstrydotnetemp.Models
{
    public class Task
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CreatedBy { get; set; }  // Mentor's ID
        public Guid AssignedTo { get; set; } // Assistant's ID
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TaskStatus CurrentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum TaskStatus
    {
        Pending,
        Doing,
        Done
    }
} 