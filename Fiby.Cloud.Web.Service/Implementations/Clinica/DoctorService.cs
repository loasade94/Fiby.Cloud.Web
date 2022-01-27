using Fiby.Cloud.Web.DTO.Modules.Clinica.Request;
using Fiby.Cloud.Web.DTO.Modules.Clinica.Response;
using Fiby.Cloud.Web.Persistence.Interfaces.Clinica;
using Fiby.Cloud.Web.Service.Interfaces.Clinica;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Implementations.Clinica
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public async Task<List<DoctorDTOResponse>> GetDoctorPorEspecialidad(DoctorDTORequest doctorDTORequest)
        {
            var response = await _doctorRepository.GetDoctorPorEspecialidad(doctorDTORequest);
            return response;
        }
    }
}
