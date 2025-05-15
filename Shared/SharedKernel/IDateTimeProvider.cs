namespace SaaS.SharedKernel;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
