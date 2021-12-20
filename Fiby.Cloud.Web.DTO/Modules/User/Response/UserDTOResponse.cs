using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.User.Response
{
    public class UserDTOResponse
    {
        public int UserId { get; set; }
        public string Names { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NameUser { get; set; }
        public string Password { get; set; }
        public int StoreId { get; set; }
        public StoreDTOResponse oStore { get; set; }
        public int RolId { get; set; }
        public RolDTOResponse oRol { get; set; }
        public List<MenuDTOResponse> oListMenu { get; set; }
        public List<SubMenuDTOResponse> oListSubMenu { get; set; }
        public bool Active { get; set; }
        public DateTime? DateRegister { get; set; }
        public int CompanyId { get; set; }
        public UserDTOResponse oUser { get; set; }
        public CompanyDTOResponse oCompany { get; set; }
        public int IdEmpleado { get; set; }
    }
}
