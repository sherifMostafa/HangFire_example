using HangFire.Data;

namespace HangFire.Services
{
    public class ReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public void GenerateDailyReport()
        {
            var incompleteTasks = _context.Tasks.Where(t => !t.IsCompleted).ToList();
            Console.WriteLine("Daily Report - Incomplete Tasks:");
            foreach (var task in incompleteTasks)
            {
                Console.WriteLine($"- {task.Title} (Assigned to {task.Employee.Name})");
            }
        }
    }
}
