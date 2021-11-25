using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Implementations;
using Fiby.Cloud.Web.Persistence.Implementations.Horario;
using Fiby.Cloud.Web.Persistence.Implementations.Maintenance;
using Fiby.Cloud.Web.Persistence.Implementations.Pagos;
using Fiby.Cloud.Web.Persistence.Implementations.Reportes;
using Fiby.Cloud.Web.Persistence.Implementations.Sale;
using Fiby.Cloud.Web.Persistence.Interfaces;
using Fiby.Cloud.Web.Persistence.Interfaces.Horario;
using Fiby.Cloud.Web.Persistence.Interfaces.Maintenance;
using Fiby.Cloud.Web.Persistence.Interfaces.Pagos;
using Fiby.Cloud.Web.Persistence.Interfaces.Reportes;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Implementations;
using Fiby.Cloud.Web.Service.Implementations.Horario;
using Fiby.Cloud.Web.Service.Implementations.Maintenance;
using Fiby.Cloud.Web.Service.Implementations.Pagos;
using Fiby.Cloud.Web.Service.Implementations.Reportes;
using Fiby.Cloud.Web.Service.Interfaces;
using Fiby.Cloud.Web.Service.Interfaces.Horario;
using Fiby.Cloud.Web.Service.Interfaces.Maintenance;
using Fiby.Cloud.Web.Service.Interfaces.Pagos;
using Fiby.Cloud.Web.Service.Interfaces.Reportes;
using Fiby.Cloud.Web.Service.Modules.Data.Implementations;
using Fiby.Cloud.Web.Service.Modules.Data.Implementations.Sale;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces;
using Fiby.Cloud.Web.Service.Modules.Data.Interfaces.Sale;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

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
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPleService, PleService>();
            services.AddScoped<ISerieService, SerieService>();
            services.AddScoped<IEmpleadoService, EmpleadoService>();
            services.AddScoped<ICalendarioService, CalendarioService>();
            services.AddScoped<ISemanaService, SemanaService>();
            services.AddScoped<IPagoEmpleadoService, PagoEmpleadoService>();
            services.AddScoped<IPagoClienteService, PagoClienteService>();
            services.AddScoped<IReporteSemanaService, ReporteSemanaService>();
            services.AddScoped<IClienteService, ClienteService>();

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

            return services;
        }
        private static IServiceCollection AddRegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IPleRepository, PleRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ISerieRepository, SerieRepository>();
            services.AddSingleton<IEmpleadoRepository, EmpleadoRepository>();
            services.AddSingleton<ICalendarioRepository, CalendarioRepository>();
            services.AddSingleton<ISemanaRepository, SemanaRepository>();
            services.AddSingleton<IPagoEmpleadoRepository, PagoEmpleadoRepository>();
            services.AddSingleton<IPagoClienteRepository, PagoClienteRepository>();
            services.AddSingleton<IReporteSemanaRepository, ReporteSemanaRepository>();
            services.AddSingleton<IClienteRepository, ClienteRepository>();
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
