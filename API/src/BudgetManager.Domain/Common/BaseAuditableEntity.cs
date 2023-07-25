namespace BudgetManager.Domain.Common;

public class BaseAuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public Guid? LastModifiedBy { get; set; }
}
