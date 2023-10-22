namespace BloggerSample.Application.Common.Exceptions.Blogs
{
    public sealed class DuplicateTitleException : Exception
    {
        public DuplicateTitleException(string title) 
            : base($"Blog with {title} title already exists")
        {
            
        }
    }
}
