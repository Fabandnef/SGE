using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;

namespace SGE.Aplicacion.CasosDeUso;

public class UsuarioBajaCasoDeUso(
    IRepositorioUsuario repositorioUsuario, 
    IServicioAutorizacion servicioAutorizacion
)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public void Ejecutar(Usuario usuario, Usuario usuarioActual)
    {
        if (!servicioAutorizacion.PoseeElPermiso(usuarioActual, PermisoEnum.ExpedienteBaja)) {
            throw new AutorizacionException("El usuario no tiene permisos para dar de baja un expediente.");
        }
        
        repositorioUsuario.Delete(usuario);
    }
    #endregion
}