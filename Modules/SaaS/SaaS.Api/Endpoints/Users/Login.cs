using Application.Abstractions.Messaging;
using SaaS.Application.Users.Login;
using SaaS.SharedKernel;
using SaaS.Api.Extensions;
using SaaS.Api.Infrastructure;

namespace SaaS.Api.Endpoints.Users;

internal sealed class Login : IEndpoint
{
    public sealed record Request(string Email, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/{v1}/Users/Login", async (
            Request request,
            ICommandHandler<LoginUserCommand, string> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new LoginUserCommand(request.Email, request.Password);

            Result<string> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
