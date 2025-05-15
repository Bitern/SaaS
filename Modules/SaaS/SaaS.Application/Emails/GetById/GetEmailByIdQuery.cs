using Application.Abstractions.Messaging;

namespace SaaS.Application.Emails.GetById;
public sealed record GetEmailByIdQuery(Guid Id) : IQuery<EmailResponse>;
