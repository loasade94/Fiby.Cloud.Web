using Fiby.Cloud.Web.DTO.Modules.Pagos.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Interfaces.Pagos
{
    public interface IPagoClienteService
    {
        Task<string> ActualizarPagoClienteXServicio(PagoClienteDTORequest pagoClienteDTORequest);
    }
}
