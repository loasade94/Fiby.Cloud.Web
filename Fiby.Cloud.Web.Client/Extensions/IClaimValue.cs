using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client.Extensions
{
    public interface IClaimValue
    {
        string GetValue(string cookieName, string keyName);
        void SetValue(string cookieName, string keyName, string value);
    }
}
