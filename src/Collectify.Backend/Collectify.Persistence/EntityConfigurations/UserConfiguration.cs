using Collectify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collectify.Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        builder.HasIndex(user => user.EmailAddress).IsUnique();
        builder.HasIndex(user => user.UserName).IsUnique();
        builder.Property(user => user.PasswordHash).IsRequired();
    }
}