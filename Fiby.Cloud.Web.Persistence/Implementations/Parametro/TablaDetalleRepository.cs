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
    }
}
