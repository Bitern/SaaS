using SaaS.SharedKernel;

namespace SaaS.Domain.Emails;
public static class EmailErrors
{
    public static Error NotFound(Guid emailId) => Error.NotFound(
        "Emails.NotFound",
        $"The email with the Id = '{emailId}' was not found");

    public static Error Unauthorized() => Error.Failure(
        "Emails.Unauthorized",
        "You are not authorized to perform this action.");

    public static readonly Error NotFoundByEmail = Error.NotFound(
        "Emails.NotFoundByEmail",
        "The email with the specified email was not found");

    public static readonly Error EmailNotUnique = Error.Conflict(
        "Emails.EmailNotUnique",
        "The provided email is not unique");
}
