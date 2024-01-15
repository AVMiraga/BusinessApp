using BusinessApp.Business.Enums;
using BusinessApp.Business.Services.Interfaces;
using BusinessApp.Business.ViewModels.UserVms;
using BusinessApp.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace BusinessApp.Business.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AppUser> ValidateUserCredentialsAsync(LoginVm loginVm)
        {
            var user = (await _userManager.FindByNameAsync(loginVm.LoginId)
            ?? await _userManager.FindByEmailAsync(loginVm.LoginId))
            ?? null;

            if (user is null)
            {
                return null;
            }

            var result = await _userManager.CheckPasswordAsync(user, loginVm.Password);

            if (!result)
            {
                return null;
            }

            return user;
        }

        public async Task<RegisterResult> RegisterAsync(RegisterVm registerVm)
        {
            AppUser user = new()
            {
                UserName = registerVm.Username,
                Email = registerVm.Email,
                FirstName = registerVm.FirstName,
                LastName = registerVm.LastName
            };

            var result = await _userManager.CreateAsync(user, registerVm.Password);

            if (!result.Succeeded)
            {
                return new RegisterResult
                {
                    IdentityResult = result,
                    User = null
                };
            }

            await _userManager.AddToRoleAsync(user, MyRoles.Admin.ToString());

            return new RegisterResult
            {
                IdentityResult = result,
                User = user
            };
        }

        public async Task CreateRoleAsync()
        {
            foreach (var role in Enum.GetNames(typeof(MyRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
