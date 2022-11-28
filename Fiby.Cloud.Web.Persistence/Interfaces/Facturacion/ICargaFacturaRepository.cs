using Fiby.Cloud.Web.DTO.Modules.Facturacion.Request;
using Fiby.Cloud.Web.DTO.Modules.Facturacion.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Interfaces.Facturacion
{
    public interface ICargaFacturaRepository
    {
        Task<List<string>> RegistrarFactura(CargaFacturaDTORequest cargaFacturaDTORequest);
        Task<List<CargaFacturaDTOResponse>> ConsultaFacturas(CargaFacturaDTORequest cargaFacturaDTORequest);
        Task<List<string>> GetPle0801(CargaFacturaDTORequest request);
        Task<string> EliminarFactura(CargaFacturaDTORequest request);
    }
}
