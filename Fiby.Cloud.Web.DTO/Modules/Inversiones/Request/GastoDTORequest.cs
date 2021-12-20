using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Inversiones.Request
{
    public class GastoDTORequest
    {
        public int IdGasto { get; set; }
        public int IdSemana { get; set; }
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public string MontoText { get; set; }
    }
}
