using Fiby.Cloud.Web.DTO.Modules.Pagos.Request;
using Fiby.Cloud.Web.DTO.Modules.Pagos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Interfaces.Pagos
{
    public interface IPagoEmpleadoService
    {
        Task<List<PagoEmpleadoDTOResponse>> GetPagosXEmpleadoSemana(PagoEmpleadoDTORequest pagoEmpleadoDTORequest);
        Task<string> RegistrarPagoEmpleado(PagoEmpleadoDTORequest pagoEmpleadoDTORequest);
        Task<List<PagoEmpleadoDTOResponse>> GetPagosXEmpleadoSemanaCab(PagoEmpleadoDTORequest pagoEmpleadoDTORequest);
        Task<string> ActualizarPasajeXServicio(PagoEmpleadoDTORequest pagoEmpleadoDTORequest);
    }
}
