using Fiby.Cloud.Web.DTO.Modules.Facturacion.Request;
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
    }
}
