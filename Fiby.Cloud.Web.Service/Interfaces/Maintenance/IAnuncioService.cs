using Fiby.Cloud.Web.DTO.Modules.Reportes.Request;
using Fiby.Cloud.Web.DTO.Modules.Reportes.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Interfaces.Maintenance
{
    public interface IAnuncioService
    {
        Task<string> RegistrarAnuncio(AnuncioDTORequest gastoDTORequest);
        Task<List<AnuncioDTOResponse>> GetAnuncioAll(AnuncioDTORequest gastoDTORequest);
        Task<string> EliminarAnuncio(AnuncioDTORequest gastoDTORequest);
        Task<AnuncioDTOResponse> GetAnuncioPorCodigo(AnuncioDTORequest gastoDTORequest);
    }
}
