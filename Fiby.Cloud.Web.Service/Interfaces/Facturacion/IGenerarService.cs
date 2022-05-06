using Fiby.Cloud.Web.DTO.Modules.Facturacion.Request;
using Fiby.Cloud.Web.DTO.Modules.Facturacion.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Interfaces.Facturacion
{
    public interface IGenerarService
    {
        Task<List<string>> RegistrarVenta(VentaDTORequest ventaDTORequest);
        Task<List<string>> RegistrarDetalleVenta(DetalleVentaDTORequest detalleVentaDTORequest);
        Task<string> ActualizarEstadoVenta(VentaDTORequest ventaDTORequest);
        Task<string> GenerarComprobante(string idVenta);
        Task<List<string>> RegistrarBaja(VentaDTORequest ventaDTORequest);
        Task<string> GenerarBaja(int idVenta);
        Task<List<VentaDTOResponse>> ListarDocumentosGenerados(VentaDTORequest ventaDTORequest);
        Task<VentaDTOResponse> ListarVentaPorId(VentaDTORequest ventaDTORequest);
        Task<List<DetalleVentaDTOResponse>> ListarDetallePorId(VentaDTORequest ventaDTORequest);
    }
}
