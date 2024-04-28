using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteModificacionCasoDeUso(ITramiteRepositorio repositorio, IServicioAutorizacion servicioAutorizacion)
{
    public void Ejecutar(Tramite tramite, int idUsuario)
    {
        if (!servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.ExpedienteModificacion)) {
            throw new AutorizacionException("El usuario no tiene permisos para realizar esta acción.");
        }
        
        tramite.IdUsuarioUltimaModificacion = idUsuario;
        tramite.UltimaModificacion          = DateTime.Now;

        repositorio.Modificar(tramite);
    }
}