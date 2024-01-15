using BusinessApp.Business.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BusinessApp.Business.ViewModels
{
    public class CreateBlogVm
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
    }

    public class CreateBlogVmValidation : AbstractValidator<CreateBlogVm>
    {
        public CreateBlogVmValidation() 
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Author)
                .NotEmpty();

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.ImageFile)
                .NotEmpty()
                .Must(x => x is not null && x.CheckType())
                .WithMessage("Only images files are accepted!")
                .Must(x => x is not null && x.CheckLength(3))
                .WithMessage("Image Size Must be lower than 3MBs!");
        }
    }
}
