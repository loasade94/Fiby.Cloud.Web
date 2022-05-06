using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Maintenance.Response
{
    public class ClienteDTOResponse
    {
        public int IdCliente { get; set; }
        public string Nombres { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string NombreCompleto { get; set; }
        public string RazonSocial { get; set; }
        public string DepartamentoDireccion { get; set; }
        public string ProvinciaDireccion { get; set; }
        public string DistritoDireccion { get; set; }
        public string UbigeoDireccion { get; set; }
        public string FacturacionDireccion { get; set; }
        public string DepartamentoDireccionDescripcion { get; set; }
        public string ProvinciaDireccionDescripcion { get; set; }
        public string DistritoDireccionDescripcion { get; set; }
        public string TipoDocumentoDescripcion { get; set; }
        public string NumeroComprobante { get; set; }
    }
}
