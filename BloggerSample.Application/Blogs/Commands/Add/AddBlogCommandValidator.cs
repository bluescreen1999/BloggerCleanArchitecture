using FluentValidation;

namespace BloggerSample.Application.Blogs.Commands.Add;

public sealed class AddBlogCommandValidator : AbstractValidator<AddBlogCommand>
{
    public AddBlogCommandValidator()
    {
        RuleFor(_ => _.AddBlogDto.title)
            .NotEmpty().WithMessage("Title is required!")
            .MaximumLength(150).WithMessage("Maximum legnth for title is 150 characters");

        RuleFor(_ => _.AddBlogDto.body)
            .NotEmpty().WithMessage("Content is required!")
            .MaximumLength(1500).WithMessage("Maximum length for content is 1500 characters");
    }
}
