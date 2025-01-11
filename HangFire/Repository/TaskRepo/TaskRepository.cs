using HangFire.Data;
using Microsoft.EntityFrameworkCore;

namespace HangFire.Repository.TaskRepo
{
    public class TaskRepository : Repository<Data.Domains.Task>, ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Data.Domains.Task>> GetTasksByEmployeeIdAsync(int employeeId)
        {
            return await _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Manager)
                .Where(t => t.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Data.Domains.Task>> GetTasksByManagerIdAsync(int managerId)
        {
            return await _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Manager)
                .Where(t => t.ManagerId == managerId)
                .ToListAsync();
        }
    }
}
