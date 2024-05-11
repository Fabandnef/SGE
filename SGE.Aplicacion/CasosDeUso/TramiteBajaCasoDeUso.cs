using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;
using SGE.Aplicacion.Servicios;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteBajaCasoDeUso(
    ITramiteRepositorio         tramiteRepositorio,
    ServicioActualizacionEstado servicioActualizacionEstado,
    IServicioAutorizacion       servicioAutorizacion
)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public void Ejecutar(int idTramite, int idUsuarioActual)
    {
        if (!servicioAutorizacion.PoseeElPermiso(idUsuarioActual, Permiso.TramiteBaja)) {
            throw new AutorizacionException("El usuario no tiene permisos para realizar esta acción.");
        }

        if (!tramiteRepositorio.Baja(idTramite)) {
            throw new RepositorioException("El trámite a eliminar no existe.");
        }

        servicioActualizacionEstado.ActualizarEstado(idTramite);
    }
    #endregion
}