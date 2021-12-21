using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Reportes.Request
{
    public class AnuncioDTORequest
    {
        public int IdAnuncio { get; set; }
        public string Titulo { get; set; }
        public string Detalle { get; set; }
        public int Orden { get; set; }
        public DateTime? FechaInicio { get; set; }
        public string FechaInicioText { get; set; }
        public DateTime? FechaFin { get; set; }
        public string FechaFinText { get; set; }
        public string Mes { get; set; }
        public string Dia { get; set; }
    }
}
