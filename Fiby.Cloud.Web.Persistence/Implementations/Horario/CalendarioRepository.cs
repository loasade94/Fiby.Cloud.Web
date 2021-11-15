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
    public class CalendarioRepository : ICalendarioRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public CalendarioRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<string> RegistrarServicio(CalendarioDTORequest calendarioDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdServicio", calendarioDTORequest.IdServicio, direction: ParameterDirection.Input);
                parameters.Add("@pIdEmpleado", calendarioDTORequest.IdEmpleado, direction: ParameterDirection.Input);
                parameters.Add("@pIdCliente", calendarioDTORequest.IdCliente, direction: ParameterDirection.Input);
                parameters.Add("@pCliente", calendarioDTORequest.ClienteOpcional, direction: ParameterDirection.Input);
                parameters.Add("@pDescripcion", calendarioDTORequest.Descripcion, direction: ParameterDirection.Input);
                parameters.Add("@pFecha", calendarioDTORequest.Fecha, direction: ParameterDirection.Input);
                parameters.Add("@pHoraInicio", calendarioDTORequest.HoraInicio, direction: ParameterDirection.Input);
                parameters.Add("@pHoraFin", calendarioDTORequest.HoraFin, direction: ParameterDirection.Input);
                parameters.Add("@pUsuario", calendarioDTORequest.UsuarioCreacion, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspInsServicioHorario";
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

        public async Task<List<CalendarioDTOResponse>> GetServicioXEmpleado(CalendarioDTORequest calendarioDTORequest)
        {
            var listResponse = new List<CalendarioDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdEmpleado", calendarioDTORequest.IdEmpleado, direction: ParameterDirection.Input);
                parameters.Add("@pFecha", calendarioDTORequest.Fecha, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListaServiciosXEmpleado";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new CalendarioDTOResponse
                        {
                            IdServicio = DataUtility.ObjectToInt(result["IdServicio"]),
                            ClienteOpcional = DataUtility.ObjectToString(result["Cliente"]),
                            HoraInicio = DataUtility.ObjectToString(result["HoraInicio"]),
                            HoraFin = DataUtility.ObjectToString(result["HoraFin"]),
                            Descripcion = DataUtility.ObjectToString(result["Descripcion"]),
                            FechaInicio = DataUtility.ObjectToString(result["FechaInicio"]),
                            FechaFin = DataUtility.ObjectToString(result["FechaFin"]),
                            NombreDia = DataUtility.ObjectToString(result["NombreDia"]),
                            NumeroDia = DataUtility.ObjectToString(result["NumeroDia"])
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

        public async Task<List<CalendarioDTOResponse>> GetServicioXEmpleadoCalendario(CalendarioDTORequest calendarioDTORequest)
        {
            var listResponse = new List<CalendarioDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdEmpleado", calendarioDTORequest.IdEmpleado, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListaServiciosXEmpleadoCalendario";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new CalendarioDTOResponse
                        {
                            ClienteOpcional = DataUtility.ObjectToString(result["Cliente"]),
                            FechaInicio = DataUtility.ObjectToString(result["FechaInicio"]),
                            FechaFin = DataUtility.ObjectToString(result["FechaFin"])
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

        public async Task<List<CalendarioDTOResponse>> GetServicioXEmpleadoTotales(CalendarioDTORequest calendarioDTORequest)
        {
            var listResponse = new List<CalendarioDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdEmpleado", calendarioDTORequest.IdEmpleado, direction: ParameterDirection.Input);
                parameters.Add("@pFecha", calendarioDTORequest.Fecha, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListaServiciosXEmpleadoTotales";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new CalendarioDTOResponse
                        {
                            TotalHorasDia = DataUtility.ObjectToInt(result["TotalHorasDia"]),
                            TotalHoraSemana = DataUtility.ObjectToInt(result["TotalHorasSemana"]),
                            TotalHoraMes = DataUtility.ObjectToInt(result["TotalHorasMes"])
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

        public async Task<string> EliminarServicio(CalendarioDTORequest calendarioDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdServicio", calendarioDTORequest.IdServicio, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspEliminarProgramacionServicio";
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

        public async Task<CalendarioDTOResponse> GetCalendarioById(CalendarioDTORequest calendarioDTORequest)
        {
            CalendarioDTOResponse rolModel = new CalendarioDTOResponse();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdServicio", calendarioDTORequest.IdServicio, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspObtenerServicioPorId";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        rolModel = new CalendarioDTOResponse()
                        {
                            IdServicio = DataUtility.ObjectToInt(result["IdServicio"]),
                            Fecha = DataUtility.ObjectToDateTime(result["Fecha"]),
                            ClienteOpcional = DataUtility.ObjectToString(result["ClienteOpcional"]),
                            Descripcion = DataUtility.ObjectToString(result["Descripcion"]),
                            HoraInicio = DataUtility.ObjectToString(result["HoraInicio"]),
                            HoraFin = DataUtility.ObjectToString(result["HoraFin"]),
                            Pasaje = DataUtility.ObjectToDecimal(result["Pasaje"])
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
