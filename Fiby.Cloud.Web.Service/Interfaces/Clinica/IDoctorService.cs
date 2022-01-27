using Fiby.Cloud.Web.DTO.Modules.Clinica.Request;
using Fiby.Cloud.Web.DTO.Modules.Clinica.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Interfaces.Clinica
{
    public interface IDoctorService
    {
        Task<List<DoctorDTOResponse>> GetDoctorPorEspecialidad(DoctorDTORequest doctorDTORequest);
    }
}
