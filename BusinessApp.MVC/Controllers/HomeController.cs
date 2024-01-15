using BusinessApp.Business.Services.Interfaces;
using BusinessApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BusinessApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService _service;

        public HomeController(IBlogService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> Blogs = await _service.GetAllAsync();

            return View(Blogs);
        }
    }
}
