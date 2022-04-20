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
        Task<List<DepartamentoDTOResponse>> GetDepartamentoPorCodigo(DepartamentoDTORequest departamentoDTORequest);
        Task<List<ProvinciaDTOResponse>> GetProvinciaPorCodigo(ProvinciaDTORequest provinciaDTORequest);
        Task<List<DistritoDTOResponse>> GetDistritoPorCodigo(DistritoDTORequest distritoDTORequest);
    }
}
