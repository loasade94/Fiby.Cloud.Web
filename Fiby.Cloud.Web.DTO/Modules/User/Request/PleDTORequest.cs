using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.User.Request
{
    public class PleDTORequest
    {
        public string Line { get; set; }
        public int SequenceCUO { get; set; }
        public string TipoDocumento { get; set; }
        public string FechaEmision { get; set; }
        public string Serie { get; set; }
        public string NumeroSerie { get; set; }
        public string ValorNeto { get; set; }
        public string ValorIgv { get; set; }
        public string ValorTotal { get; set; }
        public string TipoDocumentoCliente { get; set; }
        public string NumeroDocumentoCliente { get; set; }
        public string NombresDocumentoCliente { get; set; }
    }
}
