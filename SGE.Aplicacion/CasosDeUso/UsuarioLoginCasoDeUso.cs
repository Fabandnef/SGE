using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;

namespace SGE.Aplicacion.CasosDeUso;

public class UsuarioLoginCasoDeUso(IServicioDeClaves servicioDeClaves, IRepositorioUsuario repositorioUsuario)
{
    public Usuario Ejecutar(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            throw new AutorizacionException("El email y la contraseña son obligatorios.");
        }
        
        Usuario? usuario = repositorioUsuario.GetUsuario(email);
        
        if (usuario == null)
        {
            throw new AutorizacionException("Credenciales incorrectas, intente de nuevo.");
        }
        
        if (!servicioDeClaves.Validate(password, usuario.Password))
        {
            throw new AutorizacionException("Credenciales incorrectas, intente de nuevo.");
        }

        return usuario;
    }
}