using Microsoft.AspNetCore.Identity;

namespace Finbuckle.MultiTenant.AspNetCore.Models;

public class ApplicationUser : IdentityUser
{
    public string TenantId { get; set; } = "D2FA78CE-3185-458E-964F-8FD0052B4330";
}