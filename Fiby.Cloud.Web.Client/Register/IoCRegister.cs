using Fiby.Cloud.Web.Client.Extensions;
using Fiby.Cloud.Web.Persistence.Connection;
using Fiby.Cloud.Web.Persistence.Implementations;
using Fiby.Cloud.Web.Persistence.Implementations.Facturacion;
using Fiby.Cloud.Web.Persistence.Implementations.Horario;
using Fiby.Cloud.Web.Persistence.Implementations.Inversiones;
using Fiby.Cloud.Web.Persistence.Implementations.Maintenance;
using Fiby.Cloud.Web.Persistence.Implementations.Pagos;
using Fiby.Cloud.Web.Persistence.Implementations.Parametro;
using Fiby.Cloud.Web.Persistence.Implementations.Reportes;
using Fiby.Cloud.Web.Persistence.Implementations.Sale;
using Fiby.Cloud.Web.Persistence.Interfaces;
using Fiby.Cloud.Web.Persistence.Interfaces.Facturacion;
using Fiby.Cloud.Web.Persistence.Interfaces.Horario;
using Fiby.Cloud.Web.Persistence.Interfaces.Inversiones;
using Fiby.Cloud.Web.Persistence.Interfaces.Maintenance;
using Fiby.Cloud.Web.Persistence.Interfaces.Pagos;
using Fiby.Cloud.Web.Persistence.Interfaces.Parametro;
using Fiby.Cloud.Web.Persistence.Interfaces.Reportes;
using Fiby.Cloud.Web.Proxy.Src;
using Fiby.Cloud.Web.Service.Implementations;
using Fiby.Cloud.Web.Service.Implementations.Facturacion;
using Fiby.Cloud.Web.Service.Implementations.Horario;
using Fiby.Cloud.Web.Service.Implementations.Inversiones;
using Fiby.Cloud.Web.Service.Implementations.Maintenance;
using Fiby.Cloud.Web.Service.Implementations.Pagos;
using Fiby.Cloud.Web.Service.Implementations.Parametro;
using Fiby.Cloud.Web.Service.Implementations.Reportes;
using Fiby.Cloud.Web.Service.Interfaces;
using Fiby.Cloud.Web.Service.Interfaces.Facturacion;
using Fiby.Cloud.Web.Service.Interfaces.Horario;
using Fiby.Cloud.Web.Service.Interfaces.Inversiones;
using Fiby.Cloud.Web.Service.Interfaces.Maintenance;
using Fiby.Cloud.Web.Service.Interfaces.Pagos;
using Fiby.Cloud.Web.Service.Interfaces.Parametro;
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
            services.AddScoped<IGastoService, GastoService>();
            services.AddScoped<IAnuncioService, AnuncioService>();

            services.AddScoped<ITablaDetalleService, TablaDetalleService>();
            services.AddScoped<IGenerarService, GenerarService>();
            services.AddScoped<ICargaFacturaService, CargaFacturaService>();

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
            services.AddSingleton<ITablaDetalleRepository, TablaDetalleRepository>();

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
            services.AddSingleton<IGastoRepository, GastoRepository>();
            services.AddSingleton<IAnuncioRepository, AnuncioRepository>();
            services.AddSingleton<IGenerarRepository, GenerarRepository>();
            services.AddSingleton<ICargaFacturaRepository, CargaFacturaRepository>();
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
