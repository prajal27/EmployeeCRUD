namespace EmployeeManagement.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string query, object parameters = null);
        Task<T> GetByIdAsync(string query,object parameters);
        Task<int> ExecuteAsync(string query,object parameters);
        Task<IEnumerable<T>> QueryAsync<T>(string query);
    }
}
