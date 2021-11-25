using Dapper;
using Fiby.Cloud.Web.DTO.Modules.User.Request;
using Fiby.Cloud.Web.DTO.Modules.User.Response;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Interfaces;
using Fiby.Cloud.Web.Util.Utility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Persistence.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IConnectionFactory _connectionFactory;

        public UserRepository(IConfiguration configuration, IConnectionFactory connectionFactory)
        {
            _configuration = configuration;
            _connectionFactory = connectionFactory;
        }

        public async Task<string> LoginUser(UserDTORequest userDTORequest)
        {
            var parameters = new DynamicParameters();
            var result = string.Empty;
            try
            {

                parameters.Add("Usuario", userDTORequest.NameUser, direction: ParameterDirection.Input);
                parameters.Add("Clave", userDTORequest.Password, direction: ParameterDirection.Input);

                parameters.Add("IdUsuario", string.Empty, direction: ParameterDirection.Output);

                var sp = "usp_LoginUsuario";
                var cn = _connectionFactory.GetConnection();
                var rpta = await cn.ExecuteReaderAsync(
                        sp,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                string err = (parameters.Get<string>("IdUsuario") == null ?
                    string.Empty :
                    parameters.Get<string>("IdUsuario"));

                result = err;
            }
            catch (Exception ex)
            {

                result = ex.Message;
            }

            return result;

        }

        public async Task<UserDTOResponse> GetUserLogin(UserDTORequest userDTORequest)
        {
            UserDTOResponse authModel = new UserDTOResponse();
            var parameters = new DynamicParameters();

            parameters.Add("@pIdUsuario", userDTORequest.UserId, direction: ParameterDirection.Input);
            var sp = "usp_ObtenerDetalleUsuarioLista";
            var cn = _connectionFactory.GetConnection();
            var result = await cn.ExecuteReaderAsync(
                   sp,
                   parameters,
                   commandTimeout: 0,
                   commandType: CommandType.StoredProcedure
               );

            if (result.FieldCount > 0)
            {

                #region DATOS DE USUARIO

                //DATOS DE USUARIO
                UserDTOResponse userModel = new UserDTOResponse();
                while (result.Read())
                {
                    userModel = new UserDTOResponse()
                    {
                        UserId = DataUtility.ObjectToInt(result["IdUsuario"]),
                        Names = DataUtility.ObjectToString(result["Nombres"]),
                        LastName = DataUtility.ObjectToString(result["Apellidos"]),
                        Email = DataUtility.ObjectToString(result["Correo"]),
                        NameUser = DataUtility.ObjectToString(result["Usuario"]),
                        Password = DataUtility.ObjectToString(result["Clave"]),
                        StoreId = DataUtility.ObjectToInt(result["IdTienda"]),
                        RolId = DataUtility.ObjectToInt(result["IdRol"]),
                        Active = DataUtility.ObjectToBool(result["Activo"]),
                        DateRegister = DataUtility.ObjectToDateTime(result["FechaRegistro"]),
                        CompanyId = DataUtility.ObjectToInt(result["IdEmpresa"])
                    };
                }
                authModel.oUser = userModel;

                result.NextResult();

                #endregion

                #region DATOS DE TIENDA
                //DATOS DE TIENDA

                StoreDTOResponse storeModel = new StoreDTOResponse();
                while (result.Read())
                {
                    storeModel = new StoreDTOResponse()
                    {
                        StoreId = DataUtility.ObjectToInt(result["IdTienda"]),
                        Name = DataUtility.ObjectToString(result["Nombre"]),
                        RUC = DataUtility.ObjectToString(result["RUC"]),
                        Direction = DataUtility.ObjectToString(result["Direccion"]),
                        Phone = DataUtility.ObjectToString(result["Telefono"]),
                        Active = DataUtility.ObjectToBool(result["Activo"]),
                        DateRegister = DataUtility.ObjectToDateTime(result["FechaRegistro"])
                    };
                }
                authModel.oStore = storeModel;

                result.NextResult();
                #endregion

                #region DATOS DE ROL

                //DATOS DE ROL

                RolDTOResponse rolModel = new RolDTOResponse();
                while (result.Read())
                {
                    rolModel = new RolDTOResponse()
                    {
                        RolId = DataUtility.ObjectToInt(result["IdRol"]),
                        Description = DataUtility.ObjectToString(result["Descripcion"]),
                        Active = DataUtility.ObjectToBool(result["Activo"]),
                        DateRegister = DataUtility.ObjectToDateTime(result["FechaRegistro"])
                    };
                }
                authModel.oRol = rolModel;

                result.NextResult();

                #endregion

                #region DATOS DE MENU

                // DATOS DE MENU
                var listMenuModel = new List<MenuDTOResponse>();

                while (result.Read())
                {
                    listMenuModel.Add(new MenuDTOResponse
                    {
                        MenuId = DataUtility.ObjectToInt(result["IdMenu"]),
                        Name = DataUtility.ObjectToString(result["Nombre"]),
                        Icon = DataUtility.ObjectToString(result["Icono"]),
                        Active = DataUtility.ObjectToBool(result["Activo"]),
                        DateRegister = DataUtility.ObjectToDateTime(result["FechaRegistro"])
                    });
                }
                authModel.oListMenu = listMenuModel;
                result.NextResult();

                #endregion

                #region DATOS DE SUBMENU

                //DATOS DE SUBMENU
                var listSubMenuModel = new List<SubMenuDTOResponse>();
                while (result.Read())
                {
                    listSubMenuModel.Add(new SubMenuDTOResponse
                    {
                        IdSubMenu = DataUtility.ObjectToInt(result["IdSubMenu"]),
                        IdMenu = DataUtility.ObjectToInt(result["IdMenu"]),
                        Name = DataUtility.ObjectToString(result["Nombre"]),
                        Controlator = DataUtility.ObjectToString(result["Controlador"]),
                        Action = DataUtility.ObjectToString(result["Accion"]),
                        NumberOrder = DataUtility.ObjectToInt(result["NumeroOrden"]),
                        NameForm = DataUtility.ObjectToString(result["NombreFormulario"]),
                        Active = DataUtility.ObjectToBool(result["Activo"]),
                        DateRegister = DataUtility.ObjectToDateTime(result["FechaRegistro"]),
                        ImageIco = DataUtility.ObjectToString(result["ImageIco"]),
                        Area = DataUtility.ObjectToString(result["Area"]),
                    });
                }
                authModel.oListSubMenu = listSubMenuModel;
                result.NextResult();

                #endregion

                #region DATOS DE EMPRESA

                //DATOS DE EMPRESA

                CompanyDTOResponse companyModel = new CompanyDTOResponse();
                while (result.Read())
                {
                    companyModel = new CompanyDTOResponse()
                    {
                        CompanyId = DataUtility.ObjectToInt(result["IdEmpresa"]),
                        NameCompany = DataUtility.ObjectToString(result["NombreEmpresa"]),
                        Active = DataUtility.ObjectToBool(result["Activo"]),
                        DateRegister = DataUtility.ObjectToDateTime(result["FechaRegistro"]),
                        VersionUbl = DataUtility.ObjectToString(result["VersionUbl"]),
                        VersionEstDoc = DataUtility.ObjectToString(result["VersionEstDoc"]),
                        RUC = DataUtility.ObjectToString(result["RUC"]),
                    };
                }
                authModel.oCompany = companyModel;

                result.NextResult();

                #endregion

                result.Close();
            }


            return authModel;
        }
    }
}
