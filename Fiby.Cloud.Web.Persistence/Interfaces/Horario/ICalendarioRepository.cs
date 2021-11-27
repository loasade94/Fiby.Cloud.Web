using Fiby.Cloud.Web.DTO.Modules.Horario.Request;
using Fiby.Cloud.Web.DTO.Modules.Horario.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Interfaces.Horario
{
    public interface ICalendarioRepository
    {
        Task<List<string>> RegistrarServicio(CalendarioDTORequest calendarioDTORequest);
        Task<List<CalendarioDTOResponse>> GetServicioXEmpleado(CalendarioDTORequest calendarioDTORequest);
        Task<List<CalendarioDTOResponse>> GetServicioXEmpleadoCalendario(CalendarioDTORequest calendarioDTORequest);
        Task<List<CalendarioDTOResponse>> GetServicioXEmpleadoTotales(CalendarioDTORequest calendarioDTORequest);
        Task<string> EliminarServicio(CalendarioDTORequest calendarioDTORequest);
        Task<CalendarioDTOResponse> GetCalendarioById(CalendarioDTORequest calendarioDTORequest);
    }
}
