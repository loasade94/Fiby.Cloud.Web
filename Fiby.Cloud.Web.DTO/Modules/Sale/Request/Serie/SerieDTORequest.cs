using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Sale.Request.Serie
{
    public class SerieDTORequest
    {
        public int SerieId { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime? DateRegister { get; set; }
    }
}
