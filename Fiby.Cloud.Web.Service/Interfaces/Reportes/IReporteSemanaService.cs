using Fiby.Cloud.Web.DTO.Modules.Reportes.Request;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Interfaces.Reportes
{
    public interface IReporteSemanaService
    {
        Task<List<ReporteSemanaDTOResponse>> GetReporteRentabilidadSemanal(ReporteSemanaDTORequest reporteSemanaDTORequest);
        Task<List<AnuncioDTOResponse>> GetAnunciosParaEmpleados();
    }
}
