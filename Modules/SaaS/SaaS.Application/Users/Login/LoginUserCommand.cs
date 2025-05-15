using Application.Abstractions.Messaging;

namespace SaaS.Application.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<string>;
