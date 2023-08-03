using BudgetManager.Domain.Expenses;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Infrastructure.Persistence.Configurations;

internal class ExpenseCategoryTypeConfiguration : EntityTypeConfigurationBase<ExpenseCategory>
{
    protected override string TableName => "ExpenseCategories";
    public override void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Name)
            .HasMaxLength(50);
    }
}
