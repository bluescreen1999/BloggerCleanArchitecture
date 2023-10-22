using FluentValidation;

namespace BloggerSample.Application.Blogs.Queries.GetAll
{
    public sealed class GetAllBlogsQueryValidator 
        : AbstractValidator<GetAllBlogsQuery>
    {
        public GetAllBlogsQueryValidator()
        {
            RuleFor(_ => _.PagingParams.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber should be greater than or equal to 1.");

            RuleFor(_ => _.PagingParams.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize should be greater than or equal to 1.");
        }
    }
}
