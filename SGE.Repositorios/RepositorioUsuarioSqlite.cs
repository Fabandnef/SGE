using Microsoft.EntityFrameworkCore;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

public class RepositorioUsuarioSqlite(SgeContext context) : IRepositorioUsuario
{
    public void Register(Usuario usuario)
    {
        try {
            context.Usuarios.Add(usuario);
            context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al registrar el usuario con email {usuario.Email}. {e.Message}");
        }
    }

    public void Update(Usuario usuario)
    {
        try {
            Usuario usuarioDb = context.Usuarios.Include("Permisos").FirstOrDefault(u => u.Id == usuario.Id)!;
            context.Entry(usuarioDb).CurrentValues.SetValues(usuario);
            
            usuarioDb.Permisos.Clear();
            
            foreach (Permiso permiso in usuario.Permisos) {
                usuarioDb.Permisos.Add(context.Permisos.Find(permiso.Id)!);
            }
            
            context.Update(usuarioDb);
            
            context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al actualizar el usuario con email {usuario.Email}. {e.Message}");
        }
    }

    public void Delete(Usuario usuario)
    {
        try {
            context.Usuarios.Remove(usuario);
            context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al eliminar el usuario con id {usuario.Id}. {e.Message}");
        }
    }

    public Usuario? GetUsuario(string email)
    {
        return context.Usuarios.Include("Permisos").AsNoTracking().FirstOrDefault(u => u.Email == email);
    }

    public List<Usuario> GetUsuarios(int page) => context.Usuarios.Skip((page - 1) * 10).Take(10).ToList().ToList();

    public int ContarTotal() => context.Usuarios.Count();
    
    public Usuario? BuscarPorId(int idUsuario) => context.Usuarios.Include("Permisos").AsNoTracking().FirstOrDefault(u => u.Id == idUsuario);
}