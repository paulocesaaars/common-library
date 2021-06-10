using Deviot.Common.EntitySQLite;
using Deviot.Common.EntitySQLiteTests.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Common.EntitySQLiteTests.Mapping
{
    [ExcludeFromCodeCoverage]
    public class UserMapping : EntityMapping, IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureBase<User>(builder, $"User");

            builder.Property(o => o.Name)
                .HasColumnType("varchar(20)")
                .IsRequired();
        }
    }
}
