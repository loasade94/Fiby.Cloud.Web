using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Request;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Response;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces.Reportes;
using Fiby.Cloud.Web.Util.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Implementations.Reportes
{
    public class ReporteSemanaRepository : IReporteSemanaRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public ReporteSemanaRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<ReporteSemanaDTOResponse>> GetReporteRentabilidadSemanal(ReporteSemanaDTORequest pagoEmpleadoDTORequest)
        {
            var listResponse = new List<ReporteSemanaDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdSemana", pagoEmpleadoDTORequest.IdSemana, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListaReporteGeneral";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new ReporteSemanaDTOResponse
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
                            MontoPagoCliente = DataUtility.ObjectToDecimal(result["MontoPagoCliente"]),
                            Rentabilidad = DataUtility.ObjectToDecimal(result["Rentabilidad"])
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

        public async Task<List<ReporteSemanaDTOResponse>> GetReporteRentabilidadSemanalEmpleado(ReporteSemanaDTORequest pagoEmpleadoDTORequest)
        {
            var listResponse = new List<ReporteSemanaDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdSemana", pagoEmpleadoDTORequest.IdSemana, direction: ParameterDirection.Input);
                parameters.Add("@pIdEmpleado", pagoEmpleadoDTORequest.IdEmpleado, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListaReporteGeneralXEmpleado";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new ReporteSemanaDTOResponse
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
                            MontoPagoCliente = DataUtility.ObjectToDecimal(result["MontoPagoCliente"]),
                            Rentabilidad = DataUtility.ObjectToDecimal(result["Rentabilidad"])
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

        public async Task<List<AnuncioDTOResponse>> GetAnunciosParaEmpleados()
        {
            var listResponse = new List<AnuncioDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                //parameters.Add("@pIdSemana", pagoEmpleadoDTORequest.IdSemana, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListaAnunciosPorFechas";

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
                            Titulo = DataUtility.ObjectToString(result["Titulo"]),
                            Detalle = DataUtility.ObjectToString(result["Detalle"]),
                            Mes = DataUtility.ObjectToString(result["Mes"]),
                            Dia = DataUtility.ObjectToString(result["Dia"]),
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
    }
}
