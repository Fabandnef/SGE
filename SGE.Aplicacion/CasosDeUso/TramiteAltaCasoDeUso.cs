using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;
using SGE.Aplicacion.Interfaces.Validadores;
using SGE.Aplicacion.Servicios;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteAltaCasoDeUso(
    ITramiteRepositorio         tramiteRepositorio,
    ITramiteValidador           tramiteValidador,
    IServicioAutorizacion       servicioAutorizacion,
    ServicioActualizacionEstado servicioActualizacionEstado
)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public Tramite Ejecutar(Tramite tramite, int idUsuario)
    {
        if (!servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.TramiteAlta)) {
            throw new AutorizacionException("El usuario no tiene permisos para realizar esta acción.");
        }
        
        if (!tramiteValidador.Validar(tramite, out string error)) {
            throw new ValidacionException(error);
        }

        tramite.FechaCreacion               = DateTime.Now;
        tramite.UltimaModificacion          = DateTime.Now;
        tramite.IdUsuarioUltimaModificacion = idUsuario;

        Tramite t = tramiteRepositorio.Alta(tramite);
        servicioActualizacionEstado.ActualizarEstado(tramite);

        return t;
    }
    #endregion
}