using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Interfaces;

public interface IServicioAutorizacion
{
    bool PoseeElPermiso(int IdUsuario, Permiso permiso);
}