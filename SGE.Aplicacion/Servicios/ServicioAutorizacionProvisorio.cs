using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Servicios;

namespace SGE.Aplicacion.Servicios;

/// <summary>
///     Clase que implementa la interfaz IServicioAutorizacion. Permite autorizar a un usuario
///     a realizar ciertas acciones dentro del sistema.
/// </summary>
public class ServicioAutorizacionProvisorio : IServicioAutorizacion
{
    /// <inheritdoc />
    public bool PoseeElPermiso(int idUsuario, Permiso permiso) => idUsuario == 1;
}