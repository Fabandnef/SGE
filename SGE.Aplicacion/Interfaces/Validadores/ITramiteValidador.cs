using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Interfaces.Validadores;

/// <summary>
///     Interfaz que define los métodos para validar un expediente.
/// </summary>
public interface ITramiteValidador
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    /// <summary>
    ///     Valida un trámite.
    /// </summary>
    /// <param name="tramite">Trámite a validar.</param>
    /// <param name="error">Mensaje de error si no cumple con las reglas.</param>
    /// <returns><c>True</c> si se considera que el trámite es válido, <c>False</c> caso contrario.</returns>
    bool Validar(Tramite tramite, out string error);
    #endregion
}