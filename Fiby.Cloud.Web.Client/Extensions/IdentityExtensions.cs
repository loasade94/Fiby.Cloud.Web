using Fiby.Cloud.Web.DTO.Modules.User.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Extensions
{
    public static class IdentityExtensions
    {
        public static UserDTOResponse ListAplicationAdmin(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst("AplicationAdmin");
            UserDTOResponse asas = new UserDTOResponse();
            if (claim == null)
                return asas;
            string ValueToString = claim.Value;
            var result = JsonConvert.DeserializeObject<UserDTOResponse>(ValueToString);
            return result;
        }
    }
}
