using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.User.Response
{
    public class StoreDTOResponse
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string RUC { get; set; }
        public string Direction { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public DateTime? DateRegister { get; set; }
    }
}
