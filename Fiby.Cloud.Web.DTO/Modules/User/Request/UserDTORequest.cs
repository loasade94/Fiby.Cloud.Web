using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.User.Request
{
    public class UserDTORequest
    {
        public int UserId { get; set; }
        public string Names { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NameUser { get; set; }
        public string Password { get; set; }
        public int StoreId { get; set; }
        //public Store oTienda { get; set; }
        public int RolId { get; set; }
        //public Rol oRol { get; set; }
        //public List<Menu> oListaMenu { get; set; }
        public bool Active { get; set; }
        public DateTime? DateRegister { get; set; }
        public int Company { get; set; }
    }
}
