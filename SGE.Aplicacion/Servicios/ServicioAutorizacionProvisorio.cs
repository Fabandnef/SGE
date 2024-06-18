using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;

namespace SGE.Aplicacion.Servicios;

/// <summary>
///     Clase que implementa la interfaz IServicioAutorizacion. Permite autorizar a un usuario
///     a realizar ciertas acciones dentro del sistema.
/// </summary>
public class ServicioAutorizacionProvisorio(IRepositorioUsuario repositorioUsuario) : IServicioAutorizacion
{
    #region IMPLEMENTACIONES DE INTERFACES -------------------------------------------------------------
    #region IServicioAutorizacion
    /// <inheritdoc />
    public bool PoseeElPermiso(Usuario usuario, PermisoEnum permisoEnum)
    {
        // El downside de esto, es que si el permiso está cacheado, y se le quita o se le agregan permisos
        // al usuario, no se va a reflejar en la autorización hasta que el usuario navegue
        // a alguna URL y el sistema refresque los permisos automáticamente. Refrescarlos también acá
        return usuario.IsAdmin || repositorioUsuario.GetUsuario(usuario.Email)!.Permisos.Any(p => p.Nombre == permisoEnum.ToString());
    }
    #endregion
    #endregion
}