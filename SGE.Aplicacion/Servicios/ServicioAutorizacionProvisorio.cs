using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Servicios;

namespace SGE.Aplicacion.Servicios;

/// <summary>
///     Clase que implementa la interfaz IServicioAutorizacion. Permite autorizar a un usuario
///     a realizar ciertas acciones dentro del sistema.
/// </summary>
public class ServicioAutorizacionProvisorio : IServicioAutorizacion
{
    #region IMPLEMENTACIONES DE INTERFACES -------------------------------------------------------------
    #region IServicioAutorizacion
    /// <inheritdoc />
    public bool PoseeElPermiso(Usuario usuario, PermisoEnum permisoEnum)
    {
        return usuario.IsAdmin || usuario.Permisos.Any(p => p.Nombre == permisoEnum.ToString());
    }
    #endregion
    #endregion
}