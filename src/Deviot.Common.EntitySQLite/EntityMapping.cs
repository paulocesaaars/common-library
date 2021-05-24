using Deviot.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deviot.Common.EntitySQLite
{
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
