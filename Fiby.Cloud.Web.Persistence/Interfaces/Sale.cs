using Fiby.Cloud.Web.DTO.Modules.Sale.Request.Serie;
using Fiby.Cloud.Web.DTO.Modules.Sale.Response.Serie;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Interfaces
{
    public interface ISerieRepository
    {
        Task<List<SerieDTOResponse>> GetSerieAll(SerieDTORequest rolDTORequest);
        Task<string> RegisterSerie(SerieDTORequest rolDTORequest);
        Task<string> DeleteSerie(SerieDTORequest rolDTORequest);
        Task<string> UpdateSerie(SerieDTORequest rolDTORequest);
        Task<SerieDTOResponse> GetSerieById(SerieDTORequest rolDTORequest);
    }
}
