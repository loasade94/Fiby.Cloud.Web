using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Horario.Request
{
    public class ServicioClienteDTORequest
    {
        public int IdSemana { get; set; }
        public int IdCliente { get; set; }
        public string Horas { get; set; }
        public int IdServicio { get; set; }
    }
}
