using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Implementations.Clinica;
using Fiby.Cloud.Web.Persistence.Implementations.Parametro;
using Fiby.Cloud.Web.Persistence.Implementations.Usuario;
using Fiby.Cloud.Web.Persistence.Interfaces.Clinica;
using Fiby.Cloud.Web.Persistence.Interfaces.Parametro;
using Fiby.Cloud.Web.Persistence.Interfaces.Usuario;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Implementations.Clinica;
using Fiby.Cloud.Web.Service.Implementations.Parametro;
using Fiby.Cloud.Web.Service.Implementations.Usuario;
using Fiby.Cloud.Web.Service.Interfaces.Clinica;
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

            services.AddScoped<ICitaService, CitaService>();
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IDoctorService, DoctorService>();

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

            services.AddSingleton<ICitaRepository, CitaRepository>();
            services.AddSingleton<IPacienteRepository, PacienteRepository>();
            services.AddSingleton<IDoctorRepository, DoctorRepository>();
            
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
