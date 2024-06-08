using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.Servicios;

/// <summary>
///     Clase que define la especificación para el cambio de estado de un expediente.
/// </summary>
/// <param name="repositorioTramite">Repositorio de trámites.</param>
public class EspecificacionCambioEstado(IRepositorioTramite repositorioTramite)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    /// <summary>
    ///     Define el estado de un expediente.
    /// </summary>
    /// <param name="idExpediente">ID del expediente.</param>
    /// <returns><see cref="EstadoExpediente" /> que corresponde al expediente.</returns>
    public EstadoExpediente? DefinirEstado(int idExpediente)
    {
        Tramite? tramite = repositorioTramite.ObtenerUltimoPorExpediente(idExpediente);

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
    #endregion
}