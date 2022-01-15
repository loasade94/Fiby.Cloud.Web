using Fiby.Cloud.Web.DTO.Modules.Horario.Request;
using Fiby.Cloud.Web.DTO.Modules.Horario.Response;
using Fiby.Cloud.Web.DTO.Modules.Maintenance.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Interfaces.Horario
{
    public interface ISemanaRepository
    {
        Task<List<SemanaDTOResponse>> GetDisponibilidadSemana(SemanaDTORequest semanaDTORequest);
        Task<List<SemanaDTOResponse>> GetListaSemana();
        Task<List<SemanaDTOResponse>> GetListaDiasXSemana(SemanaDTORequest semanaDTORequest);
        Task<List<SemanaDTOResponse>> GetListaHorario();
        Task<SemanaDTOResponse> GetRentabilidadGraficoDashboard();
        Task<SemanaDTOResponse> GetPasajesEmpleadoDashboard();
        Task<List<SemanaDTOResponse>> GetListaSemanaPagadaXEmpleado(EmpleadoDTORequest empleadoDTORequest);
        Task<List<ServicioClienteDTOResponse>> GetListaServicioXCliente(ServicioClienteDTORequest servicioClienteDTORequest);
    }
}
