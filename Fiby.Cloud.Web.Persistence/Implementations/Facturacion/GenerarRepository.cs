using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Facturacion.Request;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces.Facturacion;
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
    }
}
