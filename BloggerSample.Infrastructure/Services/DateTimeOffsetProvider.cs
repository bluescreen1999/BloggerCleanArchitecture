using BloggerSample.Application.Common.Interfaces;

namespace BloggerSample.Infrastructure.Services
{
    public sealed class DateTimeOffsetProvider : IDateTimeOffsetProvider
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}
