using Microsoft.EntityFrameworkCore;
using SGE.Aplicacion.CasosDeUso;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;
using SGE.Aplicacion.Interfaces.Validadores;
using SGE.Aplicacion.Servicios;
using SGE.Aplicacion.Validadores;
using SGE.Repositorios;

namespace SGE.Consola;

public class Program
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    static public void Main(string[] args)
    {
        SgeSqlite.SetUp();

        using SgeContext context = new();

        Usuario? usuario = context.Usuarios.Include("Permisos").FirstOrDefault(u => u.Email == "fabro.la22@gmail.com");
        if ((usuario == null) || (usuario.Permisos.Count == 0)) {
            Console.WriteLine("Usuario no encontrado.");
            return;
        }

        IExpedienteValidador       expedienteValidador        = new ExpedienteValidador();
        ITramiteValidador          tramiteValidador           = new TramiteValidador();
        IExpedienteRepositorio     expedienteRepositorio      = new RepositorioExpedienteSqlite(context);
        ITramiteRepositorio        tramiteRepositorio         = new RepositorioTramiteSqlite(context);
        IServicioAutorizacion      servicioAutorizacion       = new ServicioAutorizacionProvisorio();
        EspecificacionCambioEstado especificacionCambioEstado = new(tramiteRepositorio);

        ServicioActualizacionEstado servicioActualizacionEstado =
            new(expedienteRepositorio, especificacionCambioEstado);

        ExpedienteAltaCasoDeUso expedienteAltaCasoDeUso =
            new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);

        TramiteAltaCasoDeUso tramiteAltaCasoDeUso =
            new(tramiteRepositorio, tramiteValidador, servicioAutorizacion, servicioActualizacionEstado);

        TramiteModificacionCasoDeUso tramiteModificacionCasoDeUso =
            new(tramiteRepositorio, tramiteValidador, servicioActualizacionEstado, servicioAutorizacion);

        TramiteConsultaPorEtiquetaCasoDeUso tramiteConsultaPorEtiquetaCasoDeUso =
            new(tramiteRepositorio);

        Expediente expediente = new() { Caratula = "Carátula de prueba del expediente" };

        try {
            expedienteAltaCasoDeUso.Ejecutar(expediente, usuario);
            Console.WriteLine($"Expediente guardado con ID {expediente.Id}");
        } catch (AutorizacionException ex) {
            Console.WriteLine($"Error de autorización: {ex.Message}");
        } catch (ValidacionException ex) {
            Console.WriteLine($"Error de validación: {ex.Message}");
        } catch (RepositorioException ex) {
            Console.WriteLine($"Error de repositorio: {ex.Message}");
        } catch (Exception ex) {
            Console.WriteLine($"Error inesperado: {ex.Message}");
        }

        Tramite tramite = new() {
                                    Contenido    = "Contenido de prueba del trámite1",
                                    ExpedienteId = expediente.Id,
                                };

        try {
            tramiteAltaCasoDeUso.Ejecutar(tramite, usuario);
            Console.WriteLine($"Trámite con ID {tramite.Id} guardado al expediente con ID {tramite.ExpedienteId}");
        } catch (AutorizacionException ex) {
            Console.WriteLine($"Error de autorización: {ex.Message}");
        } catch (ValidacionException ex) {
            Console.WriteLine($"Error de validación: {ex.Message}");
        } catch (RepositorioException ex) {
            Console.WriteLine($"Error de repositorio: {ex.Message}");
        } catch (Exception ex) {
            Console.WriteLine($"Error inesperado: {ex.Message}");
        }

        try {
            Console.WriteLine($"Modificando trámite con ID {tramite.Id}...");
            tramite.Contenido = "Contenido de prueba modificado del trámite";
            tramiteModificacionCasoDeUso.Ejecutar(tramite, usuario);
            Console.WriteLine($"Trámite modificado con ID {tramite.Id}.");
        } catch (AutorizacionException ex) {
            Console.WriteLine($"Error de autorización: {ex.Message}");
        } catch (ValidacionException ex) {
            Console.WriteLine($"Error de validación: {ex.Message}");
        } catch (RepositorioException ex) {
            Console.WriteLine($"Error de repositorio: {ex.Message}");
        } catch (Exception ex) {
            Console.WriteLine($"Error inesperado: {ex.Message}");
        }

        try {
            Console.WriteLine("Buscando trámites con etiqueta EscritoPresentado...");
            Console.WriteLine("Trámites encontrados:");

            List<Tramite> tramites
                = tramiteConsultaPorEtiquetaCasoDeUso.Ejecutar(EtiquetaTramite.EscritoPresentado);

            foreach (Tramite t in tramites) {
                Console.WriteLine(t.ToString());
            }
        } catch (RepositorioException ex) {
            Console.WriteLine($"Error de repositorio: {ex.Message}");
        } catch (Exception ex) {
            Console.WriteLine($"Error inesperado: {ex.Message}");
        }

        try {
            Console.WriteLine($"Modificando trámite con ID {tramite.Id} a un contenido vacío...");
            tramite.Contenido = "";
            tramiteModificacionCasoDeUso.Ejecutar(tramite, usuario);
            Console.WriteLine($"Trámite modificado con ID {tramite.Id}.");
        } catch (AutorizacionException ex) {
            Console.WriteLine($"Error de autorización: {ex.Message}");
        } catch (ValidacionException ex) {
            Console.WriteLine($"Error de validación: {ex.Message}");
        } catch (RepositorioException ex) {
            Console.WriteLine($"Error de repositorio: {ex.Message}");
        } catch (Exception ex) {
            Console.WriteLine($"Error inesperado: {ex.Message}");
        }
    }
    #endregion
}