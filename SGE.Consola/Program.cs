using SGE.Aplicacion.CasosDeUso;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Servicios;
using SGE.Aplicacion.Validadores;
using SGE.Repositorios;

namespace SGE.Consola;

public class Program
{
    static public void Main(string[] args)
    {
        Usuario usuario = new();

        ExpedienteValidador            expedienteValidador         = new();
        TramiteValidador               tramiteValidador            = new();
        RepositorioExpedienteTxt       repositorioExpediente       = new();
        RepositorioTramiteTxt          repositorioTramite          = new();
        ServicioAutorizacionProvisorio servicioAutorizacion        = new();
        EspecificacionCambioEstado     especificacionCambioEstado  = new(repositorioTramite);
        ServicioActualizacionEstado    servicioActualizacionEstado = new(repositorioExpediente, especificacionCambioEstado);
        ExpedienteAltaCasoDeUso        expedienteAltaCasoDeUso     = new(repositorioExpediente, expedienteValidador, servicioAutorizacion);

        TramiteAltaCasoDeUso tramiteAltaCasoDeUso =
            new(repositorioTramite, tramiteValidador, servicioAutorizacion, servicioActualizacionEstado);

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
                expedienteAltaCasoDeUso.Ejecutar(expediente, usuario.Id);
            }
            catch (ValidacionException e) {
                Console.WriteLine(e.Message);
            }
        }

        List<Tramite> tramites = [];

        for (int i = 0; i < 80; i++) {
            tramites.Add(new Tramite {
                                         ExpedienteId = new Random().Next(1, 15),
                                         Etiqueta = (EtiquetaTramite)(Enum.GetValues(typeof(EtiquetaTramite))
                                                                          .GetValue(new Random().Next(0, 5)) ?? 0),
                                         Contenido = GenerarFrase(new Random().Next(5, 24)),
                                     }
                        );
        }

        foreach (Tramite tramite in tramites) {
            try {
                tramiteAltaCasoDeUso.Ejecutar(tramite, usuario.Id);
            }
            catch (ValidacionException e) {
                Console.WriteLine(e.Message);
            }
        }

        ExpedienteBajaCasoDeUso expedienteBajaCasoDeUso =
            new(repositorioExpediente, repositorioTramite, servicioAutorizacion);

        expedienteBajaCasoDeUso.Ejecutar(2, usuario.Id);

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
}