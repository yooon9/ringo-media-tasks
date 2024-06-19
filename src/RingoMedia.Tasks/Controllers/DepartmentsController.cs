namespace RingoMedia.Tasks.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RingoMedia.Tasks.Application.Models;
    using RingoMedia.Tasks.Application.Services.DepartmentServices;

    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()    
        {
            return View(await _departmentService.GetDepartmentsAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var department = await _departmentService.GetDepartmentAsync(id);

            if (department is null)
                return NotFound();

            return View(department);
        }
        
        public async Task<IActionResult> DisplayDepartments()
        {
            var departments = await _departmentService.GetRecursiveDepartmentHierarchy();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentModel departmentModel)
        {
            if (!ModelState.IsValid)
                return View(departmentModel);

            await _departmentService.CreateDepartmentAsync(departmentModel);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetSubDepartments(int? id)
        {
            return Json(await _departmentService.GetSubDepartmentsAsync(id));
        }
    }
}
