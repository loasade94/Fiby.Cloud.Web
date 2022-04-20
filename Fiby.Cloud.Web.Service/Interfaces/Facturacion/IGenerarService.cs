using Fiby.Cloud.Web.DTO.Modules.Facturacion.Request;
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
    }
}
