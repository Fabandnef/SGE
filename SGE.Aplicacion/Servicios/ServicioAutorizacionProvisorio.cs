using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Servicios;

namespace SGE.Aplicacion.Servicios;

public class ServicioAutorizacionProvisorio : IServicioAutorizacion
{
    public bool PoseeElPermiso(int idUsuario, Permiso permiso) => idUsuario == 1;
}