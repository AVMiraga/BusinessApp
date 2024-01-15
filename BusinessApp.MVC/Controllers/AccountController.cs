using AutoMapper;
using BusinessApp.Business.Services.Interfaces;
using BusinessApp.Business.ViewModels.UserVms;
using BusinessApp.Core.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BusinessApp.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountController(
        SignInManager<AppUser> signInManager,
        IAccountService accountService,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper)
        {
            _signInManager = signInManager;
            _accountService = accountService;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm, string? returnUrl)
        {
            RegisterVmValidation validation = new RegisterVmValidation();
            ValidationResult valResult = await validation.ValidateAsync(registerVm);

            if(!valResult.IsValid)
            {
                ModelState.Clear();
                valResult.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
                return View(registerVm);
            }

            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }

            var result = await _accountService.RegisterAsync(registerVm);

            if (result.IdentityResult is null)
            {
                ModelState.AddModelError("", "Something went wrong.");
                return View(registerVm);
            }

            if (!result.IdentityResult.Succeeded)
            {
                foreach (var error in result.IdentityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(registerVm);
            }

            await _signInManager.SignInAsync(result.User, false);

            if (returnUrl != null && returnUrl != "/Account/Login" && returnUrl != "/Account/Register")
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm, string? returnUrl)
        {
            LoginVmValidation validation = new LoginVmValidation();
            ValidationResult valResult = await validation.ValidateAsync(loginVm);

            if (!valResult.IsValid)
            {
                ModelState.Clear();
                valResult.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
                return View(loginVm);
            }

            AppUser user = await _accountService.ValidateUserCredentialsAsync(loginVm);

            if (user is null)
            {
                ModelState.AddModelError("", "Username or password is incorrect.");
                return View(loginVm);
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, true, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Account is locked out.");
                return View(loginVm);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is incorrect.");
                return View(loginVm);
            }

            if (returnUrl != null && returnUrl != "/Account/Login" && returnUrl != "/Account/Register")
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRole()
        {
            await _accountService.CreateRoleAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
