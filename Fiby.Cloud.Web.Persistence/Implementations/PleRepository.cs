using Dapper;
using Fiby.Cloud.Web.DTO.Modules.Ple.Request;
using Fiby.Cloud.Web.DTO.Modules.Ple.Response;
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

        public async Task<List<PleDTOResponse>> GetPleAll(PLE14100DTORequest pLE14100DTORequest)
        {
            var listResponse = new List<PleDTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pCodigo", pLE14100DTORequest.CODIGO, direction: ParameterDirection.Input);
                parameters.Add("@pMes", pLE14100DTORequest.MesLista, direction: ParameterDirection.Input);

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


        public async Task<List<PLE14100DTOResponse>> GetPlePLE14100All(PLE14100DTORequest pLE14100DTORequest)
        {
            var listResponse = new List<PLE14100DTOResponse>();
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@pIdEmpresa", pLE14100DTORequest.IdEmpresa, direction: ParameterDirection.Input);
                parameters.Add("@pMesLista", pLE14100DTORequest.MesLista, direction: ParameterDirection.Input);

                var cn = _connectionFactory.GetConnection();
                var sp = "uspPLE14100Lst";

                var result = await cn.ExecuteReaderAsync(
                             sp,
                             parameters,
                             commandTimeout: 0,
                             commandType: CommandType.StoredProcedure);

                if (result.FieldCount > 0)
                {
                    while (result.Read())
                    {
                        listResponse.Add(new PLE14100DTOResponse
                        {
                            codigo = DataUtility.ObjectToInt(result["CODIGO"]),
                            periodo = DataUtility.ObjectToString(result["PERIODO"]),
                            idempresa = DataUtility.ObjectToInt(result["IdEmpresa"]),
                            cod_unico_oper_cuo = DataUtility.ObjectToString(result["COD_UNICO_OPER_CUO"]),
                            linea_sec_cuo = DataUtility.ObjectToString(result["LINEA_SEC_CUO"]),
                            fecha_emision = DataUtility.ObjectToString(result["FECHA_EMISION"]),
                            fecha_vencimiento = DataUtility.ObjectToString(result["FECHA_VENCIMIENTO"]),
                            tipo_doc_comprobante = DataUtility.ObjectToString(result["TIPO_DOC_COMPROBANTE"]),
                            serie = DataUtility.ObjectToString(result["SERIE"]),
                            numero = DataUtility.ObjectToString(result["NUMERO"]),
                            num_final_boletas = DataUtility.ObjectToString(result["NUM_FINAL_BOLETAS"]),
                            tipo_doc_cliente = DataUtility.ObjectToString(result["TIPO_DOC_CLIENTE"]),
                            numero_doc_cliente = DataUtility.ObjectToString(result["NUMERO_DOC_CLIENTE"]),
                            nombre_cliente = DataUtility.ObjectToString(result["NOMBRE_CLIENTE"]),
                            exportacion = DataUtility.ObjectToString(result["EXPORTACION"]),
                            base_imponible_grav = DataUtility.ObjectToString(result["BASE_IMPONIBLE_GRAV"]),
                            dcto_base_imponible_grav = DataUtility.ObjectToString(result["DCTO_BASE_IMPONIBLE_GRAV"]),
                            igv_grav = DataUtility.ObjectToString(result["IGV_GRAV"]),
                            dcto_igv_grav = DataUtility.ObjectToString(result["DCTO_IGV_GRAV"]),
                            exonerada_oper = DataUtility.ObjectToString(result["EXONERADA_OPER"]),
                            inafecta_oper = DataUtility.ObjectToString(result["INAFECTA_OPER"]),
                            isc = DataUtility.ObjectToString(result["ISC"]),
                            base_imponible_arroz = DataUtility.ObjectToString(result["BASE_IMPONIBLE_ARROZ"]),
                            ivap_arroz = DataUtility.ObjectToString(result["IVAP_ARROZ"]),
                            icpb = DataUtility.ObjectToString(result["ICPB"]),
                            otros_concepto = DataUtility.ObjectToString(result["OTROS_CONCEPTO"]),
                            importe_total = DataUtility.ObjectToString(result["IMPORTE_TOTAL"]),
                            codigo_moneda = DataUtility.ObjectToString(result["CODIGO_MONEDA"]),
                            tipo_cambio = DataUtility.ObjectToString(result["TIPO_CAMBIO"]),
                            fecha_doc_ref = DataUtility.ObjectToString(result["FECHA_DOC_REF"]),
                            tipo_doc_ref = DataUtility.ObjectToString(result["TIPO_DOC_REF"]),
                            serie_doc_ref = DataUtility.ObjectToString(result["SERIE_DOC_REF"]),
                            numero_doc_ref = DataUtility.ObjectToString(result["NUMERO_DOC_REF"]),
                            contrato_empresarial = DataUtility.ObjectToString(result["CONTRATO_EMPRESARIAL"]),
                            error_tipo_1 = DataUtility.ObjectToString(result["ERROR_TIPO_1"]),
                            indic_com_can_med_pago = DataUtility.ObjectToString(result["INDIC_COM_CAN_MED_PAGO"]),
                            estado = DataUtility.ObjectToString(result["ESTADO"])
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
