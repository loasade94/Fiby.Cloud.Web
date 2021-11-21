using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Extensions
{
    public class CustomClaimTypes : IdentityUser
    {
        public const string AplicationAdmin = "AplicationAdmin";
        public const string CompanyId = "CompanyId";
        public const string ProfileId = "ProfileId";
        public const string Profile = "Profile";
        public const string Nombre = "Nombre";
    }
}
