using EmployeeManagement.Models;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DepartmentService _depService;
        public DepartmentController(DepartmentService depService)
        {
            _depService = depService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _depService.GetAllDepartmentsAsync();
            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department model)
        {
            if (ModelState.IsValid)
            {
                var result = await _depService.AddDepartmentAsync(model);
                TempData["SuccessMessage"] = result;
                return RedirectToAction("Index");
            }
            return View("Create", model);

        }
    }
}
