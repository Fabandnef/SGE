using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces;
using SGE.Aplicacion.Servicios;
using SGE.Aplicacion.Validadores;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteAltaCasoDeUso(
    ITramiteRepositorio         repositorio,
    IServicioAutorizacion       servicioAutorizacion,
    ServicioActualizacionEstado servicioActualizacionEstado,
    TramiteValidador            validador
)
{
    public void Ejecutar(Tramite tramite, int idUsuario)
    {
        if (!validador.ValidarTramite(tramite, out string mensajeError)) {
            throw new ValidacionException(mensajeError);
        }

        if (!servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.ExpedienteAlta)) {
            throw new AutorizacionException("El usuario no tiene permisos para realizar esta acción.");
        }

        tramite.FechaCreacion               = DateTime.Now;
        tramite.UltimaModificacion          = DateTime.Now;
        tramite.IdUsuarioUltimaModificacion = idUsuario;

        repositorio.AltaTramite(tramite);
        servicioActualizacionEstado.ActualizarEstado(tramite);
    }
}