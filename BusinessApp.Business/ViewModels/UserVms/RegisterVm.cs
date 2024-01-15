using FluentValidation;

namespace BusinessApp.Business.ViewModels.UserVms
{
    public class RegisterVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterVmValidation : AbstractValidator<RegisterVm>
    {
        public RegisterVmValidation()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Username)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(p => p.Password);
        }
    }
}
