using Fiby.Cloud.Web.DTO.Modules.Parametro.Request;
using Fiby.Cloud.Web.DTO.Modules.Parametro.Response;
using Fiby.Cloud.Web.Persistence.Interfaces.Parametro;
using Fiby.Cloud.Web.Service.Interfaces.Parametro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations.Parametro
{
    public class TablaDetalleService : ITablaDetalleService
    {
        private readonly ITablaDetalleRepository _tablaDetalleRepository;

        public TablaDetalleService(ITablaDetalleRepository tablaDetalleRepository)
        {
            _tablaDetalleRepository = tablaDetalleRepository;
        }
        public async Task<List<TablaDetalleDTOResponse>> GetTablaDetalleAll(TablaDetalleDTORequest tablaDetalleDTORequest)
        {
            var response = await _tablaDetalleRepository.GetTablaDetalleAll(tablaDetalleDTORequest);
            return response;
        }

        public async Task<List<DepartamentoDTOResponse>> GetDepartamentoPorCodigo(DepartamentoDTORequest departamentoDTORequest)
        {
            var response = await _tablaDetalleRepository.GetDepartamentoPorCodigo(departamentoDTORequest);
            return response;
        }

        public async Task<List<ProvinciaDTOResponse>> GetProvinciaPorCodigo(ProvinciaDTORequest provinciaDTORequest)
        {
            var response = await _tablaDetalleRepository.GetProvinciaPorCodigo(provinciaDTORequest);
            return response;
        }

        public async Task<List<DistritoDTOResponse>> GetDistritoPorCodigo(DistritoDTORequest distritoDTORequest)
        {
            var response = await _tablaDetalleRepository.GetDistritoPorCodigo(distritoDTORequest);
            return response;
        }
    }
}
