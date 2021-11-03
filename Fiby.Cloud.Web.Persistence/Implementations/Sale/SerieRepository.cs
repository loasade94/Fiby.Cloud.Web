using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Sale.Request.Serie;
using Fiby.Cloud.Web.DTO.Modules.Sale.Response.Serie;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces;
using Fiby.Cloud.Web.Util.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Implementations.Sale
{
    public class SerieRepository : ISerieRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public SerieRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<SerieDTOResponse>> GetSerieAll(SerieDTORequest rolDTORequest)
        {
            var listResponse = new List<SerieDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("pDescripcion", rolDTORequest.Description, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "usp_ObtenerSeries";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new SerieDTOResponse
                        {
                            SerieId = DataUtility.ObjectToInt32(result["IdSerie"]),
                            Description = DataUtility.ObjectToString(result["SERIE"])
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

        public async Task<string> RegisterSerie(SerieDTORequest rolDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("pDescripcion", rolDTORequest.Description, direction: ParameterDirection.Input);
                parameters.Add("pActivo", rolDTORequest.Active == true ? 1 : 0, direction: ParameterDirection.Input);

                parameters.Add("pResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "usp_RegistrarSerie";
                var cn = _connectionFactory.GetConnection();
                var rpta = await cn.ExecuteReaderAsync(
                        sp,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                string err = (parameters.Get<string>("pResultado") == null ?
                    string.Empty :
                    parameters.Get<string>("pResultado"));

                result = err;
            }
            catch (Exception ex)
            {

                result = ex.Message;
            }

            return result;

        }

        public async Task<string> DeleteSerie(SerieDTORequest rolDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("pIdSerie", rolDTORequest.SerieId, direction: ParameterDirection.Input);

                parameters.Add("pResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "usp_EliminarSerie";
                var cn = _connectionFactory.GetConnection();
                var rpta = await cn.ExecuteReaderAsync(
                        sp,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                string err = (parameters.Get<string>("pResultado") == null ?
                    string.Empty :
                    parameters.Get<string>("pResultado"));

                result = err;
            }
            catch (Exception ex)
            {

                result = ex.Message;
            }

            return result;

        }

        public async Task<string> UpdateSerie(SerieDTORequest rolDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("pIdSerie", rolDTORequest.SerieId, direction: ParameterDirection.Input);
                parameters.Add("pDescripcion", rolDTORequest.Description, direction: ParameterDirection.Input);
                parameters.Add("pActivo", rolDTORequest.Active == true ? 1 : 0, direction: ParameterDirection.Input);

                parameters.Add("pResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "usp_ModificarSerie";
                var cn = _connectionFactory.GetConnection();
                var rpta = await cn.ExecuteReaderAsync(
                        sp,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                string err = (parameters.Get<string>("pResultado") == null ?
                    string.Empty :
                    parameters.Get<string>("pResultado"));

                result = err;
            }
            catch (Exception ex)
            {

                result = ex.Message;
            }

            return result;

        }

        public async Task<SerieDTOResponse> GetSerieById(SerieDTORequest rolDTORequest)
        {
            SerieDTOResponse rolModel = new SerieDTOResponse();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("pIdSerie", rolDTORequest.SerieId, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "usp_ObtenerSerieesXId";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        rolModel = new SerieDTOResponse()
                        {
                            SerieId = DataUtility.ObjectToInt(result["IdSerie"]),
                            Description = DataUtility.ObjectToString(result["Descripcion"]),
                            Active = DataUtility.ObjectToBool(result["Activo"])
                        };
                    }
                }


            }
            catch (Exception ex)
            {
                rolModel = null;
            }
            return rolModel;
        }
    }
}
