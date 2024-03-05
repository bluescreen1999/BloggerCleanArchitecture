namespace BloggerSample.Application.Common.Interfaces;

public interface IDateTimeOffsetProvider
{
    DateTimeOffset UtcNow { get; }
}
