// Copyright Finbuckle LLC, Andrew White, and Contributors.
// Refer to the solution LICENSE file for more information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.AspNetCore.Models;
using Finbuckle.MultiTenant.AspNetCore.Options;
using Finbuckle.MultiTenant.Internal;
using Finbuckle.MultiTenant.Stores;
using Finbuckle.MultiTenant.Stores.ConfigurationStore;
using Finbuckle.MultiTenant.Stores.InMemoryStore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Finbuckle.MultiTenant.AspNetCore.Internal;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal class MultiTenantAuthenticationService<TTenantInfo> : IAuthenticationService
    where TTenantInfo : class, ITenantInfo, new()
{
    private readonly IAuthenticationService _inner;
    private readonly IOptions<TenantOptions> _configuration;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IOptionsMonitor<MultiTenantAuthenticationOptions> _multiTenantAuthenticationOptions;

    public MultiTenantAuthenticationService(IAuthenticationService inner, IOptionsMonitor<MultiTenantAuthenticationOptions> multiTenantAuthenticationOptions, UserManager<ApplicationUser> userManager, IOptions<TenantOptions> _configuration)
    {
            this._inner = inner ?? throw new System.ArgumentNullException(nameof(inner));
            this._multiTenantAuthenticationOptions = multiTenantAuthenticationOptions;
            this.userManager = userManager;
            this._configuration = _configuration;
    }

    private static void AddTenantIdentifierToProperties(HttpContext context, ref AuthenticationProperties? properties)
    {
            // Add tenant identifier to the properties so on the callback we can use it to set the multitenant context.
            var multiTenantContext = context.GetMultiTenantContext<TTenantInfo>();
            if (multiTenantContext?.TenantInfo != null)
            {
                properties ??= new AuthenticationProperties();
                if(!properties.Items.Keys.Contains(Constants.TenantToken))
                    properties.Items.Add(Constants.TenantToken, multiTenantContext.TenantInfo.Identifier);
            }
    }

    public Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string? scheme)
        => _inner.AuthenticateAsync(context, scheme);

    public async Task ChallengeAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
    {
        if (_multiTenantAuthenticationOptions.CurrentValue.SkipChallengeIfTenantNotResolved)
        {
            if (context.GetMultiTenantContext<TTenantInfo>()?.TenantInfo == null)
                return;
        }

        AddTenantIdentifierToProperties(context, ref properties);
        await _inner.ChallengeAsync(context, scheme, properties);
    }

    public async Task ForbidAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
    {
        AddTenantIdentifierToProperties(context, ref properties);
        await _inner.ForbidAsync(context, scheme, properties);
    }

    public async Task SignInAsync(HttpContext context, string? scheme, ClaimsPrincipal principal, AuthenticationProperties? properties)
    {
        var user = await userManager.GetUserAsync(principal);
        SetAuthInfo(ref context, user.TenantId);
        AddTenantIdentifierToProperties(context, ref properties);
        await _inner.SignInAsync(context, scheme, principal, properties);
    }

    public async Task SignOutAsync(HttpContext context, string? scheme, AuthenticationProperties? properties)
    {
       
        await _inner.SignOutAsync(context, scheme, properties);
    }

    private void SetAuthInfo(ref HttpContext context, string tenantId)
    {
        var mtc = context.GetMultiTenantContext<AppTenantInfo>();
        var tenantInfo = _configuration.Value.Tenants.Find(x=>x.Id == tenantId);
        mtc.TenantInfo = tenantInfo;
        context.SetTenantInfo<AppTenantInfo>(tenantInfo, true);
    }
}