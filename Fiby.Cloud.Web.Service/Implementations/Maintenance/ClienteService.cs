using Fiby.Cloud.Cross.Util.DTOGeneric;
using Fiby.Cloud.Web.DTO.Modules.Maintenance.Request;
using Fiby.Cloud.Web.DTO.Modules.Maintenance.Response;
using Fiby.Cloud.Web.Persistence.Interfaces.Maintenance;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Interfaces.Maintenance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations.Maintenance
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IHttpClient _httpClient;

        public ClienteService(IClienteRepository clienteRepository,
                                IHttpClient httpClient)
        {
            _clienteRepository = clienteRepository;
            _httpClient = httpClient;
        }

        public async Task<string> RegistrarCliente(ClienteDTORequest clienteDTORequest)
        {
            var response = await _clienteRepository.RegistrarCliente(clienteDTORequest);
            return response;
        }

        public async Task<List<ClienteDTOResponse>> GetClienteAll()
        {
            var response = await _clienteRepository.GetClienteAll();
            return response;
        }

        public async Task<string> EliminarCliente(ClienteDTORequest clienteDTORequest)
        {
            var response = await _clienteRepository.EliminarCliente(clienteDTORequest);
            return response;
        }

        public async Task<ClienteDTOResponse> GetClientePorCodigo(ClienteDTORequest clienteDTORequest)
        {
            var response = await _clienteRepository.GetClientePorCodigo(clienteDTORequest);
            return response;
        }

        public async Task<DocumentoEmpresaDTOResponse> GetEmpresaPorDocumento(string ruc)
        {
            string uri = "https://api.apis.net.pe/v1/ruc?numero=" + ruc;
            var result = await _httpClient.GetStringAsync(uri, "apis-token-1920.CY9YCPjJ7yeY3dyXsCXaaiVjfK7lhPjq");
            var response = JsonConvert.DeserializeObject<DocumentoEmpresaDTOResponse>(result);
            return response;
        }

        public async Task<DocumentoEmpresaDTOResponse> GetPersonaPorDocumento(string dni)
        {
            string uri = "https://api.apis.net.pe/v1/dni?numero=" + dni;
            var result = await _httpClient.GetStringAsync(uri, "apis-token-1920.CY9YCPjJ7yeY3dyXsCXaaiVjfK7lhPjq");
            var response = JsonConvert.DeserializeObject<DocumentoEmpresaDTOResponse>(result);
            return response;
        }

        public async Task<ClienteDTOResponse> GetClientePorDocumento(ClienteDTORequest clienteDTORequest)
        {
            var response = await _clienteRepository.GetClientePorDocumento(clienteDTORequest);
            return response;
        }
    }
}
