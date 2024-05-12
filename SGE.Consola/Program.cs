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
        Usuario                usuario               = new();
        IExpedienteValidador   expedienteValidador   = new ExpedienteValidador();
        IExpedienteRepositorio expedienteRepositorio = new RepositorioExpedienteTxt();
        IServicioAutorizacion  servicioAutorizacion  = new ServicioAutorizacionProvisorio();

        ExpedienteAltaCasoDeUso expedienteAltaCasoDeUso = new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);
        ExpedienteListarCasoDeUso expedienteListarCasoDeUso = new(expedienteRepositorio);

        Expediente expedienteNuevo1 = new() { Caratula = "Caratula de prueba del expediente1",};
        Expediente expedienteNuevo2 = new() { Caratula  = "Caratula de prueba del expediente2",};

        // Añado los expedientes
        try {
            expedienteAltaCasoDeUso.Ejecutar(expedienteNuevo1, usuario.Id);
            Console.WriteLine($"Expediente guardado con id: {expedienteNuevo1.Id}");
            expedienteAltaCasoDeUso.Ejecutar(expedienteNuevo1, usuario.Id);
            Console.WriteLine($"Expediente guardado con id: {expedienteNuevo2.Id}");
        } catch (RepositorioException e) {
            Console.WriteLine("Problema reportado por el repositorio: " + e.Message);
        } catch (ValidacionException e) {
            Console.WriteLine("Problema reportado por el validador: " + e.Message);
        } catch (AutorizacionException e) {
            Console.WriteLine("Problema reportado por el servicio de autorización: " + e.Message);
        } catch (Exception e) {
            Console.WriteLine("Problema desconocido: " + e.Message);
        }

        // Listo los expedientes
        List<Expediente> expedientes = expedienteListarCasoDeUso.Ejecutar().ToList();

        foreach (Expediente expediente in expedientes) {
            Console.WriteLine(expediente.ToString());
        }
    }
    #endregion

    #region METODOS ------------------------------------------------------------------------------------
    static private string GenerarFrase(int palabras = 5)
    {
        Random obj = new();
        string st  = string.Empty;
        int    letras;

        for (int i = 0; i < palabras; i++) {
            letras =  obj.Next(5, 10);
            st     += GenerarPalabra(letras) + " ";
        }

        return st.TrimEnd();
    }

    static private string GenerarPalabra(int longitud = 5)
    {
        Random       obj            = new();
        const string letras         = "abcdefghijklmnopqrstuvwxyz";
        int          cantidadLetras = letras.Length;
        string       st             = letras[obj.Next(longitud)].ToString().ToUpper();

        for (int i = 0; i < longitud; i++) {
            st += letras[obj.Next(cantidadLetras)].ToString();
        }

        return st;
    }
    #endregion
}