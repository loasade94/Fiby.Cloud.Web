using Fiby.Cloud.Web.DTO.Modules.Pagos.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Interfaces.Pagos
{
    public interface IPagoClienteRepository
    {
        Task<string> ActualizarPagoClienteXServicio(PagoClienteDTORequest pagoClienteDTORequest);
    }
}
