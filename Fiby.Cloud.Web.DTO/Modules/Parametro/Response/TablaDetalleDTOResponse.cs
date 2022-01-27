using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Parametro.Response
{
    public class TablaDetalleDTOResponse
    {
        public string CodigoRegistro { get; set; }

        public string CodigoTabla { get; set; }

        public string DescripcionRegistro { get; set; }
        public string DescripcionRegistroCodigo { get; set; }
        public string SituacionRegistro { get; set; }
        public string Valor { get; set; }
        public string Nota { get; set; }
    }
}
