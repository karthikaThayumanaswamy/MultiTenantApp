using System.Collections.Generic;

namespace Finbuckle.MultiTenant.AspNetCore.Options;

public class TenantOptions
{
    public List<AppTenantInfo> Tenants { get; set; } 
}