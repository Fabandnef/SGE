using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Interfaces.Servicios;

/// <summary>
///     Interfaz que define los métodos para la autorización de usuarios.
/// </summary>
public interface IServicioAutorizacion
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    /// <summary>
    ///     Indica si un usuario posee un permiso dado.
    /// </summary>
    /// <param name="usuario">Identificador del usuario.</param>
    /// <param name="permisoEnum">Permiso a verificar.</param>
    /// <returns><c>True</c> si el usuario posee el permiso, <c>False</c> caso contrario.</returns>
    bool PoseeElPermiso(Usuario usuario, PermisoEnum permisoEnum);
    #endregion
}