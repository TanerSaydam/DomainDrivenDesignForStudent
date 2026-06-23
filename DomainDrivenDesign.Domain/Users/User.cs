using Microsoft.AspNetCore.Identity;

namespace DomainDrivenDesign.Domain.Users;

public sealed class User : IdentityUser<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => $"{FirstName} {LastName}";
}