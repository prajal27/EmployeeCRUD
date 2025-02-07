using System.Data;
using Dapper;
using EmployeeManagement.Repository.IRepository;

namespace EmployeeManagement.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbConnection _connection;
        public Repository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<T>> GetAllAsync(string query, object parameters = null)
        {
            return await _connection.QueryAsync<T>(query, parameters);
        }

        public async Task<T> GetByIdAsync(string query, object parameters)
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(query, parameters);
        }
        
        public async Task<int> ExecuteAsync(string query, object parameters)
        {
            return await _connection.ExecuteAsync(query, parameters);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query)
        {
            return await _connection.QueryAsync<T>(query);
        }
    }
}
