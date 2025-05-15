using Application.Abstractions.Messaging;

namespace SaaS.Application.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
