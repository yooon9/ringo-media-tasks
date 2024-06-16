namespace RingoMedia.Tasks.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RingoMedia.Tasks.Application.Models;
    using RingoMedia.Tasks.Application.Services.ReminderServices;

    public class RemindersController : Controller
    {
        private readonly IReminderService _reminderService;

        public RemindersController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _reminderService.GetRemindersAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReminderModel reminderModel)
        {
            if (!ModelState.IsValid)
                return View(reminderModel);

            await _reminderService.CreateReminderAsync(reminderModel);
            return RedirectToAction(nameof(Index));
        }

    }
}
