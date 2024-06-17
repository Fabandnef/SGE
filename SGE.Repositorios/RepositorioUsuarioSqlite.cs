using Microsoft.EntityFrameworkCore;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

public class RepositorioUsuarioSqlite(SgeContext context) : RepositorioSqlite(context), IRepositorioUsuario
{
    public void Register(Usuario usuario)
    {
        try {
            Context.Usuarios.Add(usuario);
            Context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al registrar el usuario con email {usuario.Email}. {e.Message}");
        }
    }

    public void Update(Usuario usuario)
    {
        try {
            Context.Usuarios.Update(usuario);
            Context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al actualizar el usuario con email {usuario.Email}. {e.Message}");
        }
    }

    public void Delete(Usuario usuario)
    {
        try {
            Context.Usuarios.Remove(usuario);
            Context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al eliminar el usuario con email {usuario.Email}. {e.Message}");
        }
    }

    public Usuario? GetUsuario(string email)
    {
        return Context.Usuarios.Include("Permisos").FirstOrDefault(u => u.Email == email);
    }

    public List<Usuario> GetUsuarios() => Context.Usuarios.ToList();
}