using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces;

namespace SGE.Aplicacion.Servicios;

public class ServicioActualizacionEstado(IExpedienteRepositorio repositorio, EspecificacionCambioEstado especificacionCambioEstado)
{
    public void ActualizarEstado(int idExpediente)
    {
        EstadoExpediente? estado = especificacionCambioEstado.DefinirEstado(idExpediente);

        if (estado is null) {
            return;
        }

        repositorio.ExpedienteActualizarEstado(idExpediente, estado.Value);
    }
    
    public void ActualizarEstado(Tramite tramite)
    {
        EstadoExpediente? estado = especificacionCambioEstado.DefinirEstado(tramite);

        if (estado is null) {
            return;
        }

        repositorio.ExpedienteActualizarEstado(tramite.ExpedienteId, estado.Value);
    }
}