using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Horario.Request;
using Fiby.Cloud.Web.DTO.Modules.Horario.Response;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces.Horario;
using Fiby.Cloud.Web.Util.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Implementations.Horario
{
    public class SemanaRepository : ISemanaRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public SemanaRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<SemanaDTOResponse>> GetDisponibilidadSemana(SemanaDTORequest semanaDTORequest)
        {
            var listResponse = new List<SemanaDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdSemana", semanaDTORequest.IdSemana, direction: ParameterDirection.Input);
                
                var cn = _connectionFactory.GetConnection();
                var sp = "uspListarDisponibilidadEmpleados";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new SemanaDTOResponse
                        {
                            IdSemana = DataUtility.ObjectToInt(result["IdSemana"]),
                            Horario = DataUtility.ObjectToString(result["Horario"]),
                            Lunes = DataUtility.ObjectToString(result["Lunes"]),
                            Martes = DataUtility.ObjectToString(result["Martes"]),
                            Miercoles = DataUtility.ObjectToString(result["Miercoles"]),
                            Jueves = DataUtility.ObjectToString(result["Jueves"]),
                            Viernes = DataUtility.ObjectToString(result["Viernes"]),
                            Sabado = DataUtility.ObjectToString(result["Sabado"]),
                            Domingo = DataUtility.ObjectToString(result["Domingo"])
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

        public async Task<List<SemanaDTOResponse>> GetListaSemana()
        {
            var listResponse = new List<SemanaDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListarSemana";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new SemanaDTOResponse
                        {
                            IdSemana = DataUtility.ObjectToInt(result["IdSemana"]),
                            NombreSemana = DataUtility.ObjectToString(result["NombreSemana"]),
                            Prioridad = DataUtility.ObjectToInt(result["Prioridad"])
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

        public async Task<List<SemanaDTOResponse>> GetListaDiasXSemana(SemanaDTORequest semanaDTORequest)
        {
            var listResponse = new List<SemanaDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdSemana", semanaDTORequest.IdSemana, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListarDiasPorSemana";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new SemanaDTOResponse
                        {
                            Dia = DataUtility.ObjectToString(result["NombreDia"]),
                            Fecha = DataUtility.ObjectToDateTime(result["Fecha"])
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

        public async Task<List<SemanaDTOResponse>> GetListaHorario()
        {
            var listResponse = new List<SemanaDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                //parameters.Add("@pIdSemana", semanaDTORequest.IdSemana, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "ListaHoras";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new SemanaDTOResponse
                        {
                            Horario = DataUtility.ObjectToString(result["Hora"])
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

        public async Task<SemanaDTOResponse> GetRentabilidadGraficoDashboard()
        {
            SemanaDTOResponse authModel = new SemanaDTOResponse();
            List<SemanaDTOResponse> ListaNombres = new List<SemanaDTOResponse>();
            List<SemanaDTOResponse> ListaMontos = new List<SemanaDTOResponse>();
            var parameters = new DynamicParameters();

            //parameters.Add("@pIdUsuario", userDTORequest.UserId, direction: ParameterDirection.Input);
            var sp = "uspListarEstadisticasGanancias";
            var cn = _connectionFactory.GetConnection();
            var result = await cn.ExecuteReaderAsync(
                   sp,
                   parameters,
                   commandTimeout: 0,
                   commandType: CommandType.StoredProcedure
               );

            if (result.FieldCount > 0)
            {

                #region NOMBRES DE FECHA

                //DATOS DE USUARIO
                while (result.Read())
                {
                    ListaNombres.Add(new SemanaDTOResponse
                    {
                        NombreSemana = DataUtility.ObjectToString(result["NombreSemana"])
                    });
                }
                authModel.ListaNombres = ListaNombres;

                result.NextResult();

                #endregion

                #region DATOS DE TIENDA
                //DATOS DE TIENDA

                while (result.Read())
                {
                    ListaMontos.Add(new SemanaDTOResponse
                    {
                        Monto = DataUtility.ObjectToDecimal(result["Monto"])
                    });
                }
                authModel.ListaMontos = ListaMontos;

                result.NextResult();
                #endregion

                result.Close();
            }


            return authModel;
        }
    }
}
