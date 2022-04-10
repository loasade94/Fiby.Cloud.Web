﻿using Fiby.Cloud.Web.DTO.Modules.Maintenance.Request;
using Fiby.Cloud.Web.DTO.Modules.Maintenance.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Service.Interfaces.Maintenance
{
    public interface IClienteService
    {
        Task<string> RegistrarCliente(ClienteDTORequest clienteDTORequest);
        Task<List<ClienteDTOResponse>> GetClienteAll();
        Task<string> EliminarCliente(ClienteDTORequest clienteDTORequest);
        Task<ClienteDTOResponse> GetClientePorCodigo(ClienteDTORequest clienteDTORequest);
    }
}