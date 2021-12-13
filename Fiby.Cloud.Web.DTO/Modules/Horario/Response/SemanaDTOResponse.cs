using System;
using System.Collections.Generic;
using System.Text;

namespace Fiby.Cloud.Web.DTO.Modules.Horario.Response
{
    public class SemanaDTOResponse
    {
        public int IdSemana { get; set; }
        public string Horario { get; set; }
        public string Lunes { get; set; }
        public string Martes { get; set; }
        public string Miercoles { get; set; }
        public string Jueves { get; set; }
        public string Viernes { get; set; }
        public string Sabado { get; set; }
        public string Domingo { get; set; }
        public string NombreSemana { get; set; }
        public string Dia { get; set; }
        public DateTime? Fecha { get; set; }
        public int Prioridad { get; set; }
        public decimal Monto { get; set; }
        public List<SemanaDTOResponse> ListaNombres { get; set; }
        public List<SemanaDTOResponse> ListaMontos { get; set; }
        public List<SemanaDTOResponse> ListaMontos1 { get; set; }
        public List<SemanaDTOResponse> ListaMontos2 { get; set; }

    }
}
