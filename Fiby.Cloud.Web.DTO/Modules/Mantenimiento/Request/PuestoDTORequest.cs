using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Mantenimiento.Request
{
    public class PuestoDTORequest
    {
        public int IdPuesto { get; set; }
        public string DescripcionPuesto { get; set; }
        public string SituacionRegistro { get; set; }
    }
}
