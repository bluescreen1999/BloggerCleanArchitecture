namespace BloggerSample.Application.Common.Exceptions.Abstractions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string entityName, Guid id)
            : base($"{entityName.ToUpper()} With id: {id} Not Found")
        {

        }
    }
}
