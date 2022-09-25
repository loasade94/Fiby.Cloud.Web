using Fiby.Cloud.Web.DTO.Modules.Facturacion.Request;
using Fiby.Cloud.Web.DTO.Modules.Facturacion.Response;
using Fiby.Cloud.Web.Persistence.Interfaces.Facturacion;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Interfaces.Facturacion;
using Fiby.Cloud.Web.Util.Utility;
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
            //string uri = "http://localhost:58683/api/Venta/GenerarBoletaFactura/" + idVenta;
            //string uri = "http://factfiby.fibycloud.com/api/Venta/GenerarBoletaFactura/" + idVenta;
            //string uri = "http://rosita98-001-site1.atempurl.com/api/Venta/GenerarBoletaFactura/" + idVenta;
            string uri = "http://rosita99-001-site1.atempurl.com/api/Venta/GenerarBoletaFactura/" + idVenta;
            var result = await _httpClient.GetStringAsync(uri);
            return result;
        }

        public async Task<List<string>> RegistrarBaja(VentaDTORequest ventaDTORequest)
        {
            var response = await _generarRepository.RegistrarBaja(ventaDTORequest);
            return response;
        }

        public async Task<string> GenerarBaja(int idVenta)
        {
            //string uri = "http://localhost:58683/api/Operaciones/GenerarComunicacionBaja_XML/" + idVenta.ToString();
            //string uri = "http://factfiby.fibycloud.com/api/Operaciones/GenerarComunicacionBaja_XML/" + idVenta.ToString();
            //string uri = "http://rosita98-001-site1.atempurl.com/api/Operaciones/GenerarComunicacionBaja_XML/" + idVenta.ToString();
            string uri = "http://rosita99-001-site1.atempurl.com/api/Operaciones/GenerarComunicacionBaja_XML/" + idVenta.ToString();
            var result = await _httpClient.GetStringAsync(uri);
            return DataUtility.OkString(result);
        }

        public async Task<string> GenerarBajaBoleta(int idVenta)
        {
            //string uri = "http://localhost:58683/api/OperacionBoleta/GenerarResumenDiario_XML/" + idVenta.ToString();
            //string uri = "http://factfiby.fibycloud.com/api/OperacionBoleta/GenerarResumenDiario_XML/" + idVenta.ToString();
            //string uri = "http://rosita98-001-site1.atempurl.com/api/OperacionBoleta/GenerarResumenDiario_XML/" + idVenta.ToString();
            string uri = "http://rosita99-001-site1.atempurl.com/api/OperacionBoleta/GenerarResumenDiario_XML/" + idVenta.ToString();
            var result = await _httpClient.GetStringAsync(uri);
            return DataUtility.OkString(result);
        }

        public async Task<List<VentaDTOResponse>> ListarDocumentosGenerados(VentaDTORequest ventaDTORequest)
        {
            var response = await _generarRepository.ListarDocumentosGenerados(ventaDTORequest);
            return response;
        }

        public async Task<VentaDTOResponse> ListarVentaPorId(VentaDTORequest ventaDTORequest)
        {
            var response = await _generarRepository.ListarVentaPorId(ventaDTORequest);
            return response;
        }

        public async Task<List<DetalleVentaDTOResponse>> ListarDetallePorId(VentaDTORequest ventaDTORequest)
        {
            var response = await _generarRepository.ListarDetallePorId(ventaDTORequest);
            return response;
        }
    }
}
