using BudgetManager.Domain.Expenses;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Infrastructure.Persistence.Configurations;

internal class ExpenseSubcategoryTypeConfiguration : EntityTypeConfigurationBase<ExpenseSubcategory>
{
    protected override string TableName => "ExpenseSubcategories";
    public override void Configure(EntityTypeBuilder<ExpenseSubcategory> builder)
    {
        base.Configure(builder);

        builder.Property(s => s.Name)
            .HasMaxLength(50);
    }
}
