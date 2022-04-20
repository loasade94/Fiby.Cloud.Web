using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Parametro.Request
{
    public class DistritoDTORequest
    {
        public string CodigoDepartamento { get; set; }
        public string CodigoProvincia { get; set; }
        public string CodigoDistrito { get; set; }
        public string DistritoDescripcion { get; set; }
        public string Ubigeo { get; set; }
        public string SituacionRegistro { get; set; }
    }
}
