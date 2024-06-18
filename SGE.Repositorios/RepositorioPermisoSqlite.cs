using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

public class RepositorioPermisoSqlite(SgeContext context) : IRepositorioPermiso
{
    public void Agregar(Permiso permiso)
    {
        try {
            context.Permisos.Add(permiso);
            context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al agregar el permiso con id {permiso.Id}. {e.Message}");
        }
    }

    public void Modificar(Permiso permiso)
    {
        try {
            context.Permisos.Update(permiso);
            context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al modificar el permiso con id {permiso.Id}. {e.Message}");
        }
    }

    public void Eliminar(Permiso permiso)
    {
        try {
            context.Permisos.Remove(permiso);
            context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al eliminar el permiso con id {permiso.Id}. {e.Message}");
        }
    }
}