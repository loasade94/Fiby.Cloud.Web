using Fiby.Cloud.Web.DTO.Modules.Maintenance.Request;
using Fiby.Cloud.Web.DTO.Modules.Maintenance.Response;
using Fiby.Cloud.Web.Persistence.Interfaces.Maintenance;
using Fiby.Cloud.Web.Service.Interfaces.Maintenance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations.Maintenance
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
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

    }
}
