using HangFire.Services;
using Microsoft.AspNetCore.Mvc;

namespace HangFire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignTask(int employeeId, int managerId, string title, string description, DateTime dueDate)
        {
            try
            {
                await _taskService.AssignTaskAsync(employeeId, managerId, title, description, dueDate);
                return Ok(new { Message = "Task assigned successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to assign task.", Error = ex.Message });
            }
        }

        [HttpPost("complete")]
        public async Task<IActionResult> CompleteTask(int taskId)
        {
            try
            {
                await _taskService.CompleteTaskAsync(taskId);
                return Ok(new { Message = "Task completion process started." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to complete task.", Error = ex.Message });
            }
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetTasksByEmployee(int employeeId)
        {
            try
            {
                var tasks = await _taskService.GetTasksByEmployeeIdAsync(employeeId);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to fetch tasks.", Error = ex.Message });
            }
        }

        /// <summary>
        /// Gets all tasks assigned by a manager.
        /// </summary>
        /// <param name="managerId">The ID of the manager.</param>
        /// <returns>A list of tasks assigned by the manager.</returns>
        [HttpGet("manager/{managerId}")]
        public async Task<IActionResult> GetTasksByManager(int managerId)
        {
            try
            {
                var tasks = await _taskService.GetTasksByManagerIdAsync(managerId);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to fetch tasks.", Error = ex.Message });
            }
        }
    }
}
