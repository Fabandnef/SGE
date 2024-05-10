using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Interfaces.Repositorios;

/// <summary>
///     Interfaz para cualquier clase que implemente un repositorio de expedientes.
/// </summary>
public interface IExpedienteRepositorio
{
    /// <summary>
    ///     Actualiza el estado de un expediente.
    /// </summary>
    /// <param name="expedienteId">ID del expediente.</param>
    /// <param name="estadoExpediente">Nuevo <see cref="EstadoExpediente">Estado</see> del expediente.</param>
    void ActualizarEstado(int expedienteId, EstadoExpediente estadoExpediente);

    /// <summary>
    ///     Dar de alta un expediente.
    /// </summary>
    /// <param name="expediente">Expediente a dar de alta.</param>
    /// <returns><see cref="Expediente" /> dado de alta.</returns>
    Expediente Alta(Expediente expediente);

    /// <summary>
    ///     Dar de baja un expediente.
    /// </summary>
    /// <param name="expedienteId">ID del expediente a dar de baja.</param>
    /// <returns><c>True</c> si se dió de baja el expediente, <c>false</c> si no se encontró.</returns>
    bool Baja(int expedienteId);

    /// <summary>
    ///     Buscar un expediente por su ID.
    /// </summary>
    /// <param name="expedienteId">ID del expediente a buscar.</param>
    /// <returns><see cref="Expediente" /> encontrado, o <c>null</c> si no se encontró.</returns>
    Expediente? BuscarPorId(int expedienteId);

    /// <summary>
    ///     Listar todos los expedientes.
    /// </summary>
    /// <returns><see cref="List{T}" /> de <see cref="Expediente" />.</returns>
    List<Expediente> Listar();

    /// <summary>
    ///     Modificar un expediente.
    /// </summary>
    /// <param name="expediente"><see cref="Expediente" /> a modificar.</param>
    void Modificar(Expediente expediente);
}