using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Pagos.Response
{
    public class PagoClienteDTOResponse
    {
        public int IdServicio { get; set; }
        public string NombreDia { get; set; }
        public string NumeroDia { get; set; }
        public string Cliente { get; set; }
        public decimal MontoPagoCliente { get; set; }
    }
}
