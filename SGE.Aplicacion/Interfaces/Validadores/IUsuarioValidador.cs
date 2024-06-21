using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Interfaces.Validadores;

public interface IUsuarioValidador
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    /// <summary>
    ///     Valida un usuario.
    /// </summary>
    /// <param name="usuario">Usuario a validar.</param>
    /// <param name="error">Mensaje de error si no cumple con las reglas.</param>
    /// <returns><c>True</c> si se considera que el usuario es válido, <c>False</c> caso contrario.</returns>
    bool Validar(Usuario usuario, out string error);
    #endregion
}