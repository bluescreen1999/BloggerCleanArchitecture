using BloggerSample.Application.Blogs.Queries.GetDetails;
using BloggerSample.Application.Blogs.Commands.Add;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using BloggerSample.Domain.Entities;

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
            var blogId = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = blogId }, blogId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBlogDetailsDto>> Get(
            [Required, FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetBlogDetailsQuery() { Id = id };
            return Ok(await _mediator.Send(query, cancellationToken));
        }
    }
}
