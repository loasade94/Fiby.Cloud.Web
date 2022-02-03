using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Clinica.Request
{
    public class PacienteDTORequest
    {
        public string DocumentoPaciente { get; set; }
        public int CodigoPaciente { get; set; }
        public string DniPaciente { get; set; }
        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public string Seguro { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public string SituacionRegistro { get; set; }
    }
}
