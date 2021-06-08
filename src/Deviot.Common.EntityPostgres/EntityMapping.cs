using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Common.EntityPostgres
{
    [ExcludeFromCodeCoverage]
    public abstract class EntityMapping
    {
        protected static void ConfigureBase<TEntity>(EntityTypeBuilder<TEntity> builder, string tableName) where TEntity : Entity
        {
            builder.ToTable(tableName);

            builder.HasKey(t => t.Id);

            builder.HasIndex(t => new { t.Id });
        }
    }
}
