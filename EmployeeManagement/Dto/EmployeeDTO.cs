using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Dto
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        public string DepartmentName { get; set; } = "";

        [Range(1, int.MaxValue, ErrorMessage = "Please select a department")]
        public int DepartmentId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public decimal Salary { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date of Joining is required")]
        public DateTime DateOfJoining { get; set; } = DateTime.Now;
    }
}
