namespace TaskApi.DTOs
{
    //Data Transfer Object (DTO) for creating a new task
    //Validates and structures input from API clients
    public class TaskCreateRequest
    {
        //Title of the task (required)
        public string Title { get; set; }=string.Empty;

        //Detailed description of the task (optional)
        public string Description { get; set; }

    }
}
