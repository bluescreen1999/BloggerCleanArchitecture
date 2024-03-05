using FluentValidation;

namespace BloggerSample.Application.Blogs.Commands.Edit;

public sealed class EditBlogCommandValidator 
    : AbstractValidator<EditBlogCommand>
{
    public EditBlogCommandValidator()
    {
        RuleFor(_ => _.EditBlogDto.title)
            .NotEmpty().WithMessage("Title is required!")
            .MaximumLength(150).WithMessage("Maximum legnth for title is 150 characters");

        RuleFor(_ => _.EditBlogDto.body)
            .NotEmpty().WithMessage("Content is required!")
            .MaximumLength(1500).WithMessage("Maximum length for content is 1500 characters");
    }
}
