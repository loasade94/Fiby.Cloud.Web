using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Pagos.Request;
using Fiby.Cloud.Web.DTO.Modules.Pagos.Response;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces.Pagos;
using Fiby.Cloud.Web.Util.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Implementations.Pagos
{
    public class PagoEmpleadoRepository : IPagoEmpleadoRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public PagoEmpleadoRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<PagoEmpleadoDTOResponse>> GetPagosXEmpleadoSemana(PagoEmpleadoDTORequest pagoEmpleadoDTORequest)
        {
            var listResponse = new List<PagoEmpleadoDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdEmpleado", pagoEmpleadoDTORequest.IdEmpleado, direction: ParameterDirection.Input);
                parameters.Add("@pIdSemana", pagoEmpleadoDTORequest.IdSemana, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListaServiciosPagos";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new PagoEmpleadoDTOResponse
                        {
                            IdServicio = DataUtility.ObjectToInt(result["IdServicio"]),
                            NombreDia = DataUtility.ObjectToString(result["NombreDia"]),
                            NumeroDia = DataUtility.ObjectToString(result["NumeroDia"]),
                            Cliente = DataUtility.ObjectToString(result["Cliente"]),
                            HoraInicio = DataUtility.ObjectToString(result["HoraInicio"]),
                            HoraFin = DataUtility.ObjectToString(result["HoraFin"]),
                            Horas = DataUtility.ObjectToInt(result["Horas"]),
                            Pago = DataUtility.ObjectToDecimal(result["Pago"]),
                            SubTotal = DataUtility.ObjectToDecimal(result["SubTotal"]),
                            Pasaje = DataUtility.ObjectToDecimal(result["Pasaje"]),
                            MontoPagoCliente = DataUtility.ObjectToDecimal(result["MontoPagoCliente"])
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

        public async Task<string> RegistrarPagoEmpleado(PagoEmpleadoDTORequest pagoEmpleadoDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdPagoEmpleado", pagoEmpleadoDTORequest.IdPagoEmpleado, direction: ParameterDirection.Input);
                parameters.Add("@pIdEmpleado", pagoEmpleadoDTORequest.IdEmpleado, direction: ParameterDirection.Input);
                parameters.Add("@pIdSemana", pagoEmpleadoDTORequest.IdSemana, direction: ParameterDirection.Input);
                parameters.Add("@pEstado", pagoEmpleadoDTORequest.Estado, direction: ParameterDirection.Input);
                
                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspInsPagoEmpleado";
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

        public async Task<List<PagoEmpleadoDTOResponse>> GetPagosXEmpleadoSemanaCab(PagoEmpleadoDTORequest pagoEmpleadoDTORequest)
        {
            var listResponse = new List<PagoEmpleadoDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdEmpleado", pagoEmpleadoDTORequest.IdEmpleado, direction: ParameterDirection.Input);
                parameters.Add("@pIdSemana", pagoEmpleadoDTORequest.IdSemana, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListarPagoEmpleadoCab";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new PagoEmpleadoDTOResponse
                        {
                            IdPagoEmpleado = DataUtility.ObjectToInt(result["IdPagoEmpleado"]),
                            Horas = DataUtility.ObjectToInt(result["HorasTotal"]),
                            Pago = DataUtility.ObjectToDecimal(result["MontoTotal"]),
                            Fecha = DataUtility.ObjectToDateTime(result["FechaRegistro"]),
                            DescripcionEstado = DataUtility.ObjectToString(result["DescripcionEstado"]),
                            Nombres = DataUtility.ObjectToString(result["Nombres"]),
                            ApellidoPaterno = DataUtility.ObjectToString(result["ApellidoPaterno"]),
                            ApellidoMaterno = DataUtility.ObjectToString(result["ApellidoMaterno"])
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

        public async Task<string> ActualizarPasajeXServicio(PagoEmpleadoDTORequest pagoEmpleadoDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdServicio", pagoEmpleadoDTORequest.IdServicio, direction: ParameterDirection.Input);
                parameters.Add("@pPasaje", pagoEmpleadoDTORequest.Pasaje, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspUdpPasajeXServicio";
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

        public async Task<string> AnularPagoEmpleado(PagoEmpleadoDTORequest gastoDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pId", gastoDTORequest.IdPagoEmpleado, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspDelPagoEmpleado";
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
