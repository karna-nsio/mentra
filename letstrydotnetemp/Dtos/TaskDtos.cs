using System;
using System.ComponentModel.DataAnnotations;
using letstrydotnetemp.Models;

namespace letstrydotnetemp.DTOs
{
    public class CreateTaskDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_.,!?()]+$", ErrorMessage = "Title contains invalid characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 1000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "CreatedBy is required")]
        public Guid CreatedBy { get; set; }

        public Guid? AssignedTo { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? DueDate { get; set; }
        public string Priority { get; set; }
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
        public Guid? AssignedTo { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? DueDate { get; set; }
        public string Priority { get; set; }
        public letstrydotnetemp.Models.TaskStatus CurrentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}