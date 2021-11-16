using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Pagos.Request
{
    public class PagoClienteDTORequest
    {
        public int IdServicio { get; set; }
        public string NombreDia { get; set; }
        public string NumeroDia { get; set; }
        public string Cliente { get; set; }
        public int IdSemana { get; set; }
        public int IdEmpleado { get; set; }
        public int IdPagoEmpleado { get; set; }
        public decimal MontoPagoCliente { get; set; }
        public string MontoPagoClienteText { get; set; }
    }
}
