using Microsoft.EntityFrameworkCore.Infrastructure;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

internal class RepositorioPermisoSqlite(SgeContext context) : RepositorioSqlite(context), IPermisoRepositorio
{
    public void Agregar(Permiso permiso)
    {
        try {
            Context.Permisos.Add(permiso);
            Context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al agregar el permiso con id {permiso.Id}. {e.Message}");
        }
    }

    public void Modificar(Permiso permiso)
    {
        try {
            Context.Permisos.Update(permiso);
            Context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al modificar el permiso con id {permiso.Id}. {e.Message}");
        }
    }

    public void Eliminar(Permiso permiso)
    {
        try {
            Context.Permisos.Remove(permiso);
            Context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al eliminar el permiso con id {permiso.Id}. {e.Message}");
        }
    }
}