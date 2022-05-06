using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Facturacion.Request
{
    public class VentaDTORequest
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public int IdServicio { get; set; }
        public int IdEmpresa { get; set; }
        public string VersionUbl { get; set; }
        public string VersionEstDoc { get; set; }
        public string Serie { get; set; }
        public string Correlativo { get; set; }
        public string CodigoComprobante { get; set; }
        public int contadorProductos { get; set; }
        public string EmpresaRUCemisor { get; set; }
        public string EmpresaRazonsocialEmisora { get; set; }
        public string Ubigeo { get; set; }
        public string DptoempresaEmisora { get; set; }
        public string ProvempresaEmisora { get; set; }
        public string DistmpresaEmisora { get; set; }
        public string DireccionEmpresaEmisora { get; set; }
        public string CodigoTipoIdentificacion { get; set; }
        public string EmpresaRUCcliente { get; set; }
        public string DireccionCliente { get; set; }
        public string EmpresaRazonsocialCliente { get; set; }
        public string UbigeoCliente { get; set; }
        public string DptoempresaCliente { get; set; }
        public string ProvempresaCliente { get; set; }
        public string DistempresaCliente { get; set; }
        public decimal TotalIgv { get; set; }
        public decimal TotSubtotal { get; set; }
        public decimal Monto_total { get; set; }
        public string TotalIgvTexto { get; set; }
        public string TotSubtotalTexto { get; set; }
        public string Monto_totalTexto { get; set; }
        public double Porcentaje_IGV { get; set; }
        public string SituacionRegistro { get; set; }
        public string Estado { get; set; }

        public string Ticket { get; set; }
        public string Estadosunat { get; set; }
        public string Codigo { get; set; }

        public DateTime? FechaInicio { get; set; }
        public string FechaInicioTexto { get; set; }
        public DateTime? FechaFin { get; set; }
        public string FechaFinTexto { get; set; }
        public string Comprobante { get; set; }
        public DateTime? FechaEmision { get; set; }

        //----------------------------------------

        public string NumeroCorrelativo { get; set; }
        public string EmpresaRUCCliente { get; set; }
        
    }
}
