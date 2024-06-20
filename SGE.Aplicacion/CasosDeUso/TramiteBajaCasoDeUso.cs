using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;
using SGE.Aplicacion.Servicios;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteBajaCasoDeUso(
    IRepositorioTramite         repositorioTramite,
    ServicioActualizacionEstado servicioActualizacionEstado,
    IServicioAutorizacion       servicioAutorizacion
)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public void Ejecutar(int idTramite, Usuario usuario)
    {
        if (!servicioAutorizacion.PoseeElPermiso(usuario, PermisoEnum.TramiteBaja)) {
            throw new AutorizacionException("El usuario no tiene permisos para realizar esta acción.");
        }

        Tramite? tramite = repositorioTramite.ObtenerPorId(idTramite);
        
        if (tramite is null) {
            throw new ValidacionException($"No se encontró el trámite con id {idTramite}.");
        }
        
        int idExpediente = tramite.ExpedienteId;
        repositorioTramite.Baja(idTramite);
        servicioActualizacionEstado.ActualizarEstado(idExpediente);
        
    }
    #endregion
}