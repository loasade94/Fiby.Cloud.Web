using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.User.Request
{
    public class RolDTORequest
    {
        public int RolId { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime? DateRegister { get; set; }
    }
}
