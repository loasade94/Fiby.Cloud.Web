using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Sale.Response.Serie
{
    public class SerieDTOResponse
    {
        public int SerieId { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime? DateRegister { get; set; }
    }
}
