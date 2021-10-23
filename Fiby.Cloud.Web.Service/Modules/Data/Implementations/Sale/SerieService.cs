using Fiby.Cloud.Web.Service.Modules.Data.Interfaces.Sale;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.Service.Modules.Data.Implementations.Sale
{
    public class RolService : IRolService
    {

        private readonly IRolRepository _rolSRepository;
        private readonly IMapper _mapper;

        public RolService(IRolRepository rolRepository, IMapper mapper)
        {
            _rolSRepository = rolRepository;
            _mapper = mapper;
        }

        public async Task<List<RolDTOResponse>> GetRolAll(RolDTORequest rolDTORequest);
        {
            var response = await _pleRepository.GetPleAll(pLE14100DTORequest);
            return response;
        }

        public async Task<string> RegisterRol(RolDTORequest rolDTORequest);
        {
            var response = await _pleRepository.RegistrarPle14100DPorMes(pLE14100DTORequest);
            return response;
        }

        public async DeleteRol(RolDTORequest rolDTORequest);
{
            var response = await _pleRepository.RegistrarPle14100DPorMes(pLE14100DTORequest);
            return response;
        }

        public async UpdateRol(RolDTORequest rolDTORequest);
{
            var response = await _pleRepository.RegistrarPle14100DPorMes(pLE14100DTORequest);
            return response;
        }

        public async Task<RolDTOResponse> GetRolById(RolDTORequest rolDTORequest);
{
            var response = await _pleRepository.GetPleAll(pLE14100DTORequest);
            return response;
        }
    }
}
