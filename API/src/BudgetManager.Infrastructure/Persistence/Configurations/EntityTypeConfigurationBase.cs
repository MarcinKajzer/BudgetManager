using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Infrastructure.Persistence.Configurations;

internal abstract class EntityTypeConfigurationBase<T> : IEntityTypeConfiguration<T> where T : class
{
    protected abstract string TableName { get; }
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(TableName);
    }
}
