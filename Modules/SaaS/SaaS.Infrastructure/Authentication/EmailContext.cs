using Microsoft.AspNetCore.Http;
using SaaS.Application.Abstractions.Authentication;
using System;
using System.Security.Claims;

namespace SaaS.Infrastructure.Authentication;

public class EmailContext : IEmailContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmailContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    private HttpContext? HttpContext => _httpContextAccessor.HttpContext;

    public string? GetUserEmail() =>
        HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

    public Guid? GetUserId()
    {
        var userIdClaim = HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(userIdClaim, out var userId) ? userId : null;
    }
    public Guid? GetEmailId()
    {
        var userIdClaim = HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(userIdClaim, out var userId) ? userId : null;
    }

    public bool IsAuthenticated() =>
        HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}