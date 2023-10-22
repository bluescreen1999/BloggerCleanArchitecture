using BloggerSample.Application.Blogs.Commands.Add;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace BloggerSample.WebApi.Controllers
{
    [ApiController, Route("v1/api/[controller]")]
    public sealed class BlogsController: ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(
            [Required, FromBody] AddBlogDto dto,
            CancellationToken cancellationToken)
        {
            var command = new AddBlogCommand() { AddBlogDto = dto };
            return await _mediator.Send(command, cancellationToken);
        }
    }
}
