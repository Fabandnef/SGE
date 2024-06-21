using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Validadores;

namespace SGE.Aplicacion.Validadores;

public class UsuarioValidador : IUsuarioValidador
{
    #region IMPLEMENTACIONES DE INTERFACES -------------------------------------------------------------
    #region IExpedienteValidador
    /// <inheritdoc />
    public bool Validar(Usuario usuario, out string error)
    {
        error = "";

        if (string.IsNullOrWhiteSpace(usuario.Nombre)) {
            error += "El nombre no puede estar vacío ni pueden ser solo espacios en blanco.";
        }

        if (string.IsNullOrWhiteSpace(usuario.Apellido)) {
            error += "El apellido no puede estar vacío ni pueden ser solo espacios en blanco.";
        }

        if (string.IsNullOrWhiteSpace(usuario.Email)) {
            error += "El email no puede estar vacío ni pueden ser solo espacios en blanco.";
        }

        if (string.IsNullOrWhiteSpace(usuario.Password)) {
            error += "La contraseña no puede estar vacía ni pueden ser solo espacios en blanco.";
        }

        if (usuario.Password.Length < 6) {
            error += "La contraseña debe tener al menos 6 caracteres.";
        }

        return string.IsNullOrWhiteSpace(error);
    }
    #endregion
    #endregion
}