using Finbuckle.MultiTenant.Abstractions;

namespace Finbuckle.MultiTenant;

public class AppTenantInfo : ITenantInfo
{
    public string? Id { get; set; }
    public string? Identifier { get; set; }
    public string? Name { get; set; }
    public string? ConnectionString { get; set; }

    public string? Logo { get; set; } // New property for logo
}