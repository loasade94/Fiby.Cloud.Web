using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.User.Response
{
    public class SubMenuDTOResponse
    {
        public int IdSubMenu { get; set; }
        public int IdMenu { get; set; }
        public string Name { get; set; }
        public string Controlator { get; set; }
        public string Action { get; set; }
        public int NumberOrder { get; set; }
        public string NameForm { get; set; }
        public bool Active { get; set; }
        public DateTime? DateRegister { get; set; }
        public string ImageIco { get; set; }
        public string Area { get; set; }
    }
}
