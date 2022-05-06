using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Facturacion.Response
{
    public class DetalleVentaDTOResponse
    {
        public int IdServicio { get; set; }
        public int iddetalle_venta { get; set; }
        public int idventa { get; set; }
        public int Id_producto { get; set; }
        public int cantidad { get; set; }
        public decimal preciounitario { get; set; }
        public string preciounitarioTexto { get; set; }
        public string Moneda { get; set; }
        public decimal Total_a_pagar { get; set; }
        public string Total_a_pagarTexto { get; set; }
        public string Unidad_de_medida { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public double Costo { get; set; }
        public double Ganancia { get; set; }
        public decimal mtoValorVentaItem { get; set; }
        public string mtoValorVentaItemTexto { get; set; }
        public decimal porIgvItem { get; set; }
        public string porIgvItemTexto { get; set; }
        public string CodigoProdSunat { get; set; }

        public decimal igv { get; set; }
    }
}
