using BudgetManager.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManager.Infrastructure.Persistence.Configurations;

internal class UserTypeConfiguration : EntityTypeConfigurationBase<User>
{
    protected override string TableName => "Users";

    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(u => u.Email)
            .HasMaxLength(100);
    }
}
