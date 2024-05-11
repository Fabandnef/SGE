using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.Servicios;

/// <summary>
///     Clase que define la especificación para el cambio de estado de un expediente.
/// </summary>
/// <param name="tramiteRepositorio">Repositorio de trámites.</param>
public class EspecificacionCambioEstado(ITramiteRepositorio tramiteRepositorio)
{
    /// <summary>
    ///     Define el estado de un expediente.
    /// </summary>
    /// <param name="idExpediente">ID del expediente.</param>
    /// <returns><see cref="EstadoExpediente" /> que corresponde al expediente.</returns>
    public EstadoExpediente? DefinirEstado(int idExpediente)
    {
        Tramite? tramite = tramiteRepositorio.ObtenerUltimoPorExpediente(idExpediente);

        return tramite is null ? null : DefinirEstado(tramite);
    }

    /// <summary>
    ///     Define el estado de un expediente.
    /// </summary>
    /// <param name="tramite">Trámite a partir del cual se define el estado.</param>
    /// <returns><see cref="EstadoExpediente" /> que corresponde al expediente.</returns>
    public EstadoExpediente? DefinirEstado(Tramite tramite)
    {
        return tramite.Etiqueta switch {
                   EtiquetaTramite.Resolucion    => EstadoExpediente.ConResolucion,
                   EtiquetaTramite.PaseAEstudio  => EstadoExpediente.ParaResolver,
                   EtiquetaTramite.PaseAlArchivo => EstadoExpediente.Finalizado,
                   _                             => null,
               };
    }
}