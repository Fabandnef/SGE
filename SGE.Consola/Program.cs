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
        Usuario usuario = new();

        IExpedienteValidador       expedienteValidador        = new ExpedienteValidador();
        ITramiteValidador          tramiteValidador           = new TramiteValidador();
        IExpedienteRepositorio     expedienteRepositorio      = new RepositorioExpedienteTxt();
        ITramiteRepositorio        tramiteRepositorio         = new RepositorioTramiteTxt();
        IServicioAutorizacion      servicioAutorizacion       = new ServicioAutorizacionProvisorio();
        EspecificacionCambioEstado especificacionCambioEstado = new(tramiteRepositorio);

        ServicioActualizacionEstado servicioActualizacionEstado =
            new(expedienteRepositorio, especificacionCambioEstado);

        ExpedienteAltaCasoDeUso expedienteAltaCasoDeUso =
            new(expedienteRepositorio, expedienteValidador, servicioAutorizacion);

        TramiteAltaCasoDeUso tramiteAltaCasoDeUso =
            new(tramiteRepositorio, tramiteValidador, servicioAutorizacion, servicioActualizacionEstado);

        List<Expediente> expedientes = [];

        for (int i = 0; i < 15; i++) {
            expedientes.Add(new Expediente {
                                               IdUsuarioUltimaModificacion = usuario.Id,
                                               Caratula                    = GenerarFrase(new Random().Next(3, 8)),
                                           }
                           );
        }

        foreach (Expediente expediente in expedientes) {
            try {
                Expediente e = expedienteAltaCasoDeUso.Ejecutar(expediente, usuario.Id);
                Console.WriteLine($"Expediente {e.Id} creado correctamente.");
            } catch (ValidacionException e) {
                Console.WriteLine(e.Message);
            }
        }

        List<Tramite> tramites = [];

        for (int i = 0; i < 80; i++) {
            tramites.Add(new Tramite {
                                         IdExpediente = new Random().Next(1, 15),
                                         Etiqueta = (EtiquetaTramite)(Enum.GetValues(typeof(EtiquetaTramite))
                                                                          .GetValue(new Random().Next(0, 5)) ?? 0),
                                         Contenido = GenerarFrase(new Random().Next(5, 24)),
                                     }
                        );
        }

        foreach (Tramite tramite in tramites) {
            try {
                tramiteAltaCasoDeUso.Ejecutar(tramite, usuario.Id);
            } catch (ValidacionException e) {
                Console.WriteLine(e.Message);
            }
        }

//        ExpedienteBajaCasoDeUso expedienteBajaCasoDeUso =
//            new(repositorioExpediente, repositorioTramite, servicioAutorizacion);
//
//        try {
//            expedienteBajaCasoDeUso.Ejecutar(2, usuario.Id);
//        } catch (Exception e) {
//            Console.WriteLine(e.Message);
//        }

        ExpedienteListarConTramitesCasoDeUso expedienteListarConTramitesCasoDeUso =
            new(expedienteRepositorio, tramiteRepositorio);

        List<Expediente> expedientesConTramites = expedienteListarConTramitesCasoDeUso.Ejecutar();

        ExpedienteBuscarPorIdConTramitesCasoDeUso expedienteBuscarPorIdConTramitesCasoDeUso =
            new(expedienteRepositorio, tramiteRepositorio);

        Expediente? expedienteBuscado = expedienteBuscarPorIdConTramitesCasoDeUso.Ejecutar(1);

        // TramiteBajaCasoDeUso tramiteBajaCasoDeUso = new(repositorioTramite, servicioAutorizacion);
        //
        // tramiteBajaCasoDeUso.Ejecutar(2, usuario.Id);
        // tramiteBajaCasoDeUso.Ejecutar(7, usuario.Id);
        //
        // TramiteConsultaPorEtiquetaCasoDeUso tramiteConsultaPorEtiquetaCasoDeUso = new(repositorioTramite);
        //
        // List<Tramite> tramitesNotificacion =
        //     tramiteConsultaPorEtiquetaCasoDeUso.Ejecutar(EtiquetaTramite.Notificacion).ToList();
        //
        // Console.WriteLine("Trámites de notificación:");
        //
        // foreach (Tramite t in tramitesNotificacion) {
        //     Console.WriteLine("-----------------------------------");
        //     Console.WriteLine(t.FormatoLegible());
        // }
        //
        // TramiteModificacionCasoDeUso tramiteModificacionCasoDeUso = new(repositorioTramite, servicioAutorizacion);
        // Tramite                      tramiteParaEditar            = tramitesNotificacion[1];
        //
        // tramiteParaEditar.Contenido = "Trámite 4 modificado";
        // tramiteModificacionCasoDeUso.Ejecutar(tramiteParaEditar, usuario.Id);

        Console.ReadKey();
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