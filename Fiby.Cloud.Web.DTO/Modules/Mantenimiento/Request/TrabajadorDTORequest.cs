using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Mantenimiento.Request
{
    public class TrabajadorDTORequest
    {
        public string CodigoUnico { get; set; }
        public int CodigoTrabajador { get; set; }
        public string IdEmpresa { get; set; }
        public string TipoTrabajador { get; set; }
        public string DescripcionTipoTrabajador { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string FechaRegistroText { get; set; }
        public string UsuarioRegistro { get; set; }
        public DateTime? FechaCambio { get; set; }
        public string FechaCambioText { get; set; }
        public string UsuarioCambio { get; set; }
        public int IdPuesto { get; set; }
        public string IdPuestoDescripcion { get; set; }
        public string NumeroDocumento { get; set; }
        public string EspecialidadMedica { get; set; }
        public string EspecialidadMedicaDescripcion { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public string SexoDescripcion { get; set; }
        public string SituacionRegistro { get; set; }

    }
}
