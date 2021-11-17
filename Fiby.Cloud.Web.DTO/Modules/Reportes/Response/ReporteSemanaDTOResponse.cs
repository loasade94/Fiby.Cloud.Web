using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Reportes.Response
{
    public class ReporteSemanaDTOResponse
    {
        public int IdServicio { get; set; }
        public string NombreDia { get; set; }
        public string NumeroDia { get; set; }
        public string Cliente { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public int Horas { get; set; }
        public decimal Pago { get; set; }
        public string DescripcionEstado { get; set; }
        public decimal Pasaje { get; set; }
        public decimal MontoPagoCliente { get; set; }
        public decimal SubTotal { get; set; }
        public DateTime? Fecha { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public decimal Rentabilidad { get; set; }
    }
}
