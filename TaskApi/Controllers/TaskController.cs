using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TaskApi.Data;
using TaskApi.DTOs;
using TaskApi.Models;

namespace TaskApi.Controllers
{
    /// API Controller for handling tasks
    /// Provides endpoints for CRUD operations on tasks

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TaskController : ControllerBase
    {
        private readonly TaskDbContext _context;
        private readonly ILogger<TaskController> _logger;

        public TaskController(TaskDbContext context, ILogger<TaskController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Get all tasks
        ///<returns>List of all tasks, Ordered by Creation Date (newest First)</returns>
        ///<response code="200">Returns the list of tasks</response>
        ///<response code="500">Internal Server Error</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetAllTasks()
        {
            try
            {
                _logger.LogInformation("Fetching all tasks");
                var tasks = await _context.Tasks
                    .OrderByDescending(t => t.CreatedAt)
                    .ToListAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching tasks");
                return StatusCode(500, new { message = "Error fetching tasks", error = ex.Message });
            }
        }

        //Get a specific Task by ID
        ///<param name="id">Task ID</param>
        ///<returns>The task with the specified ID</returns>
        ///<response code="200">Returns the task with the specified ID</response>
        ///<response code="404">Task not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoTask>> GetTaskById(int id)
        {
            _logger.LogInformation($"Fetching task with ID : {id}");
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                _logger.LogWarning($"Task with ID {id} not found");
                return NotFound(new { message = "Task not Found" });
            }
            return Ok(task);
        }

        //Create a new Task
        ///<param name="request">Task Creation Details</param>
        ///<returns>The newly created task</returns>
        ///<response code="201">Task created successfully</response>
        ///<response code="400">Invalid input</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoTask>> CreateTask([FromBody] TaskCreateRequest request)
        {
            //Validate input
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                _logger.LogWarning("Task creation failed: Title is required");
                return BadRequest(new { message = "Title is required" });
            }

            var task = new TodoTask
            {
                Title = request.Title.Trim(),
                Description = request.Description?.Trim() ?? string.Empty,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Task created with ID : {task.Id}");
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        ///<summary>
        //Update an existing Task(partial update supported)
        ///</summary>
        ///<param name="id">Task ID to update</param>
        ///<param name="request">Fields to update</param>
        ///<returns>The updated task</returns>
        ///<response code="200">Task updated successfully</response>
        ///<response code="404">Task not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoTask>> UpdateTask(int id, [FromBody] TaskUpdateRequest request)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                _logger.LogWarning($"Task with ID {id} not found for update");
                return NotFound(new { message = "Task not Found" });
            }
            //Update fields if provided
            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                task.Title = request.Title.Trim();
            }
            if (request.Description != null)
            {
                task.Description = request.Description.Trim();
            }
            if (request.IsCompleted.HasValue)
            {
                task.IsCompleted = request.IsCompleted.Value;
            }
            task.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Task with ID {id} updated successfully");
            return Ok(new { message = "Task updated successfully", task });
        }

        ///<summary>
        ///Delete Task
        ///</summary>
        ///<param name="id" >Task ID to delete</param>
        ///<returns>Confirmation message</returns>
        ///<response code="200">Task deleted successfully</response>
        ///<response code="404">Task not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                _logger.LogWarning($"Task with ID {id} not found for deletion");
                return NotFound(new { message = "Task not Found" });
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Task with ID {id} deleted successfully");
            return Ok(new { message = "Task deleted successfully" });
        }
    
        ///<summary>
        ///Get tasks filtered by completion status  
        ///</summary>
        ///<param name="isCompleted">Filter by completion status</param>
        ///<returns>Filtered list of tasks </returns>
        ///<response code="200">Returns the list of filtered tasks</response>
        [HttpGet("completed/{isCompleted}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetTasksByStatus(bool isCompleted)
        {
            _logger.LogInformation($"Fetching tasks with completion status : {isCompleted}");
            var tasks = await _context.Tasks
                .Where(t => t.IsCompleted == isCompleted)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return Ok(tasks);
        }
    }
}
