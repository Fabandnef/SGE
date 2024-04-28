using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.Servicios;

public class ServicioActualizacionEstado(
    IExpedienteRepositorio     expedienteRepositorio,
    EspecificacionCambioEstado especificacionCambioEstado
)
{
    public void ActualizarEstado(int idExpediente)
    {
        EstadoExpediente? estado = especificacionCambioEstado.DefinirEstado(idExpediente);

        if (estado is null) {
            return;
        }

        expedienteRepositorio.ActualizarEstado(idExpediente, estado.Value);
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