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

        public static string CompanyId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst("CompanyId");
            string asas = string.Empty;
            if (claim == null)
                return asas;
            string ValueToString = claim.Value;
            var result = JsonConvert.DeserializeObject<string>(ValueToString);
            return result;
        }
        public static string GetProfileId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst("ProfileId");

            if (claim == null)
                return "";
            return claim.Value;
        }
        public static string GetProfile(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst("Profile");

            if (claim == null)
                return "";

            return claim.Value;
        }
        public static string GetNombre(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst("Nombre");

            if (claim == null)
                return "";

            return claim.Value;
        }

        public static string GetCodigoUnico(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst("CodigoUnico");

            if (claim == null)
                return "";

            return claim.Value;
        }

        public static string GetIdEmpleado(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst("IdEmpleado");

            if (claim == null)
                return "";
            return claim.Value;
        }
    }
}
