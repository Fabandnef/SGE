using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Interfaces.Servicios;

public interface IServicioAutorizacion
{
    bool PoseeElPermiso(int idUsuario, Permiso permiso);
}