using BudgetManager.Domain.Incomes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Infrastructure.Persistence.Configurations;

internal class IncomeCategoryTypeConfiguration : EntityTypeConfigurationBase<IncomeCategory>
{
    protected override string TableName => "IncomeCategories";
    public override void Configure(EntityTypeBuilder<IncomeCategory> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Name)
            .HasMaxLength(50);
    }
}
