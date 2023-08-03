using BudgetManager.Domain.Incomes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Infrastructure.Persistence.Configurations;

internal class IncomeTypeConfiguration : EntityTypeConfigurationBase<Income>
{
    protected override string TableName => "Incomes";
    public override void Configure(EntityTypeBuilder<Income> builder)
    {
        base.Configure(builder);

        builder.Property(i => i.Comment)
            .HasMaxLength(100);
    }
}
