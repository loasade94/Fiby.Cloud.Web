﻿using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Horario.Request;
using Fiby.Cloud.Web.DTO.Modules.Horario.Response;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces.Horario;
using Fiby.Cloud.Web.Util.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Implementations.Horario
{
    public class SemanaRepository : ISemanaRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public SemanaRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<SemanaDTOResponse>> GetDisponibilidadSemana(SemanaDTORequest semanaDTORequest)
        {
            var listResponse = new List<SemanaDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdSemana", semanaDTORequest.IdSemana, direction: ParameterDirection.Input);
                
                var cn = _connectionFactory.GetConnection();
                var sp = "uspListarDisponibilidadEmpleados";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new SemanaDTOResponse
                        {
                            IdSemana = DataUtility.ObjectToInt(result["IdSemana"]),
                            Horario = DataUtility.ObjectToString(result["Horario"]),
                            Lunes = DataUtility.ObjectToString(result["Lunes"]),
                            Martes = DataUtility.ObjectToString(result["Martes"]),
                            Miercoles = DataUtility.ObjectToString(result["Miercoles"]),
                            Jueves = DataUtility.ObjectToString(result["Jueves"]),
                            Viernes = DataUtility.ObjectToString(result["Viernes"]),
                            Sabado = DataUtility.ObjectToString(result["Sabado"]),
                            Domingo = DataUtility.ObjectToString(result["Domingo"])
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
