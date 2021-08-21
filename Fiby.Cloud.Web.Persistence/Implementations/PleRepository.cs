using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Ple.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces;
using Fiby.Cloud.Web.Util.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Implementations
{
    public class PleRepository : IPleRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public PleRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<PleDTOResponse>> GetPleAll(PleDTORequest pleDTORequest)
        {
            var listResponse = new List<PleDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pSecuenciaCUO", pleDTORequest.SequenceCUO, direction: ParameterDirection.Input);
                parameters.Add("@pTipoDocumento", pleDTORequest.TipoDocumento, direction: ParameterDirection.Input);
                parameters.Add("@pFechaEmision", pleDTORequest.FechaEmision, direction: ParameterDirection.Input);
                parameters.Add("@pSerie", pleDTORequest.Serie, direction: ParameterDirection.Input);
                parameters.Add("@pNumeroSerie", pleDTORequest.NumeroSerie, direction: ParameterDirection.Input);
                parameters.Add("@pValorNeto", pleDTORequest.ValorNeto, direction: ParameterDirection.Input);
                parameters.Add("@pValorIgv", pleDTORequest.ValorIgv, direction: ParameterDirection.Input);
                parameters.Add("@pValorTotal", pleDTORequest.ValorTotal, direction: ParameterDirection.Input);
                parameters.Add("@pTipoDocumentoCliente", pleDTORequest.TipoDocumentoCliente, direction: ParameterDirection.Input);
                parameters.Add("@pNumeroDocumentoCliente", pleDTORequest.NumeroDocumentoCliente, direction: ParameterDirection.Input);
                parameters.Add("@pNombresDocumentoCliente", pleDTORequest.NombresDocumentoCliente, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspGenerarDataPLE140";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new PleDTOResponse
                        {
                            Line = DataUtility.ObjectToString(result["linea"]),
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

        public async Task<string> RegistrarPle14100DPorMes(PLE14100DTORequest pLE14100DTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("@pPERIODO", pLE14100DTORequest.PERIODO, direction: ParameterDirection.Input);
                parameters.Add("@pIdEmpresa", pLE14100DTORequest.IdEmpresa, direction: ParameterDirection.Input);
                parameters.Add("@pCOD_UNICO_OPER_CUO", pLE14100DTORequest.COD_UNICO_OPER_CUO, direction: ParameterDirection.Input);
                parameters.Add("@pLINEA_SEC_CUO", pLE14100DTORequest.LINEA_SEC_CUO, direction: ParameterDirection.Input);
                parameters.Add("@pFECHA_EMISION", pLE14100DTORequest.FECHA_EMISION, direction: ParameterDirection.Input);
                parameters.Add("@pFECHA_VENCIMIENTO", pLE14100DTORequest.FECHA_VENCIMIENTO, direction: ParameterDirection.Input);
                parameters.Add("@pTIPO_DOC_COMPROBANTE", pLE14100DTORequest.TIPO_DOC_COMPROBANTE, direction: ParameterDirection.Input);
                parameters.Add("@pSERIE", pLE14100DTORequest.SERIE, direction: ParameterDirection.Input);
                parameters.Add("@pNUMERO", pLE14100DTORequest.NUMERO, direction: ParameterDirection.Input);
                parameters.Add("@pNUM_FINAL_BOLETAS", pLE14100DTORequest.NUM_FINAL_BOLETAS, direction: ParameterDirection.Input);
                parameters.Add("@pTIPO_DOC_CLIENTE", pLE14100DTORequest.TIPO_DOC_CLIENTE, direction: ParameterDirection.Input);
                parameters.Add("@pNUMERO_DOC_CLIENTE", pLE14100DTORequest.NUMERO_DOC_CLIENTE, direction: ParameterDirection.Input);
                parameters.Add("@pNOMBRE_CLIENTE", pLE14100DTORequest.NOMBRE_CLIENTE, direction: ParameterDirection.Input);
                parameters.Add("@pEXPORTACION", pLE14100DTORequest.EXPORTACION, direction: ParameterDirection.Input);
                parameters.Add("@pBASE_IMPONIBLE_GRAV", pLE14100DTORequest.BASE_IMPONIBLE_GRAV, direction: ParameterDirection.Input);
                parameters.Add("@pDCTO_BASE_IMPONIBLE_GRAV", pLE14100DTORequest.DCTO_BASE_IMPONIBLE_GRAV, direction: ParameterDirection.Input);
                parameters.Add("@pIGV_GRAV", pLE14100DTORequest.IGV_GRAV, direction: ParameterDirection.Input);
                parameters.Add("@pDCTO_IGV_GRAV", pLE14100DTORequest.DCTO_IGV_GRAV, direction: ParameterDirection.Input);
                parameters.Add("@pEXONERADA_OPER", pLE14100DTORequest.EXONERADA_OPER, direction: ParameterDirection.Input);
                parameters.Add("@pINAFECTA_OPER", pLE14100DTORequest.INAFECTA_OPER, direction: ParameterDirection.Input);
                parameters.Add("@pISC", pLE14100DTORequest.ISC, direction: ParameterDirection.Input);
                parameters.Add("@pBASE_IMPONIBLE_ARROZ", pLE14100DTORequest.BASE_IMPONIBLE_ARROZ, direction: ParameterDirection.Input);
                parameters.Add("@pIVAP_ARROZ", pLE14100DTORequest.IVAP_ARROZ, direction: ParameterDirection.Input);
                parameters.Add("@pICPB", pLE14100DTORequest.ICPB, direction: ParameterDirection.Input);
                parameters.Add("@pOTROS_CONCEPTO", pLE14100DTORequest.OTROS_CONCEPTO, direction: ParameterDirection.Input);
                parameters.Add("@pIMPORTE_TOTAL", pLE14100DTORequest.IMPORTE_TOTAL, direction: ParameterDirection.Input);
                parameters.Add("@pCODIGO_MONEDA", pLE14100DTORequest.CODIGO_MONEDA, direction: ParameterDirection.Input);
                parameters.Add("@pTIPO_CAMBIO", pLE14100DTORequest.TIPO_CAMBIO, direction: ParameterDirection.Input);
                parameters.Add("@pFECHA_DOC_REF", pLE14100DTORequest.FECHA_DOC_REF, direction: ParameterDirection.Input);
                parameters.Add("@pTIPO_DOC_REF", pLE14100DTORequest.TIPO_DOC_REF, direction: ParameterDirection.Input);

                parameters.Add("@pSERIE_DOC_REF", pLE14100DTORequest.SERIE_DOC_REF, direction: ParameterDirection.Input);
                parameters.Add("@pNUMERO_DOC_REF", pLE14100DTORequest.NUMERO_DOC_REF, direction: ParameterDirection.Input);
                parameters.Add("@pCONTRATO_EMPRESARIAL", pLE14100DTORequest.CONTRATO_EMPRESARIAL, direction: ParameterDirection.Input);
                parameters.Add("@pERROR_TIPO_1", pLE14100DTORequest.ERROR_TIPO_1, direction: ParameterDirection.Input);
                parameters.Add("@pINDIC_COM_CAN_MED_PAGO", pLE14100DTORequest.INDIC_COM_CAN_MED_PAGO, direction: ParameterDirection.Input);
                parameters.Add("@pESTADO", pLE14100DTORequest.ESTADO, direction: ParameterDirection.Input);

                parameters.Add("@pMensajeResultado", string.Empty, direction: ParameterDirection.Output);

                var sp = "uspPLE14100Ins";
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
