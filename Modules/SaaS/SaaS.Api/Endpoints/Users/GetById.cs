using Application.Abstractions.Messaging;
using SaaS.Application.Users.GetById;
using SaaS.SharedKernel;
using SaaS.Api.Extensions;
using SaaS.Api.Infrastructure;

namespace SaaS.Api.Endpoints.Users;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/{v1}/Users/{userId}", async (
            Guid userId,
            IQueryHandler<GetUserByIdQuery, UserResponse> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetUserByIdQuery(userId);

            Result<UserResponse> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .HasPermission(Permissions.UsersAccess)
        .WithTags(Tags.Users);
    }
}
