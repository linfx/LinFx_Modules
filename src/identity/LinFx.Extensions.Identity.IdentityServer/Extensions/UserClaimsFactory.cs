using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using IdentityModel;
using LinFx.Extensions.MultiTenancy;

namespace LinFx.Extensions.IdentityServer.AspNetIdentity
{
    internal class UserClaimsFactory<TUser> : IUserClaimsPrincipalFactory<TUser>
        where TUser : class
    {
        private readonly Decorator<IUserClaimsPrincipalFactory<TUser>> _inner;
        private UserManager<TUser> _userManager;

        public UserClaimsFactory(Decorator<IUserClaimsPrincipalFactory<TUser>> inner, UserManager<TUser> userManager)
        {
            _inner = inner;
            _userManager = userManager;
        }

        public async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var principal = await _inner.Instance.CreateAsync(user);
            var identity = principal.Identities.First();

            if (!identity.HasClaim(x => x.Type == JwtClaimTypes.Subject))
            {
                var sub = await _userManager.GetUserIdAsync(user);
                identity.AddClaim(new Claim(JwtClaimTypes.Subject, sub));
            }

            var username = await _userManager.GetUserNameAsync(user);
            var usernameClaim = identity.FindFirst(claim => claim.Type == _userManager.Options.ClaimsIdentity.UserNameClaimType && claim.Value == username);
            if (usernameClaim != null)
            {
                identity.RemoveClaim(usernameClaim);
                identity.AddClaim(new Claim(JwtClaimTypes.PreferredUserName, username));
            }

            if (!identity.HasClaim(x=>x.Type == JwtClaimTypes.Name))
            {
                identity.AddClaim(new Claim(JwtClaimTypes.Name, username));
            }

            if (_userManager.SupportsUserEmail)
            {
                var email = await _userManager.GetEmailAsync(user);
                if (!string.IsNullOrWhiteSpace(email))
                {
                    identity.AddClaims(new[]
                    {
                        new Claim(JwtClaimTypes.Email, email),
                        new Claim(JwtClaimTypes.EmailVerified,
                            await _userManager.IsEmailConfirmedAsync(user) ? "true" : "false", ClaimValueTypes.Boolean)
                    });
                }
            }

            if (_userManager.SupportsUserPhoneNumber)
            {
                var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
                if (!string.IsNullOrWhiteSpace(phoneNumber))
                {
                    identity.AddClaims(new[]
                    {
                        new Claim(JwtClaimTypes.PhoneNumber, phoneNumber),
                        new Claim(JwtClaimTypes.PhoneNumberVerified,
                            await _userManager.IsPhoneNumberConfirmedAsync(user) ? "true" : "false", ClaimValueTypes.Boolean)
                    });
                }
            }

            if (user is IMultiTenant item && !string.IsNullOrEmpty(item?.TenantId))
            {
                identity.AddClaim(new Claim(Security.Claims.ClaimTypes.TenantId, item?.TenantId));
            }

            return principal;
        }
    }
}
