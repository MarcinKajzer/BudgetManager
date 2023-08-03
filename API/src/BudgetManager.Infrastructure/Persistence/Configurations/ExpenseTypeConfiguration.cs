using BudgetManager.Domain.Expenses;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Infrastructure.Persistence.Configurations;

internal class ExpenseTypeConfiguration : EntityTypeConfigurationBase<Expense>
{
    protected override string TableName => "Expenses";
    public override void Configure(EntityTypeBuilder<Expense> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Comment)
            .HasMaxLength(100);
    }
}
