namespace RingoMedia.Tasks.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using RingoMedia.Tasks.Application.Models;
    using RingoMedia.Tasks.Application.Services.DepartmentServices;
    using RingoMedia.Tasks.Domain.DbEntities;

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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentService.GetDepartmentAsync(id);
            if (department is null)
                return NotFound();

            ViewBag.ParentDepartments = department.Parents.Where(d => d.Id != department.Id);

            var dtoModel = new UpdateDepartmentModel
            {
                Id = id,
                LogoUrl = department.LogoUrl,
                Name = department.Name,
                ParentDepartmentId = department.ParentDepartmentId
            };
            return View(dtoModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LogoUrl,ParentDepartmentId")] UpdateDepartmentModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var department = await _departmentService.GetDepartmentAsync(id);
                ViewBag.ParentDepartments = department.Parents;
                return View(model);
            }

            await _departmentService.UpdateDepartmentAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
