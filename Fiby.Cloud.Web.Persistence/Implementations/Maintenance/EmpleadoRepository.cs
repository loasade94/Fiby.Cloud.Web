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
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public EmpleadoRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<EmpleadoDTOResponse>> GetEmpleadoAll()
        {
            var listResponse = new List<EmpleadoDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                //parameters.Add("@pCodigo", pLE14100DTORequest.CODIGO, direction: ParameterDirection.Input);
                //parameters.Add("@pMes", pLE14100DTORequest.MesLista, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListaEmpleados";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new EmpleadoDTOResponse
                        {
                            Codigo = DataUtility.ObjectToInt32(result["IdEmpleado"]),
                            Descripcion = DataUtility.ObjectToString(result["Descripcion"]),
                            Estado = DataUtility.ObjectToInt32(result["Estado"]),
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
        public async Task<List<EmpleadoDTOResponse>> GetEmpleadoApellido(EmpleadoDTORequest empleadoDTORequest)
        {
            var listResponse = new List<EmpleadoDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pApellidoPaterno", empleadoDTORequest.Apellido, direction: ParameterDirection.Input);
                //parameters.Add("@pMes", pLE14100DTORequest.MesLista, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "usplistaTrabajadores";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new EmpleadoDTOResponse
                        {
                            Codigo = DataUtility.ObjectToInt32(result["IdEmpleado"]),
                            Nombre = DataUtility.ObjectToString(result["Nombres"]),
                         ApellidoPaterno= DataUtility.ObjectToString(result["ApellidoPaterno"]),
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


    }
}
