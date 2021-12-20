using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Inversiones.Response
{
    public class GastoDTOResponse
    {
        public int IdGasto { get; set; }
        public int IdSemana { get; set; }
        public string NombreSemana { get; set; }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
    }
}
