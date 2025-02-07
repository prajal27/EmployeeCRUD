using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Department
    {
        public int DepartmentID { get;  set; }

        [Required(ErrorMessage = "Department Name is required")]
        [StringLength(100, ErrorMessage = "Department Name cannot be longer than 100 characters")]
        public string DepartmentName { get; set; } = "";
        /*public virtual ICollection<Employee> Employees { get; set; } */
    }
}
