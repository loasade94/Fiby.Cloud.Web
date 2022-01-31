using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Clinica.Response
{
    public class CitaDTOResponse
    {
        public int IdCita { get; set; }
        public DateTime? FechaCita { get; set; }
        public string Hora { get; set; }
        public string CodigoEspecialidad { get; set; }
        public string DescripcionEspecialidad { get; set; }
        public string CodigoUnicoDoctor { get; set; }
        public string CodigoDoctor { get; set; }
        public string NombreDoctor { get; set; }
        public int CodigoPaciente { get; set; }
        public string NombrePaciente { get; set; }
        public string DocumentoPaciente { get; set; }
        public string SituacionCita { get; set; }
        public string DescripcionSituacionCita { get; set; }

    }
}
