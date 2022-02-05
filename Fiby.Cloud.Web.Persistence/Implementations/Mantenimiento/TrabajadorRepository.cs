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
    public class TrabajadorRepository : ITrabajadorRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public TrabajadorRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<TrabajadorDTOResponse>> GetTrabajadorAll(TrabajadorDTORequest trabajadorDTORequest)
        {
            var listResponse = new List<TrabajadorDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pCodigoTrabajador", null, direction: ParameterDirection.Input);
                parameters.Add("@pDNI", null, direction: ParameterDirection.Input);
                parameters.Add("@pIdPuesto", null, direction: ParameterDirection.Input);
                parameters.Add("@SituacionRegistro", null, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspTrabajadorLst";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new TrabajadorDTOResponse
                        {
                            TipoTrabajador = DataUtility.ObjectToString(result["TipoTrabajador"]),
                            DescripcionTipoTrabajador = DataUtility.ObjectToString(result["DescripcionTipoTrabajador"]),
                            Nombres = DataUtility.ObjectToString(result["Nombres"]),
                            ApellidoPaterno = DataUtility.ObjectToString(result["ApellidoPaterno"]),
                            ApellidoMaterno = DataUtility.ObjectToString(result["ApellidoMaterno"]),
                            NumeroDocumento = DataUtility.ObjectToString(result["NumeroDocumento"]),
                            CodigoTrabajador = DataUtility.ObjectToInt(result["CodigoTrabajador"]),
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

        public async Task<string> GuardarTrabajador(TrabajadorDTORequest trabajadorDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdEmpresa", trabajadorDTORequest.IdEmpresa, direction: ParameterDirection.Input);
                parameters.Add("@pTipoTrabajador", trabajadorDTORequest.TipoTrabajador, direction: ParameterDirection.Input);
                parameters.Add("@pNombres", trabajadorDTORequest.Nombres, direction: ParameterDirection.Input);
                parameters.Add("@pApellidoPaterno", trabajadorDTORequest.ApellidoPaterno, direction: ParameterDirection.Input);
                parameters.Add("@pApellidoMaterno", trabajadorDTORequest.ApellidoMaterno, direction: ParameterDirection.Input);
                parameters.Add("@pIdPuesto", trabajadorDTORequest.IdPuesto, direction: ParameterDirection.Input);
                parameters.Add("@pNroDocumento", trabajadorDTORequest.NumeroDocumento, direction: ParameterDirection.Input);
                parameters.Add("@pSituacionRegistro", trabajadorDTORequest.SituacionRegistro, direction: ParameterDirection.Input);
                parameters.Add("@pEspecialidadMedica", trabajadorDTORequest.EspecialidadMedica, direction: ParameterDirection.Input);
                parameters.Add("@pTelefono", trabajadorDTORequest.Telefono, direction: ParameterDirection.Input);
                parameters.Add("@pSexo", trabajadorDTORequest.Sexo, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspInsTrabajador";
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

        public async Task<string> EditarTrabajador(TrabajadorDTORequest trabajadorDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pCodigoUnico", trabajadorDTORequest.CodigoUnico, direction: ParameterDirection.Input);
                parameters.Add("@pIdEmpresa", trabajadorDTORequest.IdEmpresa, direction: ParameterDirection.Input);
                parameters.Add("@pTipoTrabajador", trabajadorDTORequest.TipoTrabajador, direction: ParameterDirection.Input);
                parameters.Add("@pNombres", trabajadorDTORequest.Nombres, direction: ParameterDirection.Input);
                parameters.Add("@pApellidoPaterno", trabajadorDTORequest.ApellidoPaterno, direction: ParameterDirection.Input);
                parameters.Add("@pApellidoMaterno", trabajadorDTORequest.ApellidoMaterno, direction: ParameterDirection.Input);
                parameters.Add("@pEstado", trabajadorDTORequest.SituacionRegistro, direction: ParameterDirection.Input);
                parameters.Add("@pIdPuesto", trabajadorDTORequest.IdPuesto, direction: ParameterDirection.Input);
                parameters.Add("@pNroDocumento", trabajadorDTORequest.NumeroDocumento, direction: ParameterDirection.Input);
                parameters.Add("@pSituacionRegistro", trabajadorDTORequest.SituacionRegistro, direction: ParameterDirection.Input);
                parameters.Add("@pEspecialidadMedica", trabajadorDTORequest.EspecialidadMedica, direction: ParameterDirection.Input);
                parameters.Add("@pTelefono", trabajadorDTORequest.Telefono, direction: ParameterDirection.Input);
                parameters.Add("@pSexo", trabajadorDTORequest.Sexo, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspUdpTrabajador";
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

        public async Task<TrabajadorDTOResponse> GetTrabajadorPorId(TrabajadorDTORequest trabajadorDTORequest)
        {
            var listResponse = new TrabajadorDTOResponse();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pCodigoTrabajador", trabajadorDTORequest.CodigoTrabajador, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspObtenerTrabajadorPorId";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse = new TrabajadorDTOResponse
                        {
                            CodigoUnico = DataUtility.ObjectToString(result["CodigoUnico"]),
                            TipoTrabajador = DataUtility.ObjectToString(result["TipoTrabajador"]),
                            DescripcionTipoTrabajador = DataUtility.ObjectToString(result["DescripcionTipoTrabajador"]),
                            Nombres = DataUtility.ObjectToString(result["Nombres"]),
                            ApellidoPaterno = DataUtility.ObjectToString(result["ApellidoPaterno"]),
                            ApellidoMaterno = DataUtility.ObjectToString(result["ApellidoMaterno"]),
                            NumeroDocumento = DataUtility.ObjectToString(result["NumeroDocumento"]),
                            CodigoTrabajador = DataUtility.ObjectToInt(result["CodigoTrabajador"]),
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

        public async Task<string> EliminarTrabajador(TrabajadorDTORequest trabajadorDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pCodigoTrabajador", trabajadorDTORequest.CodigoTrabajador, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspDelTrabajador";
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
