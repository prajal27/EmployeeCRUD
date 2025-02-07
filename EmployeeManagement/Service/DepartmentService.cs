using EmployeeManagement.Models;
using EmployeeManagement.Repository.IRepository;

namespace EmployeeManagement.Service
{
    public class DepartmentService
    {
        private readonly IRepository<Department> _departRepo;
        public DepartmentService(IRepository<Department> departRepo)
        {
            _departRepo = departRepo;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            string query = "SELECT * FROM dbo.Departments";
            return await _departRepo.QueryAsync<Department>(query);
        }
        public async Task<string> AddDepartmentAsync(Department department)
        {
            var query = "EXEC Sp_CreateDepartment @DepartmentName, @DepartmentId";
            var parameters = new
            {
                DepartmentName = department.DepartmentName,
                DepartmentId = department.DepartmentID
            };
            await _departRepo.ExecuteAsync(query, parameters);
            return query;
        }
    }
}
