using Fiby.Cloud.Web.DTO.Modules.Sale.Request.Serie;
using Fiby.Cloud.Web.DTO.Modules.Sale.Response.Serie;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Modules.Data.Interfaces.Sale
{
    public interface ISerieService
    {
        Task<List<SerieDTOResponse>> GetSerieAll(SerieDTORequest serieDTORequest);
        Task<string> RegisterSerie(SerieDTORequest serieDTORequest);
        Task<string> DeleteSerie(SerieDTORequest serieDTORequest);
        Task<string> UpdateSerie(SerieDTORequest serieDTORequest);
        Task<SerieDTOResponse> GetSerieById(SerieDTORequest serieDTORequest);
    }
}
