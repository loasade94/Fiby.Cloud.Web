using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Gestion.Request;
using Fiby.Cloud.Web.DTO.Modules.Gestion.Response;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces.Gestion;
using Fiby.Cloud.Web.Util.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Implementations.Gestion
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
                parameters.Add("@SituacionCita", null, direction: ParameterDirection.Input);

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
                            CodigoDoctor = DataUtility.ObjectToString(result["CodigoTrabajador"]),
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

        public async Task<string> GuardarCita(CitaDTORequest citaDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdCita", citaDTORequest.IdCita, direction: ParameterDirection.Input);
                parameters.Add("@pFechaCita", citaDTORequest.FechaCita, direction: ParameterDirection.Input);
                parameters.Add("@pHora", citaDTORequest.Hora, direction: ParameterDirection.Input);
                parameters.Add("@pEspecialidad", citaDTORequest.CodigoEspecialidad, direction: ParameterDirection.Input);
                parameters.Add("@pCodigoUnicoDoctor", citaDTORequest.CodigoUnicoDoctor, direction: ParameterDirection.Input);
                parameters.Add("@pCodigoPaciente", citaDTORequest.CodigoPaciente, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspInsCita";
                var cn = _connectionFactory.GetConnection();
                var rpta = await cn.ExecuteReaderAsync(
                        sp,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                string err = (parameters.Get<string>("pMensajeResultado") == null ?
                    string.Empty :
                    parameters.Get<string>("pMensajeResultado"));

                result = err;
            }
            catch (Exception ex)
            {

                result = ex.Message;
            }

            return result;

        }

        public async Task<string> EditarCita(CitaDTORequest citaDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdCita", citaDTORequest.IdCita, direction: ParameterDirection.Input);
                parameters.Add("@pFechaCita", citaDTORequest.FechaCita, direction: ParameterDirection.Input);
                parameters.Add("@pHora", citaDTORequest.Hora, direction: ParameterDirection.Input);
                parameters.Add("@pEspecialidad", citaDTORequest.CodigoEspecialidad, direction: ParameterDirection.Input);
                parameters.Add("@pCodigoUnicoDoctor", citaDTORequest.CodigoUnicoDoctor, direction: ParameterDirection.Input);
                parameters.Add("@pCodigoPaciente", citaDTORequest.CodigoPaciente, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspInsCita";
                var cn = _connectionFactory.GetConnection();
                var rpta = await cn.ExecuteReaderAsync(
                        sp,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                string err = (parameters.Get<string>("pMensajeResultado") == null ?
                    string.Empty :
                    parameters.Get<string>("pMensajeResultado"));

                result = err;
            }
            catch (Exception ex)
            {

                result = ex.Message;
            }

            return result;

        }

        public async Task<CitaDTOResponse> GetCitaPorId(CitaDTORequest citaDTORequest)
        {
            var listResponse = new CitaDTOResponse();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@IdCita", citaDTORequest.IdCita, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspObtenerCitaPorId";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse = new CitaDTOResponse
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
                        };
                    }
                }


            }
            catch (Exception ex)
            {
                listResponse = null;
            }
            return listResponse;
        }

        public async Task<string> EliminarCita(CitaDTORequest citaDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdCita", citaDTORequest.IdCita, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspDelCita";
                var cn = _connectionFactory.GetConnection();
                var rpta = await cn.ExecuteReaderAsync(
                        sp,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                string err = (parameters.Get<string>("pMensajeResultado") == null ?
                    string.Empty :
                    parameters.Get<string>("pMensajeResultado"));

                result = err;
            }
            catch (Exception ex)
            {

                result = ex.Message;
            }

            return result;

        }
    }
}
