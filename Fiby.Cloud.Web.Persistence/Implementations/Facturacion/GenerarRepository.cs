using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Facturacion.Request;
using Fiby.Cloud.Web.DTO.Modules.Facturacion.Response;
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
    public class GenerarRepository : IGenerarRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public GenerarRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<string>> RegistrarVenta(VentaDTORequest ventaDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = new List<string>();
            try
            {

                parameters.Add("@pIdCliente", ventaDTORequest.IdCliente, direction: ParameterDirection.Input);
                //parameters.Add("@NumeroCorrelativo", ventaDTORequest.IdServicio, direction: ParameterDirection.Input);
                parameters.Add("@pIdEmpresa", ventaDTORequest.IdEmpresa, direction: ParameterDirection.Input);
                parameters.Add("@pCodigoComprobante", ventaDTORequest.CodigoComprobante, direction: ParameterDirection.Input);
                parameters.Add("@pContadorProductos", ventaDTORequest.contadorProductos, direction: ParameterDirection.Input);
                parameters.Add("@pCodigoTipoIdentificacion", ventaDTORequest.CodigoTipoIdentificacion, direction: ParameterDirection.Input);
                parameters.Add("@pDireccionCliente", ventaDTORequest.DireccionCliente, direction: ParameterDirection.Input);
                parameters.Add("@pEmpresaRUCcliente", ventaDTORequest.EmpresaRUCcliente, direction: ParameterDirection.Input);
                parameters.Add("@pEmpresaRazonsocialCliente", ventaDTORequest.EmpresaRazonsocialCliente, direction: ParameterDirection.Input);
                parameters.Add("@pDptoempresaCliente", ventaDTORequest.DptoempresaCliente, direction: ParameterDirection.Input);
                parameters.Add("@pProvempresaCliente", ventaDTORequest.ProvempresaCliente, direction: ParameterDirection.Input);
                parameters.Add("@pDistempresaCliente", ventaDTORequest.DistempresaCliente, direction: ParameterDirection.Input);
                parameters.Add("@pUbigeoCliente", ventaDTORequest.UbigeoCliente, direction: ParameterDirection.Input);
                parameters.Add("@pTotalIgv", ventaDTORequest.TotalIgv, direction: ParameterDirection.Input);
                parameters.Add("@pTotSubtotal", ventaDTORequest.TotSubtotal, direction: ParameterDirection.Input);
                parameters.Add("@pMonto_total", ventaDTORequest.Monto_total, direction: ParameterDirection.Input);
                parameters.Add("@pEstado", ventaDTORequest.Estado, direction: ParameterDirection.Input);

                parameters.Add("@pIdVenta", string.Empty, direction: ParameterDirection.Output);
                parameters.Add("@pCodigoResultado", string.Empty, direction: ParameterDirection.Output);
                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "usp_InsertarVenta";
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

                string id = (parameters.Get<string>("pIdVenta") == null ?
                    string.Empty :
                    parameters.Get<string>("pIdVenta"));

                result.Add(id);
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }

            return result;

        }

        public async Task<List<string>> RegistrarDetalleVenta(DetalleVentaDTORequest detalleVentaDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = new List<string>();
            try
            {

                parameters.Add("@pIdVenta", detalleVentaDTORequest.idventa, direction: ParameterDirection.Input);
                parameters.Add("@pIdServicio", detalleVentaDTORequest.IdServicio, direction: ParameterDirection.Input);
                parameters.Add("@pUnidad_de_medida", detalleVentaDTORequest.Unidad_de_medida, direction: ParameterDirection.Input);
                parameters.Add("@pcantidad", detalleVentaDTORequest.cantidad, direction: ParameterDirection.Input);
                parameters.Add("@pTotal_a_pagar", detalleVentaDTORequest.Total_a_pagar, direction: ParameterDirection.Input);
                parameters.Add("@ppreciounitario", detalleVentaDTORequest.preciounitario, direction: ParameterDirection.Input);
                parameters.Add("@pmtoValorVentaItem", detalleVentaDTORequest.mtoValorVentaItem, direction: ParameterDirection.Input);
                parameters.Add("@pigv", detalleVentaDTORequest.porIgvItem, direction: ParameterDirection.Input);
                parameters.Add("@pDescripcion", detalleVentaDTORequest.Descripcion, direction: ParameterDirection.Input);
                
                parameters.Add("@pCodigoResultado", string.Empty, direction: ParameterDirection.Output);
                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "usp_InsertarDetalleVenta";
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

        public async Task<string> ActualizarEstadoVenta(VentaDTORequest ventaDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;

            try
            {

                parameters.Add("@pIdVenta", ventaDTORequest.IdVenta, direction: ParameterDirection.Input);
                parameters.Add("@pEstado", ventaDTORequest.Estado, direction: ParameterDirection.Input);
                
                var sp = "usp_InsertarVenta";
                var cn = _connectionFactory.GetConnection();
                var rpta = await cn.ExecuteReaderAsync(
                        sp,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;

        }

        public async Task<List<string>> RegistrarBaja(VentaDTORequest ventaDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = new List<string>();
            try
            {

                parameters.Add("@pIdventa", ventaDTORequest.IdVenta, direction: ParameterDirection.Input);
                parameters.Add("@pTicket", ventaDTORequest.Ticket, direction: ParameterDirection.Input);
                parameters.Add("@pEstadosunat", ventaDTORequest.Estadosunat, direction: ParameterDirection.Input);
                parameters.Add("@pCodigo", ventaDTORequest.Codigo, direction: ParameterDirection.Input);
                
                parameters.Add("@pCodigoResultado", string.Empty, direction: ParameterDirection.Output);
                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "InsertarCombaja";
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

        public async Task<List<VentaDTOResponse>> ListarDocumentosGenerados(VentaDTORequest ventaDTORequest)
        {
            var listResponse = new List<VentaDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pFechaInicio", ventaDTORequest.FechaInicio, direction: ParameterDirection.Input);
                parameters.Add("@pFechaFin", ventaDTORequest.FechaFin, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListaDocumentosGenerados";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new VentaDTOResponse
                        {
                            IdVenta = DataUtility.ObjectToInt(result["IdVenta"]),
                            Correlativo = DataUtility.ObjectToString(result["Comprobante"]),
                            EmpresaRazonsocialCliente = DataUtility.ObjectToString(result["Cliente"]),
                            EmpresaRUCcliente = DataUtility.ObjectToString(result["DocumentoCliente"]),
                            CodigoComprobante = DataUtility.ObjectToString(result["Tipo"]),
                            FechaEmision = DataUtility.ObjectToDateTime(result["FechaEmision"])
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

        public async Task<VentaDTOResponse> ListarVentaPorId(VentaDTORequest ventaDTORequest)
        {
            var listResponse = new VentaDTOResponse();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdVenta", ventaDTORequest.IdVenta, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "usp_ListarVentaPorId";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse = new VentaDTOResponse
                        {
                            VersionUbl = DataUtility.ObjectToString(result["VersionUbl"].ToString()),
                            VersionEstDoc = DataUtility.ObjectToString(result["VersionEstDoc"].ToString()),
                            NumeroCorrelativo = DataUtility.ObjectToString(result["NumeroCorrelativo"].ToString()),
                            Correlativo = DataUtility.ObjectToString(result["Correlativo"].ToString()),
                            Serie = DataUtility.ObjectToString(result["Serie"].ToString()),
                            CodigoComprobante = DataUtility.ObjectToString(result["CodigoComprobante"].ToString()),
                            contadorProductos = DataUtility.ObjectToInt32(result["contadorProductos"].ToString()),
                            EmpresaRUCemisor = DataUtility.ObjectToString(result["EmpresaRUCemisor"].ToString()),
                            EmpresaRazonsocialEmisora = DataUtility.ObjectToString(result["EmpresaRazonsocialEmisora"].ToString()),
                            Ubigeo = DataUtility.ObjectToString(result["Ubigeo"].ToString()),
                            DptoempresaEmisora = DataUtility.ObjectToString(result["DptoempresaEmisora"].ToString()),
                            ProvempresaEmisora = DataUtility.ObjectToString(result["ProvempresaEmisora"].ToString()),
                            DistmpresaEmisora = DataUtility.ObjectToString(result["DistmpresaEmisora"].ToString()),
                            DireccionEmpresaEmisora = DataUtility.ObjectToString(result["DireccionEmpresaEmisora"].ToString()),
                            CodigoTipoIdentificacion = DataUtility.ObjectToString(result["CodigoTipoIdentificacion"].ToString()),
                            DireccionCliente = DataUtility.ObjectToString(result["DireccionCliente"].ToString()),
                            EmpresaRUCCliente = DataUtility.ObjectToString(result["EmpresaRUCcliente"].ToString()),
                            EmpresaRazonsocialCliente = DataUtility.ObjectToString(result["EmpresaRazonsocialCliente"].ToString()),
                            DptoempresaCliente = DataUtility.ObjectToString(result["DptoempresaCliente"].ToString()),
                            ProvempresaCliente = DataUtility.ObjectToString(result["ProvempresaCliente"].ToString()),
                            DistempresaCliente = DataUtility.ObjectToString(result["DistempresaCliente"].ToString()),
                            UbigeoCliente = DataUtility.ObjectToString(result["UbigeoCliente"].ToString()),
                            TotalIgv = DataUtility.ObjectToDecimal(result["TotalIgv"].ToString()),
                            TotSubtotal = DataUtility.ObjectToDecimal(result["TotSubtotal"].ToString()),
                            Monto_total = DataUtility.ObjectToDecimal(result["Monto_total"].ToString()),
                            FechaEmision = DataUtility.ObjectToDateTime(result["FechaEmision"]),
                            Porcentaje_IGV = 18,
                            SituacionRegistro = DataUtility.ObjectToString(result["SituacionRegistro"].ToString()),
                            Estado = DataUtility.ObjectToString(result["Estado"].ToString()),
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

        public async Task<List<DetalleVentaDTOResponse>> ListarDetallePorId(VentaDTORequest ventaDTORequest)
        {
            var listResponse = new List<DetalleVentaDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdVenta", ventaDTORequest.IdVenta, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "usp_ListarVentaDetallePorId";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new DetalleVentaDTOResponse
                        {
                            Unidad_de_medida = DataUtility.ObjectToString(result["Unidad_de_medida"].ToString()),
                            cantidad = DataUtility.ObjectToInt32(result["cantidad"].ToString()),
                            Total_a_pagar = DataUtility.ObjectToDecimal(result["Total_a_pagar"].ToString()),
                            preciounitario = DataUtility.ObjectToDecimal(result["preciounitario"].ToString()),
                            mtoValorVentaItem = DataUtility.ObjectToDecimal(result["mtoValorVentaItem"].ToString()),
                            igv = DataUtility.ObjectToDecimal(result["igv"].ToString()),
                            Descripcion = DataUtility.ObjectToString(result["Descripcion"].ToString()),
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
