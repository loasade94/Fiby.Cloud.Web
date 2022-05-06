using Fiby.Cloud.Web.DTO.Modules.Facturacion.Request;
using Fiby.Cloud.Web.DTO.Modules.Facturacion.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Interfaces.Facturacion
{
    public interface IGenerarRepository
    {
        Task<List<string>> RegistrarVenta(VentaDTORequest ventaDTORequest);
        Task<List<string>> RegistrarDetalleVenta(DetalleVentaDTORequest detalleVentaDTORequest);
        Task<string> ActualizarEstadoVenta(VentaDTORequest ventaDTORequest);
        Task<List<string>> RegistrarBaja(VentaDTORequest ventaDTORequest);
        Task<List<VentaDTOResponse>> ListarDocumentosGenerados(VentaDTORequest ventaDTORequest);
        Task<VentaDTOResponse> ListarVentaPorId(VentaDTORequest ventaDTORequest);
        Task<List<DetalleVentaDTOResponse>> ListarDetallePorId(VentaDTORequest ventaDTORequest);
    }
}
