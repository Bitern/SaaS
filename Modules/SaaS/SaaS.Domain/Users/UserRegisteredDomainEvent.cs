using SaaS.SharedKernel;

namespace SaaS.Domain.Users;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;
