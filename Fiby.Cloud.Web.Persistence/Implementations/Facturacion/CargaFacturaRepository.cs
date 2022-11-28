using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Facturacion.Request;
using Fiby.Cloud.Web.DTO.Modules.Facturacion.Response;
using Fiby.Cloud.Web.DTO.Modules.Ple.Request;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces.Facturacion;
using Fiby.Cloud.Web.Util.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Implementations.Facturacion
{
    public class CargaFacturaRepository : ICargaFacturaRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public CargaFacturaRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<string>> RegistrarFactura(CargaFacturaDTORequest cargaFacturaDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = new List<string>();
            try
            {

                parameters.Add("@pIdEmpresa", cargaFacturaDTORequest.IdEmpresa, direction: ParameterDirection.Input);
                parameters.Add("@pMes", cargaFacturaDTORequest.Mes, direction: ParameterDirection.Input);
                parameters.Add("@pAno", cargaFacturaDTORequest.Ano, direction: ParameterDirection.Input);
                parameters.Add("@pTipoDocumentoVenta", cargaFacturaDTORequest.TipoDocumentoVenta, direction: ParameterDirection.Input);
                parameters.Add("@pFechaEmision", cargaFacturaDTORequest.FechaEmision, direction: ParameterDirection.Input);
                parameters.Add("@pSerie", cargaFacturaDTORequest.Serie, direction: ParameterDirection.Input);
                parameters.Add("@pNumero", cargaFacturaDTORequest.Numero, direction: ParameterDirection.Input);
                parameters.Add("@pValorNeto", cargaFacturaDTORequest.ValorNeto, direction: ParameterDirection.Input);
                parameters.Add("@pIgv", cargaFacturaDTORequest.Igv, direction: ParameterDirection.Input);
                parameters.Add("@pOtrosCargos", cargaFacturaDTORequest.OtrosCargos, direction: ParameterDirection.Input);
                parameters.Add("@pValorTotal", cargaFacturaDTORequest.ValorTotal, direction: ParameterDirection.Input);
                parameters.Add("@pTipoDocumentoCliente", cargaFacturaDTORequest.TipoDocumentoCliente, direction: ParameterDirection.Input);
                parameters.Add("@pNumeroDocumentoCliente", cargaFacturaDTORequest.NumeroDocumentoCliente, direction: ParameterDirection.Input);
                parameters.Add("@pRazonSocialCliente", cargaFacturaDTORequest.RazonSocialCliente, direction: ParameterDirection.Input);

                parameters.Add("@pCodigoResultado", string.Empty, direction: ParameterDirection.Output);
                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "usp_InsertarFacturaEmpresa";
                var cn = _connectionFactory.GetConnection();
                var rpta = await cn.ExecuteReaderAsync(
                        sp,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                string cod = (parameters.Get<string>("pCodigoResultado") == null ?
                    string.Empty :
                    parameters.Get<string>("pCodigoResultado"));

                result.Add(cod);

                string err = (parameters.Get<string>("pMensajeResultado") == null ?
                    string.Empty :
                    parameters.Get<string>("pMensajeResultado"));

                result.Add(err);
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }

            return result;

        }

        public async Task<List<CargaFacturaDTOResponse>> ConsultaFacturas(CargaFacturaDTORequest cargaFacturaDTORequest)
        {
            var listResponse = new List<CargaFacturaDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pMes", cargaFacturaDTORequest.Mes, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListarFacturaEmpresa";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new CargaFacturaDTOResponse
                        {
                            IdFacturaEmpresa = DataUtility.ObjectToInt32(result["IdFacturaEmpresa"]),
                            IdEmpresa = DataUtility.ObjectToInt32(result["IdEmpresa"]),
                            Mes = DataUtility.ObjectToString(result["Mes"]),
                            Ano = DataUtility.ObjectToString(result["Ano"]),
                            TipoDocumentoVenta = DataUtility.ObjectToString(result["TipoDocumentoVenta"]),
                            FechaEmision = DataUtility.ObjectToString(result["FechaEmision"]),
                            Serie = DataUtility.ObjectToString(result["Serie"]),
                            Numero = DataUtility.ObjectToString(result["Numero"]),
                            ValorNeto = DataUtility.ObjectToString(result["ValorNeto"]),
                            Igv = DataUtility.ObjectToString(result["Igv"]),
                            OtrosCargos = DataUtility.ObjectToString(result["OtrosCargos"]),
                            ValorTotal = DataUtility.ObjectToString(result["ValorTotal"]),
                            TipoDocumentoCliente = DataUtility.ObjectToString(result["TipoDocumentoCliente"]),
                            NumeroDocumentoCliente = DataUtility.ObjectToString(result["NumeroDocumentoCliente"]),
                            RazonSocialCliente = DataUtility.ObjectToString(result["RazonSocialCliente"]),
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

        public async Task<List<string>> GetPle0801(CargaFacturaDTORequest request)
        {
            var listResponse = new List<string>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pMes", request.Mes, direction: ParameterDirection.Input);
                parameters.Add("@pAnio", request.Ano, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspGenerarDataPLE0801";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(DataUtility.ObjectToString(result["linea"]));
                    }
                }


            }
            catch (Exception ex)
            {
                listResponse = null;
            }
            return listResponse;
        }


        public async Task<string> EliminarFactura(CargaFacturaDTORequest request)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdFacturaEmpresa", request.IdFacturaEmpresa, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspDelFacturas0801";
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
