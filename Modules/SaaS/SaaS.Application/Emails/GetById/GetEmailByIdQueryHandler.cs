using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SaaS.Application.Abstractions.Authentication;
using SaaS.Domain.Emails;
using SaaS.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace SaaS.Application.Emails.GetById;
internal sealed class GetEmailByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetEmailByIdQuery, EmailResponse>
{
    public async Task<Result<EmailResponse>> Handle(GetEmailByIdQuery query, CancellationToken cancellationToken)
    {
        EmailResponse? email = await context.Emails
            .Where(u => u.Id == query.Id)
            .Select(u => new EmailResponse
            {
                Id = u.Id,
                Subject = u.Subject,
                Body = u.Body,
                RecipientEmail = u.RecipientEmail,
                CreatedAt = u.CreatedAt,
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (email is null)
        {
            return Result.Failure<EmailResponse>(EmailErrors.NotFound(query.Id));
        }

        return email;
    }
}
