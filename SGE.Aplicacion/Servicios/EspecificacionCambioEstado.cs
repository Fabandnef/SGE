using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.Servicios;

public class EspecificacionCambioEstado(ITramiteRepositorio tramiteRepositorio)
{
    public EstadoExpediente? DefinirEstado(int idExpediente)
    {
        Tramite? tramite = tramiteRepositorio.ObtenerUltimoPorExpediente(idExpediente);
        
        if (tramite is null) {
            return null;
        }
        
        return tramite.Etiqueta switch {
            EtiquetaTramite.Resolucion => EstadoExpediente.ConResolucion,
            EtiquetaTramite.PaseAEstudio => EstadoExpediente.ParaResolver,
            EtiquetaTramite.PaseAlArchivo => EstadoExpediente.Finalizado,
            _ => null,
        };
    }
    
    public EstadoExpediente? DefinirEstado(Tramite tramite)
    {
        return tramite.Etiqueta switch {
            EtiquetaTramite.Resolucion => EstadoExpediente.ConResolucion,
            EtiquetaTramite.PaseAEstudio => EstadoExpediente.ParaResolver,
            EtiquetaTramite.PaseAlArchivo => EstadoExpediente.Finalizado,
            _ => null,
        };
    }
}