using AutoMapper;
using BusinessApp.Business.Services.Interfaces;
using BusinessApp.Business.ViewModels;
using BusinessApp.Core.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessApp.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public BlogController(IBlogService blogService, IMapper mapper)
        {
            _blogService = blogService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await _blogService.GetAllAsync();

            return View(blogs);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogVm vm)
        {
            CreateBlogVmValidation validation = new CreateBlogVmValidation();
            ValidationResult result = await validation.ValidateAsync(vm);

            if(!result.IsValid)
            {
                ModelState.Clear();
                result.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
                return View(vm);
            }

            await _blogService.CreateAsync(vm);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            Blog blog = await _blogService.GetByIdAsync(id);

            UpdateBlogVm updateBlogVm = _mapper.Map<UpdateBlogVm>(blog);

            return View(updateBlogVm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBlogVm vm)
        {
            UpdateBlogVmValidation validation = new UpdateBlogVmValidation();
            ValidationResult result = await validation.ValidateAsync(vm);

            if (!result.IsValid)
            {
                ModelState.Clear();
                result.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
                return View(vm);
            }

            await _blogService.UpdateAsync(vm);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
