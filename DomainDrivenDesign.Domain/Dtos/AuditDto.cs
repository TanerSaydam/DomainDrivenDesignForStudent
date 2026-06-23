namespace DomainDrivenDesign.Domain.Dtos;

public class AuditDto
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public Guid CreatedUserId { get; set; }
    public string CreatedUserName { get; set; } = default!;
    public DateTimeOffset? UpdatedDate { get; set; }
    public Guid? UpdatedUserId { get; set; }
    public string? UpdatedUserName { get; set; }
}
