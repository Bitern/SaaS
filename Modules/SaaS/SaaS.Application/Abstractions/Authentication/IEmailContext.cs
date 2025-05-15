namespace SaaS.Application.Abstractions.Authentication;
public interface IEmailContext
{
    string? GetUserEmail();
    Guid? GetUserId();
    Guid? GetEmailId();
    bool IsAuthenticated();
}
