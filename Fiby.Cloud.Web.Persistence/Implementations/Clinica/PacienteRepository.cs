using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Clinica.Request;
using Fiby.Cloud.Web.DTO.Modules.Clinica.Response;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces.Clinica;
using Fiby.Cloud.Web.Util.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Implementations.Clinica
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public PacienteRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<PacienteDTOResponse> GetPacientePorDocumento(PacienteDTORequest PacienteDTORequest)
        {
            var listResponse = new PacienteDTOResponse();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@DocumentoPaciente", PacienteDTORequest.DocumentoPaciente, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspBuscarPacientePorDocumentoLst";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse = new PacienteDTOResponse()
                        {
                            NombrePaciente = DataUtility.ObjectToString(result["NombrePaciente"])
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
