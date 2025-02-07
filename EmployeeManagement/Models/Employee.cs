namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public  string Name { get; set; }
        public  string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfJoining { get; set; }
    }
}
