using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Inversiones.Request;
using Fiby.Cloud.Web.DTO.Modules.Inversiones.Response;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces.Inversiones;
using Fiby.Cloud.Web.Util.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Implementations.Inversiones
{
    public class GastoRepository : IGastoRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public GastoRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<string> RegistrarGasto(GastoDTORequest gastoDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdGasto", gastoDTORequest.IdGasto, direction: ParameterDirection.Input);
                parameters.Add("@pIdSemana", gastoDTORequest.IdSemana, direction: ParameterDirection.Input);
                parameters.Add("@pIdCategoria", gastoDTORequest.IdCategoria, direction: ParameterDirection.Input);
                parameters.Add("@pDescripcion", gastoDTORequest.Descripcion, direction: ParameterDirection.Input);
                parameters.Add("@pMonto", gastoDTORequest.Monto, direction: ParameterDirection.Input);
                parameters.Add("@pAdicional", gastoDTORequest.Adicional, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspInsGasto";
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

        public async Task<List<GastoDTOResponse>> GetGastoAll(GastoDTORequest gastoDTORequest)
        {
            var listResponse = new List<GastoDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdSemana", gastoDTORequest.IdSemana, direction: ParameterDirection.Input);
                parameters.Add("@pDescripcion", gastoDTORequest.Descripcion, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListarGastos";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new GastoDTOResponse
                        {
                            IdGasto = DataUtility.ObjectToInt(result["IdGasto"]),
                            IdSemana = DataUtility.ObjectToInt(result["IdSemana"]),
                            NombreSemana = DataUtility.ObjectToString(result["NombreSemana"]),
                            Descripcion = DataUtility.ObjectToString(result["DesGasto"]),
                            Monto = DataUtility.ObjectToDecimal(result["Monto"]),
                            IdCategoria = DataUtility.ObjectToInt(result["IdCategoria"]),
                            NombreCategoria = DataUtility.ObjectToString(result["DesCategoria"])
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

        public async Task<string> EliminarGasto(GastoDTORequest gastoDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdGasto", gastoDTORequest.IdGasto, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspDelGasto";
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

        public async Task<GastoDTOResponse> GetGastoPorCodigo(GastoDTORequest gastoDTORequest)
        {
            var listResponse = new GastoDTOResponse();
            try
            {
                var parameters = new DynamicParameters();

                //parameters.Add("@pIdEmpleado", calendarioDTORequest.IdEmpleado, direction: ParameterDirection.Input);
                parameters.Add("@pId", gastoDTORequest.IdGasto, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListarGastosPorId";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse = new GastoDTOResponse
                        {
                            IdGasto = DataUtility.ObjectToInt(result["IdGasto"]),
                            IdSemana = DataUtility.ObjectToInt(result["IdSemana"]),
                            NombreSemana = DataUtility.ObjectToString(result["NombreSemana"]),
                            Descripcion = DataUtility.ObjectToString(result["DesGasto"]),
                            Monto = DataUtility.ObjectToDecimal(result["Monto"]),
                            IdCategoria = DataUtility.ObjectToInt(result["IdCategoria"]),
                            NombreCategoria = DataUtility.ObjectToString(result["DesCategoria"])
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

    }
}
