using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces;
using SGE.Aplicacion.Validadores;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteBajaCasoDeUso(ITramiteRepositorio repositorio, IServicioAutorizacion servicioAutorizacion)
{
    public void Ejecutar(int idTramite, int idUsuarioActual)
    {
        if (idTramite <= 0) {
            throw new RepositorioException("El Id del trámite debe ser mayor a 0.");
        }
        
        if (!servicioAutorizacion.PoseeElPermiso(idUsuarioActual, Permiso.ExpedienteBaja)) {
            throw new AutorizacionException("El usuario no tiene permisos para realizar esta acción.");
        }
        
        repositorio.EliminarTramite(idTramite);
    }
}