using Fiby.Cloud.Web.DTO.Modules.Parametro.Request;
using Fiby.Cloud.Web.DTO.Modules.Parametro.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Interfaces.Parametro
{
    public interface ITablaDetalleRepository
    {
        Task<List<TablaDetalleDTOResponse>> GetTablaDetalleAll(TablaDetalleDTORequest TablaDetalleDTORequest);
    }
}
