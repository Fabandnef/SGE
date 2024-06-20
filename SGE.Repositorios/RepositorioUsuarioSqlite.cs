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
            context.Usuarios.Update(usuario);
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
            throw new RepositorioException($"Error al eliminar el usuario con email {usuario.Email}. {e.Message}");
        }
    }

    public Usuario? GetUsuario(string email)
    {
        return context.Usuarios.Include("Permisos").AsNoTracking().FirstOrDefault(u => u.Email == email);
    }

    public List<Usuario> GetUsuarios() => context.Usuarios.ToList();
}