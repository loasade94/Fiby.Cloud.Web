using Fiby.Cloud.Web.DTO.Modules.Horario.Request;
using Fiby.Cloud.Web.DTO.Modules.Horario.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Interfaces.Horario
{
    public interface ISemanaService
    {
        Task<List<SemanaDTOResponse>> GetDisponibilidadSemana(SemanaDTORequest semanaDTORequest);
        Task<List<SemanaDTOResponse>> GetListaSemana();
        Task<List<SemanaDTOResponse>> GetListaDiasXSemana(SemanaDTORequest semanaDTORequest);
        Task<List<SemanaDTOResponse>> GetListaHorario();
        Task<SemanaDTOResponse> GetRentabilidadGraficoDashboard();
        Task<SemanaDTOResponse> GetPasajesEmpleadoDashboard();
    }
}
