using BusinessApp.Business.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BusinessApp.Business.ViewModels
{
    public class UpdateBlogVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public IFormFile? ImageFile { get; set; }
    }

    public class UpdateBlogVmValidation : AbstractValidator<UpdateBlogVm>
    {
        public UpdateBlogVmValidation() 
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Author)
                .NotEmpty();

            RuleFor(x => x.Description)
                .NotEmpty();

            When(x => x.ImageFile is not null, () =>
                RuleFor(x => x.ImageFile)
                    .Must(x => x.CheckType())
                    .WithMessage("Only images files are accepted!")
                    .Must(x => x.CheckLength(3))
                    .WithMessage("Image Size Must be lower than 3MBs!")
            );
        }
    }
}
