using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Implementations.Gestion;
using Fiby.Cloud.Web.Persistence.Implementations.Mantenimiento;
using Fiby.Cloud.Web.Persistence.Implementations.Parametro;
using Fiby.Cloud.Web.Persistence.Implementations.Usuario;
using Fiby.Cloud.Web.Persistence.Interfaces.Gestion;
using Fiby.Cloud.Web.Persistence.Interfaces.Mantenimiento;
using Fiby.Cloud.Web.Persistence.Interfaces.Parametro;
using Fiby.Cloud.Web.Persistence.Interfaces.Usuario;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Implementations.Gestion;
using Fiby.Cloud.Web.Service.Implementations.Mantenimiento;
using Fiby.Cloud.Web.Service.Implementations.Parametro;
using Fiby.Cloud.Web.Service.Implementations.Usuario;
using Fiby.Cloud.Web.Service.Interfaces.Gestion;
using Fiby.Cloud.Web.Service.Interfaces.Mantenimiento;
using Fiby.Cloud.Web.Service.Interfaces.Parametro;
using Fiby.Cloud.Web.Service.Interfaces.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace Fiby.Cloud.Web.Client.Register
{
    public static class IoCRegister
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services)
        {
            AddRegisterConnection(services);
            AddRegisterServices(services);
            AddRegisterRepositories(services);
            AddRegisterComponents(services);

            return services;
        }
        private static IServiceCollection AddRegisterServices(this IServiceCollection services)
        {

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITablaDetalleService, TablaDetalleService>();

            #region Gestion
            services.AddScoped<ICitaService, CitaService>();
            #endregion

            #region Mantenimiento
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<ITrabajadorService, TrabajadorService>();
            services.AddScoped<IPuestoService, PuestoService>();
            #endregion

            return services;
        }
        private static IServiceCollection AddRegisterConnection(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            IConfiguration configurationOptimus;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configurationOptimus = serviceProvider.GetService<IConfiguration>();
            }

            services.AddTransient<IDbConnection>(db => new SqlConnection($"{configuration["BDFiby"]}"));
            services.AddTransient<IConnectionFactory, ConnectionFactory>();

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            return services;
        }
        private static IServiceCollection AddRegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ITablaDetalleRepository, TablaDetalleRepository>();

            #region Gestion
            services.AddSingleton<ICitaRepository, CitaRepository>();
            #endregion

            #region Mantenimiento
            services.AddSingleton<IPacienteRepository, PacienteRepository>();
            services.AddSingleton<IDoctorRepository, DoctorRepository>();
            services.AddSingleton<ITrabajadorRepository, TrabajadorRepository>();
            services.AddSingleton<IPuestoRepository, PuestoRepository>();
            #endregion


            return services;
        }

        private static IServiceCollection AddRegisterComponents(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IClaimValue, ClaimValue>();
            services.AddProxyHttp();
            services.AddAutoMapper(typeof(Startup));
            //services.AddMail();
            //services.AddExportEXCEL();
            //services.AddExportPDF();
            return services;
        }
    }
}
