using BloggerSample.Application.Blogs.Commands.Edit;

namespace BloggerSample.TestTools.Blogs.Edit
{
    public sealed class EditBlogDtoBuilder
    {
        private string _title;
        private string _body;

        public EditBlogDtoBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public EditBlogDtoBuilder WithBody(string body)
        {
            _body = body;
            return this;
        }

        public EditBlogDto Build()
        {
            return new EditBlogDto(_title, _body);
        }
    }
}
