using SGE.Aplicacion.CasosDeUso;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Servicios;
using SGE.Aplicacion.Validadores;

namespace SGE.Consola;

using SGE.Aplicacion;
using SGE.Repositorios;

public class Program
{
    public static void Main(string[] args)
    {
        Usuario usuario = new();

        TramiteValidador               validador            = new();
        RepositorioTramiteTxt          repositorio          = new();
        ServicioAutorizacionProvisorio servicioAutorizacion = new();

        TramiteAltaCasoDeUso tramiteAltaCasoDeUso = new(repositorio, servicioAutorizacion, validador);

        Tramite t1 = new Tramite() {
                                       Contenido                   = "Trámite 1",
                                       ExpedienteId                = 32,
                                       Etiqueta                    = EtiquetaTramite.Notificacion,
                                       IdUsuarioUltimaModificacion = usuario.Id,
                                   };

        Tramite t2 = new Tramite() {
                                       Contenido                   = "Trámite 2",
                                       ExpedienteId                = 54,
                                       Etiqueta                    = EtiquetaTramite.Notificacion,
                                       IdUsuarioUltimaModificacion = usuario.Id,
                                   };

        Tramite t3 = new Tramite() {
                                       Contenido                   = "Trámite 3",
                                       ExpedienteId                = 12,
                                       Etiqueta                    = EtiquetaTramite.PaseAEstudio,
                                       IdUsuarioUltimaModificacion = usuario.Id,
                                   };

        Tramite t4 = new Tramite() {
                                       Contenido                   = "Trámite 4",
                                       ExpedienteId                = 76,
                                       Etiqueta                    = EtiquetaTramite.Notificacion,
                                       IdUsuarioUltimaModificacion = usuario.Id,
                                   };

        Tramite t5 = new Tramite() {
                                       Contenido                   = "Trámite 5",
                                       ExpedienteId                = 89,
                                       Etiqueta                    = EtiquetaTramite.PaseAEstudio,
                                       IdUsuarioUltimaModificacion = usuario.Id,
                                   };

        Tramite t6 = new Tramite() {
                                       Contenido                   = "Trámite 6",
                                       ExpedienteId                = 45,
                                       Etiqueta                    = EtiquetaTramite.Notificacion,
                                       IdUsuarioUltimaModificacion = usuario.Id,
                                   };

        Tramite t7 = new Tramite() {
                                       Contenido                   = "Trámite 7",
                                       ExpedienteId                = 67,
                                       Etiqueta                    = EtiquetaTramite.PaseAEstudio,
                                       IdUsuarioUltimaModificacion = usuario.Id,
                                   };

        Tramite t8 = new Tramite() {
                                       Contenido                   = "Trámite 8",
                                       ExpedienteId                = 23,
                                       Etiqueta                    = EtiquetaTramite.Notificacion,
                                       IdUsuarioUltimaModificacion = usuario.Id,
                                   };

        try {
            tramiteAltaCasoDeUso.Ejecutar(t1, usuario.Id);
            tramiteAltaCasoDeUso.Ejecutar(t2, usuario.Id);
            tramiteAltaCasoDeUso.Ejecutar(t3, usuario.Id);
            tramiteAltaCasoDeUso.Ejecutar(t4, usuario.Id);
            tramiteAltaCasoDeUso.Ejecutar(t5, usuario.Id);
            tramiteAltaCasoDeUso.Ejecutar(t6, usuario.Id);
            tramiteAltaCasoDeUso.Ejecutar(t7, usuario.Id);
            tramiteAltaCasoDeUso.Ejecutar(t8, usuario.Id);
        }
        catch (ValidacionException e) {
            Console.WriteLine(e.Message);
        }
        
        TramiteBajaCasoDeUso tramiteBajaCasoDeUso = new(repositorio, servicioAutorizacion);

        tramiteBajaCasoDeUso.Ejecutar(2, usuario.Id);
        tramiteBajaCasoDeUso.Ejecutar(7, usuario.Id);
        
        TramiteConsultaPorEtiquetaCasoDeUso tramiteConsultaPorEtiquetaCasoDeUso = new(repositorio);
        
        List<Tramite> tramitesNotificacion = tramiteConsultaPorEtiquetaCasoDeUso.Ejecutar(EtiquetaTramite.Notificacion).ToList();
    }
}