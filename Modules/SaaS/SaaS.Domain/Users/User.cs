using SaaS.SharedKernel;

namespace SaaS.Domain.Users;

public sealed class User : Entity
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsEmailVerified { get; set; } = false;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
