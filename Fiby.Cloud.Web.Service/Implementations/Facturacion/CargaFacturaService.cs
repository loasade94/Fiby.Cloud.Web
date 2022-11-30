using Fiby.Cloud.Web.DTO.Modules.Facturacion.Request;
using Fiby.Cloud.Web.DTO.Modules.Facturacion.Response;
using Fiby.Cloud.Web.Persistence.Interfaces.Facturacion;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Interfaces.Facturacion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations.Facturacion
{
    public class CargaFacturaService : ICargaFacturaService
    {
        private readonly ICargaFacturaRepository _cargaFacturaRepository;
        private readonly IHttpClient _httpClient;

        public CargaFacturaService(IHttpClient httpClient, ICargaFacturaRepository cargaFacturaRepository)
        {
            _cargaFacturaRepository = cargaFacturaRepository;
            _httpClient = httpClient;
        }

        public async Task<List<string>> RegistrarFactura(CargaFacturaDTORequest cargaFacturaDTORequest)
        {
            var response = await _cargaFacturaRepository.RegistrarFactura(cargaFacturaDTORequest);
            return response;
        }

        public async Task<List<CargaFacturaDTOResponse>> ConsultaFacturas(CargaFacturaDTORequest cargaFacturaDTORequest)
        {
            var response = await _cargaFacturaRepository.ConsultaFacturas(cargaFacturaDTORequest);
            return response;
        }

        public async Task<List<string>> GetPle0801(CargaFacturaDTORequest cargaFacturaDTORequest)
        {
            var response = await _cargaFacturaRepository.GetPle0801(cargaFacturaDTORequest);
            return response;
        }

        public async Task<List<string>> GetPle1401(CargaFacturaDTORequest cargaFacturaDTORequest)
        {
            var response = await _cargaFacturaRepository.GetPle1401(cargaFacturaDTORequest);
            return response;
        }

        public async Task<string> EliminarFactura(CargaFacturaDTORequest cargaFacturaDTORequest)
        {
            var response = await _cargaFacturaRepository.EliminarFactura(cargaFacturaDTORequest);
            return response;
        }
    }
}
