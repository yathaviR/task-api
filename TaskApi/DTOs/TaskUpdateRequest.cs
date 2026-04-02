namespace TaskApi.DTOs
{
    //Data Transfer Object for updating an existing task
    //Supports patial updates (only fields provided will be updated)
    public class TaskUpdateRequest
    {
        //New title for the task (optional)
        public string? Title { get; set; }

        //New description for the task (optional)
        public string? Description { get; set; }

        //New completion status for the task (optional)
        public bool? IsCompleted { get; set; }
    }
}
