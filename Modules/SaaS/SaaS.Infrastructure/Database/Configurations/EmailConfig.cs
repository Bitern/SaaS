using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaaS.Domain.Emails;

namespace SaaS.Infrastructure.Database.Configurations;

internal sealed class EmailConfig : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        builder.HasKey(u => u.Id);
    }
}