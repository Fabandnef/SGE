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

        builder.Services.AddSingleton<Session>();
        builder.Services.AddScoped<SgeContext>();
        builder.Services.AddScoped<Usuario>();
        builder.Services.AddScoped<IRepositorioExpediente, RepositorioExpedienteSqlite>();
        builder.Services.AddScoped<IRepositorioTramite, RepositorioTramiteSqlite>();
        builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuarioSqlite>();
        builder.Services.AddScoped<IExpedienteValidador, ExpedienteValidador>();
        builder.Services.AddScoped<IServicioDeClaves, ServicioDeClaves>();
        builder.Services.AddScoped<IServicioAutorizacion, ServicioAutorizacionProvisorio>();
        builder.Services.AddTransient<ExpedienteListarCasoDeUso>();
        builder.Services.AddTransient<ExpedienteAltaCasoDeUso>();
        builder.Services.AddTransient<ExpedienteBajaCasoDeUso>();
        builder.Services.AddTransient<ExpedienteContarTotalCasoDeUso>();
        builder.Services.AddTransient<ExpedienteModificarCasoDeUso>();
        builder.Services.AddTransient<ExpedienteBuscarPorIdConTramitesCasoDeUso>();
        builder.Services.AddTransient<UsuarioLoginCasoDeUso>();
        builder.Services.AddTransient<UsuarioRegistrarCasoDeUso>();

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