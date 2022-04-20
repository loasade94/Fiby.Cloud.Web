using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Parametro.Request;
using Fiby.Cloud.Web.DTO.Modules.Parametro.Response;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces.Parametro;
using Fiby.Cloud.Web.Util.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Implementations.Parametro
{
    public class TablaDetalleRepository : ITablaDetalleRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public TablaDetalleRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<TablaDetalleDTOResponse>> GetTablaDetalleAll(TablaDetalleDTORequest TablaDetalleDTORequest)
        {
            var listResponse = new List<TablaDetalleDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@p_codigo_tabla", TablaDetalleDTORequest.CodigoTabla, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspPRMTablaDetalleLst";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new TablaDetalleDTOResponse
                        {
                            CodigoRegistro = DataUtility.ObjectToString(result["CodigoRegistro"]),
                            DescripcionRegistro = DataUtility.ObjectToString(result["DescripcionRegistro"]),
                            SituacionRegistro = DataUtility.ObjectToString(result["SituacionRegistro"]),
                            CodigoTabla = DataUtility.ObjectToString(result["CodigoTabla"]),
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

        public async Task<List<DepartamentoDTOResponse>> GetDepartamentoPorCodigo(DepartamentoDTORequest departamentoDTORequest)
        {
            var listResponse = new List<DepartamentoDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pCodigoDepartamento", departamentoDTORequest.CodigoDepartamento, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListarDepartamento";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new DepartamentoDTOResponse
                        {
                            CodigoDepartamento = DataUtility.ObjectToString(result["CodigoDepartamento"]),
                            DepartamentoDescripcion = DataUtility.ObjectToString(result["DepartamentoDescripcion"]),
                            SituacionRegistro = DataUtility.ObjectToString(result["Estado"])
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

        public async Task<List<ProvinciaDTOResponse>> GetProvinciaPorCodigo(ProvinciaDTORequest provinciaDTORequest)
        {
            var listResponse = new List<ProvinciaDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pCodigoDepartamento", provinciaDTORequest.CodigoDepartamento, direction: ParameterDirection.Input);
                parameters.Add("@pCodigoProvincia", provinciaDTORequest.CodigoProvincia, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListarProvincia";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new ProvinciaDTOResponse
                        {
                            CodigoDepartamento = DataUtility.ObjectToString(result["CodigoDepartamento"]),
                            CodigoProvincia = DataUtility.ObjectToString(result["CodigoProvincia"]),
                            ProvinciaDescripcion = DataUtility.ObjectToString(result["ProvinciaDescripcion"]),
                            SituacionRegistro = DataUtility.ObjectToString(result["Estado"])
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

        public async Task<List<DistritoDTOResponse>> GetDistritoPorCodigo(DistritoDTORequest distritoDTORequest)
        {
            var listResponse = new List<DistritoDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pCodigoDepartamento", distritoDTORequest.CodigoDepartamento, direction: ParameterDirection.Input);
                parameters.Add("@pCodigoProvincia", distritoDTORequest.CodigoProvincia, direction: ParameterDirection.Input);
                parameters.Add("@pCodigoDistrito", distritoDTORequest.CodigoDistrito, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspListarDistrito";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new DistritoDTOResponse
                        {
                            CodigoDepartamento = DataUtility.ObjectToString(result["CodigoDepartamento"]),
                            CodigoProvincia = DataUtility.ObjectToString(result["CodigoProvincia"]),
                            CodigoDistrito = DataUtility.ObjectToString(result["CodigoDistrito"]),
                            DistritoDescripcion = DataUtility.ObjectToString(result["DistritoDescripcion"]),
                            Ubigeo = DataUtility.ObjectToString(result["Ubigeo"]),
                            SituacionRegistro = DataUtility.ObjectToString(result["Estado"])
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

