//Represents a task in the to-do list application.
namespace TaskApi.Models
{
    public class TodoTask
    {
        // Unique identifier for the task.
        public int Id { get; set; }

        // Title of the task.
        public string Title { get; set; } = string.Empty;

        // Description of the task.(optional)
        public string Description { get; set; } = string.Empty;

        // Indicates whether the task is completed.
        public bool IsCompleted { get; set; } = false;

        //Timestamp when the task was created.(UTC)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Timestamp when the task was last updated.(UTC, nullable if never updated)
        public DateTime? UpdatedAt { get; set; }
    }
}
