using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Request;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Response;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces.Maintenance;
using Fiby.Cloud.Web.Util.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Implementations.Maintenance
{
    public class AnuncioRepository : IAnuncioRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public AnuncioRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<string> RegistrarAnuncio(AnuncioDTORequest gastoDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdAuncio", gastoDTORequest.IdAnuncio, direction: ParameterDirection.Input);
                parameters.Add("@pTitulo", gastoDTORequest.Titulo, direction: ParameterDirection.Input);
                parameters.Add("@pDetalle", gastoDTORequest.Detalle, direction: ParameterDirection.Input);
                parameters.Add("@pOrden", gastoDTORequest.Orden, direction: ParameterDirection.Input);
                parameters.Add("@pFechaInicio", gastoDTORequest.FechaInicio, direction: ParameterDirection.Input);
                parameters.Add("@pFechaFin", gastoDTORequest.FechaFin, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspInsAnuncio";
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

        public async Task<List<AnuncioDTOResponse>> GetAnuncioAll(AnuncioDTORequest gastoDTORequest)
        {
            var listResponse = new List<AnuncioDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pTitulo", gastoDTORequest.Titulo, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListarAnuncios";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new AnuncioDTOResponse
                        {
                            IdAnuncio = DataUtility.ObjectToInt(result["IdAnuncio"]),
                            Titulo = DataUtility.ObjectToString(result["Titulo"]),
                            Detalle = DataUtility.ObjectToString(result["Detalle"]),
                            Orden = DataUtility.ObjectToInt(result["Orden"]),
                            FechaInicio = DataUtility.ObjectToDateTime(result["FechaInicio"]),
                            FechaFin = DataUtility.ObjectToDateTime(result["FechaFin"])
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

        public async Task<string> EliminarAnuncio(AnuncioDTORequest gastoDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdAnuncio", gastoDTORequest.IdAnuncio, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspDelAnuncios";
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

        public async Task<AnuncioDTOResponse> GetAnuncioPorCodigo(AnuncioDTORequest gastoDTORequest)
        {
            var listResponse = new AnuncioDTOResponse();
            try
            {
                var parameters = new DynamicParameters();

                //parameters.Add("@pIdEmpleado", calendarioDTORequest.IdEmpleado, direction: ParameterDirection.Input);
                parameters.Add("@pId", gastoDTORequest.IdAnuncio, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListarAnunciosPorId";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse = new AnuncioDTOResponse
                        {
                            IdAnuncio = DataUtility.ObjectToInt(result["IdAnuncio"]),
                            Titulo = DataUtility.ObjectToString(result["Titulo"]),
                            Detalle = DataUtility.ObjectToString(result["Detalle"]),
                            Orden = DataUtility.ObjectToInt(result["Orden"]),
                            FechaInicio = DataUtility.ObjectToDateTime(result["FechaInicio"]),
                            FechaFin = DataUtility.ObjectToDateTime(result["FechaFin"])
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
