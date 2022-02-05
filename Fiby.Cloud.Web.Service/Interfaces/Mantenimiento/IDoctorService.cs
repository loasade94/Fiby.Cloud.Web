using Fiby.Cloud.Web.DTO.Modules.Mantenimiento.Request;
using Fiby.Cloud.Web.DTO.Modules.Mantenimiento.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Interfaces.Mantenimiento
{
    public interface IDoctorService
    {
        Task<List<DoctorDTOResponse>> GetDoctorPorEspecialidad(DoctorDTORequest doctorDTORequest);
    }
}
