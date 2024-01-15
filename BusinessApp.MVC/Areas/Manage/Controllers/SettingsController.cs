using BusinessApp.Business.Helpers;
using BusinessApp.Business.Services.SettingService;
using BusinessApp.Core.Entities;
using BusinessApp.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessApp.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            Dictionary<string, string> settings = await _context.Settings.ToDictionaryAsync(s => s.Key, s => s.Value);

            settings.TryAdd("Logo", "");
            settings.TryAdd("Description", "");

            return View(settings);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Dictionary<string, string> settings, IFormFile file)
        {
            foreach (var item in settings)
            {
                if (item.Key == "__RequestVerificationToken")
                    continue;

                Setting setting = await _context.Settings.FirstOrDefaultAsync(x => x.Key == item.Key);

                setting.Value = item.Value;

                _context.Update(setting);
            }

            if (file != null)
            {
                string fileName = await file.UploadFile(_env.WebRootPath, "Upload");

                Setting setting = await _context.Settings.FirstOrDefaultAsync(x => x.Key == "Logo");

                setting.Value = fileName;

                _context.Update(setting);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
