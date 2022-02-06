using AutoMapper;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Request;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Response;
using Fiby.Cloud.Web.Persistence.Interfaces.Reportes;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Interfaces.Reportes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations.Reportes
{
    public class ReporteSemanaService : IReporteSemanaService
    {
        private readonly IReporteSemanaRepository _reporteSemanaRepository;
        private readonly IMapper _mapper;
        private readonly IHttpClient _httpClient;
        public ReporteSemanaService(IHttpClient httpClient, IReporteSemanaRepository reporteSemanaRepository, IMapper mapper)
        {
            _httpClient = httpClient;
            _reporteSemanaRepository = reporteSemanaRepository;
            _mapper = mapper;
        }

        public async Task<List<ReporteSemanaDTOResponse>> GetReporteRentabilidadSemanal(ReporteSemanaDTORequest reporteSemanaDTORequest)
        {
            var response = await _reporteSemanaRepository.GetReporteRentabilidadSemanal(reporteSemanaDTORequest);
            return response;
        }
        public async Task<List<ReporteSemanaDTOResponse>> GetReporteRentabilidadSemanalEmpleado(ReporteSemanaDTORequest reporteSemanaDTORequest)
        {
            var response = await _reporteSemanaRepository.GetReporteRentabilidadSemanalEmpleado(reporteSemanaDTORequest);
            return response;
        }
        public async Task<List<AnuncioDTOResponse>> GetAnunciosParaEmpleados()
        {
            var response = await _reporteSemanaRepository.GetAnunciosParaEmpleados();
            return response;
        }
    }
}
