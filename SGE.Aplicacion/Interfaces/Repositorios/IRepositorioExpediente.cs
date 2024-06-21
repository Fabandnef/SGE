using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;

namespace SGE.Aplicacion.Interfaces.Repositorios;

/// <summary>
///     Interfaz para cualquier clase que implemente un repositorio de expedientes.
/// </summary>
public interface IRepositorioExpediente
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    /// <summary>
    ///     Actualiza el estado de un expediente.
    /// </summary>
    /// <param name="idExpediente">ID del expediente.</param>
    /// <param name="estadoExpediente">Nuevo <see cref="EstadoExpediente">Estado</see> del expediente.</param>
    void ActualizarEstado(int idExpediente, EstadoExpediente estadoExpediente);

    /// <summary>
    ///     Dar de alta un expediente.
    /// </summary>
    /// <param name="expediente">Expediente a dar de alta.</param>
    /// <returns><see cref="Expediente" /> dado de alta.</returns>
    /// <exception cref="RepositorioException">Excepción si el expediente ya viene con ID seteado.</exception>
    void Alta(Expediente expediente);

    /// <summary>
    ///     Dar de baja un expediente.
    /// </summary>
    /// <param name="idExpediente">ID del expediente a dar de baja.</param>
    /// <exception cref="RepositorioException">Excepción si el expediente a dar de baja no existe en el archivo.</exception>
    void Baja(int idExpediente);

    /// <summary>
    ///     Buscar un expediente por su ID.
    /// </summary>
    /// <param name="idExpediente">ID del expediente a buscar.</param>
    /// <returns><see cref="Expediente" /> encontrado, o <c>null</c> si no se encontró.</returns>
    Expediente? BuscarPorId(int idExpediente);

    int ContarTotal();

    /// <summary>
    ///     Listar todos los expedientes.
    /// </summary>
    /// <returns><see cref="List{T}" /> de <see cref="Expediente" />.</returns>
    List<Expediente> Listar(int pagina = 1);

    /// <summary>
    ///     Modificar un expediente.
    /// </summary>
    /// <param name="expediente"><see cref="Expediente" /> a modificar.</param>
    /// <exception cref="RepositorioException">Excepción si el expediente no existe en el archivo.</exception>
    void Modificar(Expediente expediente);
    #endregion
}