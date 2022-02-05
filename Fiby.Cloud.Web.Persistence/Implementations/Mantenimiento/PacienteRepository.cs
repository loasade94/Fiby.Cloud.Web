using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Mantenimiento.Request;
using Fiby.Cloud.Web.DTO.Modules.Mantenimiento.Response;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces.Mantenimiento;
using Fiby.Cloud.Web.Util.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Implementations.Mantenimiento
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public PacienteRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<PacienteDTOResponse> GetPacientePorDocumento(PacienteDTORequest PacienteDTORequest)
        {
            var listResponse = new PacienteDTOResponse();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@DocumentoPaciente", PacienteDTORequest.DocumentoPaciente, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspBuscarPacientePorDocumentoLst";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse = new PacienteDTOResponse()
                        {
                            CodigoPaciente = DataUtility.ObjectToInt32(result["CodigoPaciente"]),
                            NombrePaciente = DataUtility.ObjectToString(result["NombrePaciente"])
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

        public async Task<List<PacienteDTOResponse>> GetPacienteAll(PacienteDTORequest pacienteDTORequest)
        {
            var listResponse = new List<PacienteDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdPaciente", null, direction: ParameterDirection.Input);
                parameters.Add("@pDNI", null, direction: ParameterDirection.Input);
                parameters.Add("@pNombresPaciente", null, direction: ParameterDirection.Input);
                parameters.Add("@pApellidoPaciente", null, direction: ParameterDirection.Input);
                parameters.Add("@SituacionRegistro", null, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspPacientesLst";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new PacienteDTOResponse
                        {
                            CodigoPaciente = DataUtility.ObjectToInt(result["CodigoPaciente"]),
                            DniPaciente = DataUtility.ObjectToString(result["DniPaciente"]),
                            NombrePaciente = DataUtility.ObjectToString(result["NombresPaciente"]),
                            ApellidoPaciente = DataUtility.ObjectToString(result["ApellidosPaciente"]),
                            Seguro = DataUtility.ObjectToString(result["Seguro"]),
                            Telefono = DataUtility.ObjectToString(result["Telefono"]),
                            Sexo = DataUtility.ObjectToString(result["Sexo"]),
                            SexoDescripcion = DataUtility.ObjectToString(result["SexoDescripcion"]),
                            SituacionRegistro = DataUtility.ObjectToString(result["SituacionRegistro"])

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

        public async Task<string> GuardarPaciente(PacienteDTORequest pacienteDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdPaciente", pacienteDTORequest.CodigoPaciente, direction: ParameterDirection.Input);
                parameters.Add("@pDniPaciente", pacienteDTORequest.DniPaciente, direction: ParameterDirection.Input);
                parameters.Add("@pNombresPaciente", pacienteDTORequest.NombrePaciente, direction: ParameterDirection.Input);
                parameters.Add("@pApellidoPaciente", pacienteDTORequest.ApellidoPaciente, direction: ParameterDirection.Input);
                parameters.Add("@pSeguro", pacienteDTORequest.Seguro, direction: ParameterDirection.Input);
                parameters.Add("@pTelefono", pacienteDTORequest.Telefono, direction: ParameterDirection.Input);
                parameters.Add("@pSexo", pacienteDTORequest.Sexo, direction: ParameterDirection.Input);
                parameters.Add("@pSituacionRegistro", pacienteDTORequest.SituacionRegistro, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspInsPaciente";
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

        public async Task<string> EditarPaciente(PacienteDTORequest pacienteDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdPaciente", pacienteDTORequest.CodigoPaciente, direction: ParameterDirection.Input);
                parameters.Add("@pDniPaciente", pacienteDTORequest.DniPaciente, direction: ParameterDirection.Input);
                parameters.Add("@pNombresPaciente", pacienteDTORequest.NombrePaciente, direction: ParameterDirection.Input);
                parameters.Add("@pApellidoPaciente", pacienteDTORequest.ApellidoPaciente, direction: ParameterDirection.Input);
                parameters.Add("@pSeguro", pacienteDTORequest.Seguro, direction: ParameterDirection.Input);
                parameters.Add("@pTelefono", pacienteDTORequest.Telefono, direction: ParameterDirection.Input);
                parameters.Add("@pSexo", pacienteDTORequest.Sexo, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspInsPaciente";
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

        public async Task<PacienteDTOResponse> GetPacientePorId(PacienteDTORequest pacienteDTORequest)
        {
            var listResponse = new PacienteDTOResponse();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdPaciente", pacienteDTORequest.CodigoPaciente, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspObtenerPacientePorId";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse = new PacienteDTOResponse
                        {
                            CodigoPaciente = DataUtility.ObjectToInt(result["CodigoPaciente"]),
                            DniPaciente = DataUtility.ObjectToString(result["DniPaciente"]),
                            NombrePaciente = DataUtility.ObjectToString(result["NombresPaciente"]),
                            ApellidoPaciente = DataUtility.ObjectToString(result["ApellidosPaciente"]),
                            Seguro = DataUtility.ObjectToString(result["Seguro"]),
                            Telefono = DataUtility.ObjectToString(result["Telefono"]),
                            Sexo = DataUtility.ObjectToString(result["Sexo"]),
                            SituacionRegistro = DataUtility.ObjectToString(result["SituacionRegistro"])
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

        public async Task<string> EliminarPaciente(PacienteDTORequest pacienteDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdPaciente", pacienteDTORequest.CodigoPaciente, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspDelPaciente";
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
