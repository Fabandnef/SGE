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
    public bool PoseeElPermiso(int idUsuario, PermisoEnum permisoEnum) => idUsuario == 1;
    #endregion
    #endregion
}