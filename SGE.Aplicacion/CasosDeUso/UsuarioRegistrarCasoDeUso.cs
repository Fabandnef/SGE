using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;

namespace SGE.Aplicacion.CasosDeUso;

public class UsuarioRegistrarCasoDeUso(IRepositorioUsuario repositorioUsuario, IServicioDeClaves servicioDeClaves)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public Usuario Ejecutar(string firstname, string lastname, string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
            throw new AutorizacionException("El email y la contraseña son obligatorios.");
        }

        Usuario? usuario = repositorioUsuario.GetUsuario(email);

        if (usuario != null) {
            throw new AutorizacionException("El email ya está registrado.");
        }

        string passwordHash = servicioDeClaves.Encrypt(password);

        usuario = new Usuario {
                                  Nombre   = firstname,
                                  Apellido = lastname,
                                  Email    = email,
                                  Password = passwordHash,
                              };

        repositorioUsuario.Register(usuario);

        usuario = repositorioUsuario.GetUsuario(email);

        if (usuario == null) {
            throw new RepositorioException("Error al registrar el usuario.");
        }

        return usuario;
    }
    #endregion
}