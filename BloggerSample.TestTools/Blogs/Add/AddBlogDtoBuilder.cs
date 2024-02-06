using BloggerSample.Application.Blogs.Commands.Add;

namespace BloggerSample.TestTools.Blogs.Add
{
    public sealed class AddBlogDtoBuilder
    {
        private string _title;
        private string _body;

        public AddBlogDtoBuilder()
        {
            _title = "Dummy Blog Title";
            _body = "Dummy Blog Body";
        }

        public AddBlogDtoBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public AddBlogDtoBuilder WithBody(string body)
        {
            _body = body;
            return this;
        }

        public AddBlogDto Build()
        {
            return new AddBlogDto(_title, _body);
        }
    }
}
