using Fiby.Cloud.Web.DTO.Modules.Maintenance.Request;
using Fiby.Cloud.Web.DTO.Modules.Maintenance.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Interfaces.Maintenance
{
    public interface IEmpleadoRepository
    {
        Task<List<EmpleadoDTOResponse>> GetEmpleadoAll();
        Task<List<EmpleadoDTOResponse>> GetEmpleadoApellido(EmpleadoDTORequest empleadoDTORequest);
    }
}
