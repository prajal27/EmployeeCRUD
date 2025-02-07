using EmployeeManagement.Dto;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeService _empService;
        public EmployeesController(EmployeeService empService )
        {
            _empService = empService;
        }
        public async Task<IActionResult> Index( string searchValue="",string department ="",int pageNumber =1, int pageSize=10)
        {
            ViewBag.Departments = (await _empService.GetAllDepartmentsAsync())
                                .Select(d => d.DepartmentName)
                                .ToList();
            var employees = await _empService.GetEmployeeAsync(searchValue,department,pageNumber,pageSize);
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _empService.GetAllDepartmentsAsync();
            return View(new EmployeeDTO());
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(EmployeeDTO employeeDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _empService.AddOrUpdateEmployeeAsync(employeeDto);
                TempData["SuccessMessage"] = result;
                return RedirectToAction("Index");
            }

            ViewBag.Departments = await _empService.GetAllDepartmentsAsync();
            return View("Create", employeeDto);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _empService.GetEmployeeByIdAsync(id);
            ViewBag.Departments = await _empService.GetAllDepartmentsAsync();
            return View("Create", employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _empService.DeleteEmployeeAsync(id);
            return RedirectToAction("Index");
        }

    }
}
