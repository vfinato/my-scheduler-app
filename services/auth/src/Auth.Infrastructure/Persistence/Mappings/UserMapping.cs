using Auth.Domain.Entities;
using Auth.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Persistence.Mappings
{
    public sealed class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Id).ValueGeneratedNever();
            builder.Property(u => u.CreatedAt).IsRequired();
            builder.Property(u => u.IsActive).IsRequired().HasDefaultValue(1);

            builder.OwnsOne(u => u.Email, email =>
            {
               email.Property(e=>e.Value)
                .HasColumnName("email")
                .IsRequired(); 
            });

            builder.Property(u => u.Password)
                .HasConversion(
                    password => password.Hash,
                    hash => Password.FromHash(hash)
                )
                .HasColumnName("password_hash")
                .IsRequired();

        }
    }
}