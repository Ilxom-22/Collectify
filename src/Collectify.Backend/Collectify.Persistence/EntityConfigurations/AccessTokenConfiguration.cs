using Collectify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Collectify.Persistence.EntityConfigurations;

public class AccessTokenConfiguration : IEntityTypeConfiguration<AccessToken>
{
    public void Configure(EntityTypeBuilder<AccessToken> builder)
    {
        builder.HasKey(token => token.UserId);
        builder.HasIndex(token => token.Token).IsUnique();
        builder.Property(token => token.ExpiryTime).IsRequired();
    }
}