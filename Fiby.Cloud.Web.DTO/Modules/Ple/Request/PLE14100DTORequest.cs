using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Ple.Request
{
    public class PLE14100DTORequest
    {
        public int CODIGO { get; set; }
        public int IdEmpresa { get; set; }
        public string PERIODO { get; set; }
        public string COD_UNICO_OPER_CUO { get; set; }
        public string LINEA_SEC_CUO { get; set; }
        public string FECHA_EMISION { get; set; }
        public string FECHA_VENCIMIENTO { get; set; }
        public string TIPO_DOC_COMPROBANTE { get; set; }
        public string SERIE { get; set; }
        public string NUMERO { get; set; }
        public string NUM_FINAL_BOLETAS { get; set; }
        public string TIPO_DOC_CLIENTE { get; set; }
        public string NUMERO_DOC_CLIENTE { get; set; }
        public string NOMBRE_CLIENTE { get; set; }
        public string EXPORTACION { get; set; }
        public string BASE_IMPONIBLE_GRAV { get; set; }
        public string DCTO_BASE_IMPONIBLE_GRAV { get; set; }
        public string IGV_GRAV { get; set; }
        public string DCTO_IGV_GRAV { get; set; }
        public string EXONERADA_OPER { get; set; }
        public string INAFECTA_OPER { get; set; }
        public string ISC { get; set; }
        public string BASE_IMPONIBLE_ARROZ { get; set; }
        public string IVAP_ARROZ { get; set; }
        public string ICPB { get; set; }
        public string OTROS_CONCEPTO { get; set; }
        public string IMPORTE_TOTAL { get; set; }
        public string CODIGO_MONEDA { get; set; }
        public string TIPO_CAMBIO { get; set; }
        public string FECHA_DOC_REF { get; set; }
        public string TIPO_DOC_REF { get; set; }
        public string SERIE_DOC_REF { get; set; }
        public string NUMERO_DOC_REF { get; set; }
        public string CONTRATO_EMPRESARIAL { get; set; }
        public string ERROR_TIPO_1 { get; set; }
        public string INDIC_COM_CAN_MED_PAGO { get; set; }
        public string ESTADO { get; set; }

    }
}
