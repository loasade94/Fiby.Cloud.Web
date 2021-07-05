using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.User.Response
{
    public class CompanyDTOResponse
    {
        public int CompanyId { get; set; }
        public string NameCompany { get; set; }
        public bool Active { get; set; }
        public DateTime? DateRegister { get; set; }
    }
}
