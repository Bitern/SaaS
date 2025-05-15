using SaaS.Domain.Emails;
using SaaS.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Email> Emails { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}