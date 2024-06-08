using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Interfaces.Repositorios;

public interface IPermisoRepositorio
{
    public void Agregar(Permiso permiso);
    
    public void Modificar(Permiso permiso);
    
    public void Eliminar(Permiso permiso);
}