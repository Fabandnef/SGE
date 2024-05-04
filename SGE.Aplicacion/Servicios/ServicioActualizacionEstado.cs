using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.Servicios;

public class ServicioActualizacionEstado(
    IExpedienteRepositorio     expedienteRepositorio,
    EspecificacionCambioEstado especificacionCambioEstado
)
{
    public void ActualizarEstado(int expedienteId)
    {
        EstadoExpediente? estado = especificacionCambioEstado.DefinirEstado(expedienteId);

        if (estado is null) {
            return;
        }

        expedienteRepositorio.ActualizarEstado(expedienteId, estado.Value);
    }

    public void ActualizarEstado(Tramite tramite)
    {
        EstadoExpediente? estado = especificacionCambioEstado.DefinirEstado(tramite);

        if (estado is null) {
            return;
        }

        expedienteRepositorio.ActualizarEstado(tramite.ExpedienteId, estado.Value);
    }
}