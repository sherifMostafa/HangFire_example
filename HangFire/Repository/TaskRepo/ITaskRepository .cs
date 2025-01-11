namespace HangFire.Repository.TaskRepo
{
    public interface ITaskRepository : IRepository<Data.Domains.Task>
    {
        Task<IEnumerable<Data.Domains.Task>> GetTasksByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<Data.Domains.Task>> GetTasksByManagerIdAsync(int managerId);
    }
}
