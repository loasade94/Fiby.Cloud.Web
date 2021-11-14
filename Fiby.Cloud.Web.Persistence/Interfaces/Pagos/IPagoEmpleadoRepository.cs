using Fiby.Cloud.Web.DTO.Modules.Pagos.Request;
using Fiby.Cloud.Web.DTO.Modules.Pagos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Interfaces.Pagos
{
    public interface IPagoEmpleadoRepository
    {
        Task<List<PagoEmpleadoDTOResponse>> GetPagosXEmpleadoSemana(PagoEmpleadoDTORequest pagoEmpleadoDTORequest);
        Task<string> RegistrarPagoEmpleado(PagoEmpleadoDTORequest pagoEmpleadoDTORequest);
        Task<List<PagoEmpleadoDTOResponse>> GetPagosXEmpleadoSemanaCab(PagoEmpleadoDTORequest pagoEmpleadoDTORequest);
    }
}
