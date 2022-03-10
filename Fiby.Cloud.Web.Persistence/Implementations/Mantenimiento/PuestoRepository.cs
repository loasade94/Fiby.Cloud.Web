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
    public class PuestoRepository : IPuestoRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public PuestoRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<PuestoDTOResponse>> GetPuestoAll(PuestoDTORequest puestoDTORequest)
        {
            var listResponse = new List<PuestoDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pDescripcionPuesto", puestoDTORequest.DescripcionPuesto, direction: ParameterDirection.Input);


                var cn = _connectionFactory.GetConnection();
                var sp = "uspPuestoLst";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new PuestoDTOResponse
                        {
                            IdPuesto = DataUtility.ObjectToInt32(result["IdPuesto"]),
                            DescripcionPuesto = DataUtility.ObjectToString(result["DescripcionPuesto"]),
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
        /*
        public async Task<string> GuardarPuesto(PuestoDTORequest puestoDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdEmpresa", puestoDTORequest.IdEmpresa, direction: ParameterDirection.Input);
                parameters.Add("@pTipoPuesto", puestoDTORequest.TipoPuesto, direction: ParameterDirection.Input);
                parameters.Add("@pNombres", puestoDTORequest.Nombres, direction: ParameterDirection.Input);
                parameters.Add("@pApellidoPaterno", puestoDTORequest.ApellidoPaterno, direction: ParameterDirection.Input);
                parameters.Add("@pApellidoMaterno", puestoDTORequest.ApellidoMaterno, direction: ParameterDirection.Input);
                parameters.Add("@pIdPuesto", puestoDTORequest.IdPuesto, direction: ParameterDirection.Input);
                parameters.Add("@pNroDocumento", puestoDTORequest.NumeroDocumento, direction: ParameterDirection.Input);
                parameters.Add("@pSituacionRegistro", puestoDTORequest.SituacionRegistro, direction: ParameterDirection.Input);
                parameters.Add("@pEspecialidadMedica", puestoDTORequest.EspecialidadMedica, direction: ParameterDirection.Input);
                parameters.Add("@pTelefono", puestoDTORequest.Telefono, direction: ParameterDirection.Input);
                parameters.Add("@pSexo", puestoDTORequest.Sexo, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspInsPuesto";
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

        public async Task<string> EditarPuesto(PuestoDTORequest puestoDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pCodigoUnico", puestoDTORequest.CodigoUnico, direction: ParameterDirection.Input);
                parameters.Add("@pIdEmpresa", puestoDTORequest.IdEmpresa, direction: ParameterDirection.Input);
                parameters.Add("@pTipoPuesto", puestoDTORequest.TipoPuesto, direction: ParameterDirection.Input);
                parameters.Add("@pNombres", puestoDTORequest.Nombres, direction: ParameterDirection.Input);
                parameters.Add("@pApellidoPaterno", puestoDTORequest.ApellidoPaterno, direction: ParameterDirection.Input);
                parameters.Add("@pApellidoMaterno", puestoDTORequest.ApellidoMaterno, direction: ParameterDirection.Input);
                parameters.Add("@pEstado", puestoDTORequest.SituacionRegistro, direction: ParameterDirection.Input);
                parameters.Add("@pIdPuesto", puestoDTORequest.IdPuesto, direction: ParameterDirection.Input);
                parameters.Add("@pNroDocumento", puestoDTORequest.NumeroDocumento, direction: ParameterDirection.Input);
                parameters.Add("@pSituacionRegistro", puestoDTORequest.SituacionRegistro, direction: ParameterDirection.Input);
                parameters.Add("@pEspecialidadMedica", puestoDTORequest.EspecialidadMedica, direction: ParameterDirection.Input);
                parameters.Add("@pTelefono", puestoDTORequest.Telefono, direction: ParameterDirection.Input);
                parameters.Add("@pSexo", puestoDTORequest.Sexo, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspUdpPuesto";
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

        public async Task<PuestoDTOResponse> GetPuestoPorId(PuestoDTORequest puestoDTORequest)
        {
            var listResponse = new PuestoDTOResponse();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pCodigoPuesto", puestoDTORequest.CodigoPuesto, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspObtenerPuestoPorId";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse = new PuestoDTOResponse
                        {
                            CodigoUnico = DataUtility.ObjectToString(result["CodigoUnico"]),
                            TipoPuesto = DataUtility.ObjectToString(result["TipoPuesto"]),
                            DescripcionTipoPuesto = DataUtility.ObjectToString(result["DescripcionTipoPuesto"]),
                            Nombres = DataUtility.ObjectToString(result["Nombres"]),
                            ApellidoPaterno = DataUtility.ObjectToString(result["ApellidoPaterno"]),
                            ApellidoMaterno = DataUtility.ObjectToString(result["ApellidoMaterno"]),
                            NumeroDocumento = DataUtility.ObjectToString(result["NumeroDocumento"]),
                            CodigoPuesto = DataUtility.ObjectToInt(result["CodigoPuesto"]),
                            Telefono = DataUtility.ObjectToString(result["Telefono"]),
                            Sexo = DataUtility.ObjectToString(result["Sexo"]),
                            SexoDescripcion = DataUtility.ObjectToString(result["SexoDescripcion"]),
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

        public async Task<string> EliminarPuesto(PuestoDTORequest puestoDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pCodigoPuesto", puestoDTORequest.CodigoPuesto, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspDelPuesto";
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
        */
    }
}
