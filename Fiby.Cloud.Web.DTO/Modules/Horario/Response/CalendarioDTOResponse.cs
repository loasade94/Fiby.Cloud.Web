using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Horario.Response
{
    public class CalendarioDTOResponse
    {
        public int IdServicio { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCliente { get; set; }
        public string ClienteOpcional { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public int Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public DateTime? FechaCambio { get; set; }
        public int UsuarioCambio { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }

        public int TotalHorasDia { get; set; }
        public int TotalHoraSemana { get; set; }
        public int TotalHoraMes { get; set; }
        public string NombreDia { get; set; }
        public string NumeroDia { get; set; }

    }
}
