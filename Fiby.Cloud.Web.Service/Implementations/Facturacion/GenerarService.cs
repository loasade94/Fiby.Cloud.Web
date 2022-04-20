using Fiby.Cloud.Web.DTO.Modules.Facturacion.Request;
using Fiby.Cloud.Web.Persistence.Interfaces.Facturacion;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Interfaces.Facturacion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations.Facturacion
{
    public class GenerarService : IGenerarService
    {
        private readonly IGenerarRepository _generarRepository;
        private readonly IHttpClient _httpClient;

        public GenerarService(IHttpClient httpClient, IGenerarRepository generarRepository)
        {
            _generarRepository = generarRepository;
            _httpClient = httpClient;
        }

        public async Task<List<string>> RegistrarVenta(VentaDTORequest ventaDTORequest)
        {
            var response = await _generarRepository.RegistrarVenta(ventaDTORequest);
            return response;
        }

        public async Task<List<string>> RegistrarDetalleVenta(DetalleVentaDTORequest detalleVentaDTORequest)
        {
            var response = await _generarRepository.RegistrarDetalleVenta(detalleVentaDTORequest);
            return response;
        }

        public async Task<string> ActualizarEstadoVenta(VentaDTORequest ventaDTORequest)
        {
            var response = await _generarRepository.ActualizarEstadoVenta(ventaDTORequest);
            return response;
        }

        public async Task<string> GenerarComprobante(string idVenta)
        {
            string uri = "http://localhost:58683/api/Venta/GenerarBoletaFactura/" + idVenta;
            var result = await _httpClient.GetStringAsync(uri);
            return result;
        }
    }
}
