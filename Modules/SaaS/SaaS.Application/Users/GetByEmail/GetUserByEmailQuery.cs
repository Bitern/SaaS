using Application.Abstractions.Messaging;

namespace SaaS.Application.Users.GetByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;
