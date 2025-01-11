using Hangfire;
using HangFire.Repository.TaskRepo;

namespace HangFire.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task AssignTaskAsync(int employeeId, int managerId, string title, string description, DateTime dueDate)
        {
            var task = new Data.Domains.Task
            {
                Title = title,
                Description = description,
                DueDate = dueDate,
                EmployeeId = employeeId,
                ManagerId = managerId,
                IsCompleted = false
            };

            await _taskRepository.AddAsync(task);

            BackgroundJob.Enqueue(() => NotifyEmployeeAsync(employeeId, task.Id));

            BackgroundJob.Schedule(() => SendReminderAsync(employeeId, task.Id), dueDate);
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task NotifyEmployeeAsync(int employeeId, int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            Console.WriteLine($"Notifying employee {task.Employee.Name} about task: {task.Title}");
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task SendReminderAsync(int employeeId, int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (!task.IsCompleted)
            {
                Console.WriteLine($"Reminding employee {task.Employee.Name} to complete task: {task.Title}");
            }
        }

        public async Task CompleteTaskAsync(int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            task.IsCompleted = true;
            await _taskRepository.UpdateAsync(task);

            var parentJobId = BackgroundJob.Enqueue(() => MarkTaskAsCompletedAsync(taskId));
            BackgroundJob.ContinueJobWith(parentJobId, () => NotifyManagerAsync(task.ManagerId, taskId));
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task MarkTaskAsCompletedAsync(int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            Console.WriteLine($"Task {task.Title} marked as completed.");
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task NotifyManagerAsync(int managerId, int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            Console.WriteLine($"Notifying manager {task.Manager.Name} that task {task.Title} is completed.");
        }
    }
}