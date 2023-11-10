using BloggerSample.Application.Blogs.Queries.GetDetails;
using BloggerSample.Application.Blogs.Commands.Delete;
using BloggerSample.Application.Blogs.Queries.GetAll;
using BloggerSample.Application.Blogs.Commands.Edit;
using BloggerSample.Application.Blogs.Commands.Add;
using BloggerSample.Application.Common.Models;
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

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(
            [Required, FromRoute] Guid id,
            [Required, FromBody] EditBlogDto dto,
            CancellationToken cancellationToken)
        {
            var command = new EditBlogCommand() { EditBlogDto = dto, Id = id };
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(
            [Required, FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteBlogCommand() { Id = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<GetAllBlogsDto>>> Get(
            [Required, FromQuery] PagingParams pagingParams,
            [FromQuery] GetAllBlogsFilterDto filterDto,
            CancellationToken cancellationToken)
        {
            var query = new GetAllBlogsQuery() { PagingParams = pagingParams, FilterDto = filterDto };
            var pagedBlogs = await _mediator.Send(query, cancellationToken);

            AddLinksToPagedBlogs(pagedBlogs);

            return Ok(pagedBlogs);
        }

        private void AddLinksToPagedBlogs(PagedList<GetAllBlogsDto> pagedBlogs)
        {
            pagedBlogs.Items.ToList().ForEach(_ => _.OperationLinks.AddRange(
                new List<OperationLink>
                {
                    new OperationLink
                    {
                        Href = Url.Action(nameof(Get), ControllerContext.ActionDescriptor.ControllerName,
                        new { _.Id }, Request.Scheme),
                        Method = "Get",
                        Rel = "Self"
                    },
                    new OperationLink
                    {
                        Href = Url.Action(nameof(Put), ControllerContext.ActionDescriptor.ControllerName,
                        new { id = _.Id }, Request.Scheme),
                        Method = "Put",
                        Rel = "Update"
                    },
                    new OperationLink
                    {
                        Href = Url.Action(nameof(Delete), ControllerContext.ActionDescriptor.ControllerName,
                        new { id = _.Id }, Request.Scheme),
                        Method = "Delete",
                        Rel = "Delete"
                    }
                }));
        }
    }
}
