using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Extensions
{
    public class ClaimValue : IClaimValue
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClaimValue(IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }

        public string GetValue(string cookieName, string keyName)
        {
            var principal = _httpContextAccessor.HttpContext.User;

            var cp = principal.Identities.First(i => i.AuthenticationType == cookieName.ToString());
            return cp.FindFirst(keyName).Value;
        }

        public async void SetValue(string cookieName, string keyName, string value)
        {
            var principal = _httpContextAccessor.HttpContext.User;

            var cp = principal.Identities.First(i => i.AuthenticationType == cookieName.ToString());

            if (cp.FindFirst(keyName) != null)
            {
                cp.RemoveClaim(cp.FindFirst(keyName));
                cp.AddClaim(new Claim(keyName, value));
            }

            await _httpContextAccessor.HttpContext.SignOutAsync();

            await _httpContextAccessor.HttpContext.SignInAsync(new ClaimsPrincipal(cp),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true
                });
        }
    }
}
