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
    public class CitaRepository : ICitaRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public CitaRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<CitaDTOResponse>> GetCitaAll(CitaDTORequest citaDTORequest)
        {
            var listResponse = new List<CitaDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@IdCita", null, direction: ParameterDirection.Input);
                parameters.Add("@FechaInicial", null, direction: ParameterDirection.Input);
                parameters.Add("@FechaFinal", null, direction: ParameterDirection.Input);
                parameters.Add("@Especialidad", null, direction: ParameterDirection.Input);
                parameters.Add("@CodigoUnicoDoctor", null, direction: ParameterDirection.Input);
                parameters.Add("@CodigoPaciente", null, direction: ParameterDirection.Input);
                parameters.Add("@@SituacionCita", null, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspCitasLst";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new CitaDTOResponse
                        {
                            IdCita = DataUtility.ObjectToInt(result["IdCita"]),
                            FechaCita = DataUtility.ObjectToDateTime(result["FechaCita"]),
                            Hora = DataUtility.ObjectToString(result["Hora"]),
                            CodigoEspecialidad = DataUtility.ObjectToString(result["Especialidad"]),
                            DescripcionEspecialidad = DataUtility.ObjectToString(result["DescripcionEspecialidad"]),
                            CodigoUnicoDoctor = DataUtility.ObjectToString(result["CodigoUnicoDoctor"]),
                            NombreDoctor = DataUtility.ObjectToString(result["NombreDoctor"]),
                            CodigoPaciente = DataUtility.ObjectToInt(result["CodigoPaciente"]),
                            NombrePaciente = DataUtility.ObjectToString(result["NombrePaciente"]),
                            DocumentoPaciente = DataUtility.ObjectToString(result["DniPaciente"]),
                            SituacionCita = DataUtility.ObjectToString(result["SituacionCita"]),
                            DescripcionSituacionCita = DataUtility.ObjectToString(result["DescripcionEstado"])
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
