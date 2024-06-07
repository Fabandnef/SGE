using Microsoft.EntityFrameworkCore;
using SGE.Aplicacion.CasosDeUso;
using SGE.Aplicacion.Entidades;
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

        using SgeContext       context               = new();
        Usuario?               usuario               = context.Usuarios.Find(1);
        ITramiteRepositorio    tramiteRepositorio    = new RepositorioTramiteSqlite(context);
        IExpedienteValidador   expedienteValidador   = new ExpedienteValidador();
        IExpedienteRepositorio expedienteRepositorio = new RepositorioExpedienteSqlite(context);
        IServicioAutorizacion  servicioAutorizacion  = new ServicioAutorizacionProvisorio();

        ExpedienteAltaCasoDeUso altaCasoDeUso = new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);
        ExpedienteBajaCasoDeUso bajaCasoDeUso = new(expedienteRepositorio, tramiteRepositorio, servicioAutorizacion);

        Expediente expedienteNuevo = new() { Caratula = "Caratula de prueba del expediente", };

        int idExpediente = 0; 
        try {
            altaCasoDeUso.Ejecutar(expedienteNuevo, usuario.Id);
            idExpediente = (int)expedienteNuevo.Id;
            Console.WriteLine($"Expediente guardado con ID {idExpediente}");
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
            bajaCasoDeUso.Ejecutar(idExpediente, usuario.Id);
            Console.WriteLine($"Expediente con ID {idExpediente} eliminado");
            idExpediente = 99999;
            bajaCasoDeUso.Ejecutar(idExpediente, usuario.Id);
            Console.WriteLine($"Expediente con ID {idExpediente} eliminado");
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