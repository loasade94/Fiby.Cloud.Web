using Fiby.Cloud.Web.DTO.Modules.Maintenance.Request;
using Fiby.Cloud.Web.DTO.Modules.Maintenance.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Interfaces
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoDTOResponse>> GetEmpleadoAll();
        Task<List<EmpleadoDTOResponse>> GetEmpleadoAll(EmpleadoDTORequest empleadoDTORequest);
    }
}
