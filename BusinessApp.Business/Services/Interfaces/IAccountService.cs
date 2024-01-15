using BusinessApp.Business.ViewModels.UserVms;
using BusinessApp.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace BusinessApp.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResult> RegisterAsync(RegisterVm registerVm);
        Task<AppUser> ValidateUserCredentialsAsync(LoginVm loginVm);
        Task CreateRoleAsync();
    }
    public class RegisterResult
    {
        public IdentityResult IdentityResult { get; set; }
        public AppUser User { get; set; }
    }
}
