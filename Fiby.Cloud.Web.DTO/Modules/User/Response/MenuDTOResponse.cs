using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.User.Response
{
    public class MenuDTOResponse
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool Active { get; set; }
        public DateTime? DateRegister { get; set; }
    }
}
