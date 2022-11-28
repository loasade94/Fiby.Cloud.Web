using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Facturacion.Response
{
    public class CargaFacturaDTOResponse
    {
        public int IdFacturaEmpresa { get; set; }
        public int IdEmpresa { get; set; }
        public string Mes { get; set; }
        public string Ano { get; set; }
        public string TipoDocumentoVenta { get; set; }
        public string FechaEmision { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public string ValorNeto { get; set; }
        public string Igv { get; set; }
        public string OtrosCargos { get; set; }
        public string ValorTotal { get; set; }
        public string TipoDocumentoCliente { get; set; }
        public string NumeroDocumentoCliente { get; set; }
        public string RazonSocialCliente { get; set; }
        public string SituacionRegistro { get; set; }
        public string UsuarioRegistro { get; set; }
        public DateTime? FechaHoraRegistro { get; set; }
        public string UsuarioModifica { get; set; }
        public DateTime? FechaHoraModifica { get; set; }

    }
}
