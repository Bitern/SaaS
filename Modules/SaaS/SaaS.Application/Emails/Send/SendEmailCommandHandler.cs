using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SaaS.Domain.Emails;
using SaaS.Domain.Users;
using SaaS.SharedKernel;

namespace SaaS.Application.Emails.Send;
internal sealed class SendEmailCommandHandler(IApplicationDbContext context)
    : ICommandHandler<SendEmailCommand>
{
    public async Task<Result> Handle(SendEmailCommand command, CancellationToken cancellationToken)
    {
        var userExistsAndVerified = await context.Users
            .AnyAsync(u => u.Email == command.RecipientEmail && u.IsEmailVerified, cancellationToken);

        if (userExistsAndVerified)
        {
            return Result.Failure<Guid>(UserErrors.IsEmailVerified);
        }

        var email = new Email(
            Guid.NewGuid(),
            command.RecipientEmail,
            command.Subject,
            command.Body);

        email.Raise(new EmailSentDomainEvent(email.Id));

        context.Emails.Add(email);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
