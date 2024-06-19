using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Servicios;
using SGE.Aplicacion.Interfaces.Validadores;
using SGE.Aplicacion.Servicios;
using SGE.Aplicacion.Validadores;

namespace SGE.UI;

using Components;
using Aplicacion.CasosDeUso;
using Repositorios;
using SGE.Aplicacion.Interfaces.Repositorios;

public class Program
{
    static public void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
               .AddInteractiveServerComponents();

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        SgeSqlite.SetUp();

        // -------------------------
        // INYECCIÓN DE DEPENDENCIAS
        // -------------------------
        
        // SINGLETONS
        // Session almacena los datos de la sesión del usuario
        builder.Services.AddSingleton<Session>();
        
        // SCOPED
        // Usuario es la entidad que representa al usuario
        builder.Services.AddScoped<Usuario>();
        // Repositorios
        builder.Services.AddScoped<IRepositorioExpediente, RepositorioExpedienteSqlite>();
        builder.Services.AddScoped<IRepositorioTramite, RepositorioTramiteSqlite>();
        builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuarioSqlite>();
        builder.Services.AddScoped<IExpedienteValidador, ExpedienteValidador>();
        builder.Services.AddScoped<ITramiteValidador, TramiteValidador>();
        // Servicios
        builder.Services.AddScoped<IServicioDeClaves, ServicioDeClaves>();
        builder.Services.AddScoped<IServicioAutorizacion, ServicioAutorizacionProvisorio>();
        
        // TRANSIENT
        // SgeContext es el contexto de la base de datos
        builder.Services.AddTransient<SgeContext>();
        // Casos de uso
        builder.Services.AddTransient<ExpedienteListarCasoDeUso>();
        builder.Services.AddTransient<ExpedienteAltaCasoDeUso>();
        builder.Services.AddTransient<ExpedienteBajaCasoDeUso>();
        builder.Services.AddTransient<ExpedienteContarTotalCasoDeUso>();
        builder.Services.AddTransient<ExpedienteModificarCasoDeUso>();
        builder.Services.AddTransient<ExpedienteBuscarPorIdConTramitesCasoDeUso>();
        builder.Services.AddTransient<TramiteAltaCasoDeUso>();
        builder.Services.AddTransient<UsuarioLoginCasoDeUso>();
        builder.Services.AddTransient<UsuarioRegistrarCasoDeUso>();
        builder.Services.AddTransient<UsuarioRefrescarCasoDeUso>();

        //Servicios
        builder.Services.AddTransient<ServicioActualizacionEstado>();
        builder.Services.AddTransient<EspecificacionCambioEstado>();
        
        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
        }

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
           .AddInteractiveServerRenderMode();

        app.Run();
    }
}