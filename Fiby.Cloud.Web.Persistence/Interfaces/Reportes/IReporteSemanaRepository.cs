using Fiby.Cloud.Web.DTO.Modules.Reportes.Request;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Interfaces.Reportes
{
    public interface IReporteSemanaRepository
    {
        Task<List<ReporteSemanaDTOResponse>> GetReporteRentabilidadSemanal(ReporteSemanaDTORequest pagoEmpleadoDTORequest);
        Task<List<ReporteSemanaDTOResponse>> GetReporteRentabilidadSemanalEmpleado(ReporteSemanaDTORequest pagoEmpleadoDTORequest);
        Task<List<AnuncioDTOResponse>> GetAnunciosParaEmpleados();
    }
}
