using System;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Username).HasMaxLength(250).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        builder.HasIndex(x => x.Id);
        builder.HasIndex(x => x.Username);
    }
}