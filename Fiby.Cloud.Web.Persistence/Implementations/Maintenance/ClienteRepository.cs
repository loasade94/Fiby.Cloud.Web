using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Maintenance.Request;
using Fiby.Cloud.Web.DTO.Modules.Maintenance.Response;
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
    public class ClienteRepository : IClienteRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public ClienteRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<string> RegistrarCliente(ClienteDTORequest clienteDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdCliente", clienteDTORequest.IdCliente, direction: ParameterDirection.Input);
                parameters.Add("@pTipoDocumento", "", direction: ParameterDirection.Input);
                parameters.Add("@pNumeroDocumento", "", direction: ParameterDirection.Input);
                parameters.Add("@pNombre", clienteDTORequest.Nombres, direction: ParameterDirection.Input);
                parameters.Add("@pDireccion", clienteDTORequest.Direccion, direction: ParameterDirection.Input);
                parameters.Add("@pTelefono", clienteDTORequest.Telefono, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspInsCliente";
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

        public async Task<List<ClienteDTOResponse>> GetClienteAll()
        {
            var listResponse = new List<ClienteDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                //parameters.Add("@pIdEmpleado", calendarioDTORequest.IdEmpleado, direction: ParameterDirection.Input);
                //parameters.Add("@pFecha", calendarioDTORequest.Fecha, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListaClientes";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new ClienteDTOResponse
                        {
                            IdCliente = DataUtility.ObjectToInt(result["IdCliente"]),
                            Nombres = DataUtility.ObjectToString(result["Nombre"]),
                            Direccion = DataUtility.ObjectToString(result["Direccion"]),
                            Telefono = DataUtility.ObjectToString(result["Telefono"])
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

        public async Task<string> EliminarCliente(ClienteDTORequest clienteDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pIdCliente", clienteDTORequest.IdCliente, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspDelCliente";
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

        public async Task<ClienteDTOResponse> GetClientePorCodigo(ClienteDTORequest clienteDTORequest)
        {
            var listResponse = new ClienteDTOResponse();
            try
            {
                var parameters = new DynamicParameters();

                //parameters.Add("@pIdEmpleado", calendarioDTORequest.IdEmpleado, direction: ParameterDirection.Input);
                parameters.Add("@pIdCliente", clienteDTORequest.IdCliente, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListaClientesXCodigo";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse = new ClienteDTOResponse
                        {
                            IdCliente = DataUtility.ObjectToInt(result["IdCliente"]),
                            Nombres = DataUtility.ObjectToString(result["Nombre"]),
                            Direccion = DataUtility.ObjectToString(result["Direccion"]),
                            Telefono = DataUtility.ObjectToString(result["Telefono"])
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
