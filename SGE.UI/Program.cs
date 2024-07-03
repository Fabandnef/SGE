using SGE.Aplicacion.CasosDeUso;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;
using SGE.Aplicacion.Interfaces.Validadores;
using SGE.Aplicacion.Servicios;
using SGE.Aplicacion.Validadores;
using SGE.Repositorios;
using SGE.UI.Components;
using SGE.UI.Components.Pages;

namespace SGE.UI;

public class Program
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
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

        // -------------------------
        // SCOPED
        // -------------------------
        
        // Session almacena los datos de la sesión del usuario
        // Es scoped, para mantener viva la sessión en las mismas request
        // pero iniciar una nueva en requests nuevas. Es horrendo, pero 
        // así funciona el framework este.
        builder.Services.AddScoped<Session>();

        // -------------------------
        // TRANSIENT
        // -------------------------
        
        // Las inyecciones transient crean un objeto nuevo cada vez que se solicita
        // por lo que no se comparte entre diferentes solicitudes. Esto es necesario
        // para laburar con la base de datos, porque el contexto de la base de datos
        // es una porquería con los tracking que hace, y es un dolor de cabeza modificar
        // o eliminar entidades que se han traido en un contexto anterior.
        
        // Usuario es la entidad que representa al usuario
        builder.Services.AddTransient<Usuario>();
        
        // Repositorios
        builder.Services.AddTransient<IRepositorioExpediente, RepositorioExpedienteSqlite>();
        builder.Services.AddTransient<IRepositorioTramite, RepositorioTramiteSqlite>();
        builder.Services.AddTransient<IRepositorioUsuario, RepositorioUsuarioSqlite>();
        builder.Services.AddTransient<IRepositorioPermiso, RepositorioPermisoSqlite>();
        builder.Services.AddTransient<IExpedienteValidador, ExpedienteValidador>();
        builder.Services.AddTransient<ITramiteValidador, TramiteValidador>();
        builder.Services.AddTransient<IUsuarioValidador, UsuarioValidador>();

        // Servicios
        builder.Services.AddTransient<IServicioDeClaves, ServicioDeClaves>();
        builder.Services.AddTransient<IServicioAutorizacion, ServicioAutorizacion>();

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
        builder.Services.AddTransient<TramiteBajaCasoDeUso>();
        builder.Services.AddTransient<TramiteBuscarPorIdCasoDeUso>();
        builder.Services.AddTransient<TramiteConsultaPorEtiquetaCasoDeUso>();
        builder.Services.AddTransient<TramiteContarTotalCasoDeUso>();
        builder.Services.AddTransient<TramiteListarCasoDeUso>();
        builder.Services.AddTransient<TramiteListarTodosPorEtiqueta>();
        builder.Services.AddTransient<TramiteModificacionCasoDeUso>();
        builder.Services.AddTransient<UsuarioLoginCasoDeUso>();
        builder.Services.AddTransient<UsuarioRegistrarCasoDeUso>();
        builder.Services.AddTransient<UsuarioModificarCasoDeUso>();
        builder.Services.AddTransient<UsuarioBuscarPorIdCasoDeUso>();
        builder.Services.AddTransient<PermisoListarCasoDeUso>();
        builder.Services.AddTransient<UsuarioListarCasoDeUso>();
        builder.Services.AddTransient<UsuarioContarTotalCasoDeUso>();
        builder.Services.AddTransient<UsuarioBajaCasoDeUso>();

        // Servicios sin interfaz
        builder.Services.AddTransient<ServicioActualizacionEstado>();
        builder.Services.AddTransient<EspecificacionCambioEstado>();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Error", true);
        }

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
           .AddInteractiveServerRenderMode();

        app.Run();
    }
    #endregion
}