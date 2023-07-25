using BudgetManager.Domain.Expenses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Infrastructure.Persistence.Configurations;

public class ExpenseSubcategoryTypeConfiguration : IEntityTypeConfiguration<ExpenseSubcategory>
{
    public void Configure(EntityTypeBuilder<ExpenseSubcategory> builder)
    {
        builder.Property(s => s.Name)
            .HasMaxLength(50);
    }
}
