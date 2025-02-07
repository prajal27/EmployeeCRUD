using System.Buffers;
using AutoMapper;
using EmployeeManagement.Dto;
using EmployeeManagement.Models;
using EmployeeManagement.Repository.IRepository;

namespace EmployeeManagement.Service
{
    public class EmployeeService
    {
        private readonly IRepository<Employee> _employeeRepo;
        private readonly IMapper _mapper;
        public EmployeeService(IRepository<Employee> employeeRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeeAsync(string searchValue= "",string department="",int pageNumber=1,int pageSize =10)
        {
            var query = "Select * from dbo.fn_GetEmployees(@SearchValue,@Department,@PageNumber,@PageSize)";

            var parameters = new
            {
                SearchValue = searchValue,
                Department = department,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var employees = await _employeeRepo.GetAllAsync(query, parameters);
        
            return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
        }

        public async Task<EmployeeDTO> GetEmployeeByIdAsync(int id)
        {
            string query = "Select * from dbo.Employees where EmployeeID = @id";
            var parameters = new
            {
                id = id
            };

            var employee= await _employeeRepo.GetByIdAsync(query,parameters);
            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            string query = "SELECT * FROM dbo.Departments";
            return await _employeeRepo.QueryAsync<Department>(query);
        }

        public async Task<string> AddOrUpdateEmployeeAsync(EmployeeDTO employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);

            var query = "EXEC sp_AddUpdateEmployee @EmployeeID, @Name, @DepartmentID, @Salary, @DateOfJoining";
            var parameters = new
            {
                EmployeeID = employee.EmployeeID,
                Name = employee.Name,
                DepartmentID = employee.DepartmentID,
                Salary = employee.Salary,
                DateOfJoining = employee.DateOfJoining
            };

            await _employeeRepo.ExecuteAsync(query, parameters);

            return employee.EmployeeID == 0 ? "Employee added successfully" : "Employee updated successfully";
        }

        public async Task<string> DeleteEmployeeAsync(int employeeId)
        {
            var query = "EXEC Sp_DeleteEmployee @EmployeeID";
            var parameters = new { EmployeeID = employeeId };

            await _employeeRepo.ExecuteAsync(query, parameters);

            return "Employee deleted successfully";
        }
    }
}
