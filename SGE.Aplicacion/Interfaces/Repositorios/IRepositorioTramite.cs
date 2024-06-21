using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;

namespace SGE.Aplicacion.Interfaces.Repositorios;

public interface IRepositorioTramite
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    /// <summary>
    ///     Da de alta un trámite.
    /// </summary>
    /// <param name="tramite">Trámite a dar de alta.</param>
    /// <returns><see cref="Tramite" /> dado de alta.</returns>
    void Alta(Tramite tramite);

    /// <summary>
    ///     Da de baja un trámite.
    /// </summary>
    /// <param name="idTramite">ID del trámite a dar de baja.</param>
    /// <returns><c>True</c> si se dio de baja el trámite, <c>False</c> si no se encontró.</returns>
    /// <exception cref="RepositorioException">Excepción si el trámite a dar de baja no existe en el archivo.</exception>
    void Baja(int idTramite);

    /// <summary>
    ///     Da de baja todos los trámites de un expediente.
    /// </summary>
    /// <param name="idExpediente">ID del expediente.</param>
    void BajaPorExpediente(int idExpediente);

    /// <summary>
    ///     Modifica un trámite.
    /// </summary>
    /// <param name="tramite">Trámite a modificar.</param>
    void Modificar(Tramite tramite);

    /// <summary>
    ///     Obtiene un trámite por su etiqueta.
    /// </summary>
    /// <param name="etiquetaTramite">Etiqueta del trámite.</param>
    /// <param name="pagina">Pagina para listar los tramites.</param>
    /// <returns><see cref="List{T}" /> de <see cref="Tramite" />Obtener tramites con la etiqueta especificada.</returns>
    List<Tramite> ObtenerPorEtiqueta(EtiquetaTramite etiquetaTramite, int pagina = 1);

    /// <summary>
    ///     Obtiene un trámite por su ID.
    /// </summary>
    /// <param name="idTramite">ID del trámite.</param>
    /// <returns><see cref="Tramite" /> con el ID especificado, <c>null</c> si no se encontró.</returns>
    Tramite? ObtenerPorId(int idTramite);

    /// <summary>
    ///     Obtiene todos los trámites.
    /// </summary>
    /// <returns><see cref="List{T}" /> de <see cref="Tramite" />.</returns>
    List<Tramite> ObtenerTramites(int pagina = 1);
    
    int ContarTotal();

    /// <summary>
    ///     Obtiene todos los trámites de un expediente.
    /// </summary>
    /// <param name="expediente">Expediente al que pertenecen los trámites.</param>
    /// <returns><see cref="List{T}" /> de <see cref="Tramite" />s del expediente.</returns>
    public Expediente ObtenerTramitesPorExpediente(Expediente expediente);

    /// <summary>
    ///     Obtiene el último trámite de un expediente.
    /// </summary>
    /// <param name="idExpediente">ID del expediente.</param>
    /// <returns>Último <see cref="Tramite" /> del expediente, <c>null</c> si no se encontró.</returns>
    Tramite? ObtenerUltimoPorExpediente(int idExpediente);
    #endregion
}