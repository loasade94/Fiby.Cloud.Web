using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Ple.Response
{
    public class PLE14100DTOResponse
    {
        public int codigo { get; set; }
        public int idempresa { get; set; }
        public string periodo { get; set; }
        public string cod_unico_oper_cuo { get; set; }
        public string linea_sec_cuo { get; set; }
        public string fecha_emision { get; set; }
        public string fecha_vencimiento { get; set; }
        public string tipo_doc_comprobante { get; set; }
        public string serie { get; set; }
        public string numero { get; set; }
        public string num_final_boletas { get; set; }
        public string tipo_doc_cliente { get; set; }
        public string numero_doc_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public string exportacion { get; set; }
        public string base_imponible_grav { get; set; }
        public string dcto_base_imponible_grav { get; set; }
        public string igv_grav { get; set; }
        public string dcto_igv_grav { get; set; }
        public string exonerada_oper { get; set; }
        public string inafecta_oper { get; set; }
        public string isc { get; set; }
        public string base_imponible_arroz { get; set; }
        public string ivap_arroz { get; set; }
        public string icpb { get; set; }
        public string otros_concepto { get; set; }
        public string importe_total { get; set; }
        public string codigo_moneda { get; set; }
        public string tipo_cambio { get; set; }
        public string fecha_doc_ref { get; set; }
        public string tipo_doc_ref { get; set; }
        public string serie_doc_ref { get; set; }
        public string numero_doc_ref { get; set; }
        public string contrato_empresarial { get; set; }
        public string error_tipo_1 { get; set; }
        public string indic_com_can_med_pago { get; set; }
        public string estado { get; set; }
    }
}
