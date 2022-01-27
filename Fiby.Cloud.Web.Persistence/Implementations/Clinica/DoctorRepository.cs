using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Clinica.Request;
using Fiby.Cloud.Web.DTO.Modules.Clinica.Response;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces.Clinica;
using Fiby.Cloud.Web.Util.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Implementations.Clinica
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public DoctorRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<DoctorDTOResponse>> GetDoctorPorEspecialidad(DoctorDTORequest doctorDTORequest)
        {
            var listResponse = new List<DoctorDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@CodigoEspecialidad", doctorDTORequest.CodigoEspecialidad, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspBuscarDoctorPorEspecialidadLst";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new DoctorDTOResponse
                        {
                            CodigoDoctor = DataUtility.ObjectToString(result["CodigoTrabajador"]),
                            NombreDoctor = DataUtility.ObjectToString(result["NombreDoctor"])
                        });
                    }
                }


            }
            catch (Exception ex)
            {
                listResponse = null;
            }
            return listResponse;
        }
    }
}
