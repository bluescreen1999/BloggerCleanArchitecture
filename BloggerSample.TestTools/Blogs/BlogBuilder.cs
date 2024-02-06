using BloggerSample.Domain.Entities;

namespace BloggerSample.TestTools.Blogs
{
    public sealed class BlogBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _title = "Dummy Blog Title";
        private string _body = "Dummy Blog Body";
        private bool _isDeleted = false;
        private DateTimeOffset _creationDateTimeOffset = DateTimeOffset.UtcNow;

        public BlogBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public BlogBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public BlogBuilder WithBody(string body)
        {
            _body = body;
            return this;
        }

        public BlogBuilder WithIsDeleted(bool isDeleted)
        {
            _isDeleted = isDeleted;
            return this;
        }

        public BlogBuilder WithCreationDateTimeOffset(DateTimeOffset creationDateTimeOffset)
        {
            _creationDateTimeOffset = creationDateTimeOffset;
            return this;
        }

        public Blog Build()
        {
            return new Blog
            {
                Id = _id,
                Title = _title,
                Body = _body,
                IsDeleted = _isDeleted,
                CreationDateTime = _creationDateTimeOffset
            };
        }
    }
}
