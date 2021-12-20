using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Horario.Request
{
    public class CalendarioDTORequest
    {
        public int IdServicio { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCliente { get; set; }
        public string ClienteOpcional { get; set; }
        public string ClienteTelefono { get; set; }
        public string ClienteDireccion { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public string FechaText { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public int Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int UsuarioCambio { get; set; }
        public int Recurrente { get; set; }
        public int FlagEmpleado { get; set; }
    }
}
