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
    }
}
