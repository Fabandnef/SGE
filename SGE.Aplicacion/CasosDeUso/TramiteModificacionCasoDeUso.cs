using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;
using SGE.Aplicacion.Interfaces.Validadores;
using SGE.Aplicacion.Servicios;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteModificacionCasoDeUso(
    IRepositorioTramite         repositorioTramite,
    ITramiteValidador           tramiteValidador,
    ServicioActualizacionEstado servicioActualizacionEstado,
    IServicioAutorizacion       servicioAutorizacion
)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public void Ejecutar(Tramite tramite, Usuario usuario)
    {
        if (!servicioAutorizacion.PoseeElPermiso(usuario, PermisoEnum.TramiteModificacion)) {
            throw new AutorizacionException("El usuario no tiene permisos para realizar esta acción.");
        }

        if (!tramiteValidador.Validar(tramite, out string error)) {
            throw new ValidacionException(error);
        }

        tramite.IdUsuarioUltimaModificacion = usuario.Id;

        repositorioTramite.Modificar(tramite);
        servicioActualizacionEstado.ActualizarEstado(tramite);
    }
    #endregion
}