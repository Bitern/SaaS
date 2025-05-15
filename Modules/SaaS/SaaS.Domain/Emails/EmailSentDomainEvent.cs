using SaaS.SharedKernel;

namespace SaaS.Domain.Emails;
public sealed record EmailSentDomainEvent(Guid EmailId) : IDomainEvent;