using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Pagos.Request
{
    public class PagoEmpleadoDTORequest
    {
        public int IdServicio { get; set; }
        public string NombreDia { get; set; }
        public string NumeroDia { get; set; }
        public string Cliente { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public int Horas { get; set; }
        public decimal Pago { get; set; }
        public int IdSemana { get; set; }
        public int IdEmpleado { get; set; }
        public int IdPagoEmpleado { get; set; }
        public int Estado { get; set; }
        public decimal Pasaje { get; set; }
        public string PasajeText { get; set; }
    }
}
