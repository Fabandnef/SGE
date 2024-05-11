using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.Servicios;

/// <summary>
///     Servicio que permite actualizar el estado del expediente. Para ello,
///     utilizar una clase que implemente la interfaz <see cref="EspecificacionCambioEstado"/>
///     para determinar el nuevo estado del expediente, y en base a eso, actualizarlo.
/// </summary>
/// <param name="expedienteRepositorio">Repositorio de expedientes.</param>
/// <param name="especificacionCambioEstado">Servicio para determinar el nuevo
/// estado del expediente.</param>
public class ServicioActualizacionEstado(
    IExpedienteRepositorio     expedienteRepositorio,
    EspecificacionCambioEstado especificacionCambioEstado
)
{

    /// <summary>
    ///     Actualiza el estado de un expediente en base a la especificación de cambio de estado.
    /// </summary>
    /// <param name="idExpediente">ID del expediente.</param>
    public void ActualizarEstado(int idExpediente)
    {
        EstadoExpediente? estado = especificacionCambioEstado.DefinirEstado(idExpediente);

        if (estado is null) {
            return;
        }

        expedienteRepositorio.ActualizarEstado(idExpediente, estado.Value);
    }

    /// <summary>
    ///     Actualiza el estado de un expediente en base a la especificación de cambio de estado.
    /// </summary>
    /// <param name="tramite">Trámite a utilizar para verificar estados.</param>
    public void ActualizarEstado(Tramite tramite)
    {
        EstadoExpediente? estado = especificacionCambioEstado.DefinirEstado(tramite);

        if (estado is null) {
            return;
        }

        expedienteRepositorio.ActualizarEstado(tramite.idExpediente, estado.Value);
    }
}