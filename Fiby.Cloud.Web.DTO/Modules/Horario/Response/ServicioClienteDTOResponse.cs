using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Horario.Response
{
    public class ServicioClienteDTOResponse
    {
        public int IdServicio { get; set; }
        public DateTime? Fecha{ get; set; }
        public string Horario { get; set; }
        public decimal Pasaje { get; set; }
        public decimal MontoPagoCliente { get; set; }
        public string NombreEmpleado { get; set; }
        public string Direccion { get; set; }
        public string Horas { get; set; }
    }
}
