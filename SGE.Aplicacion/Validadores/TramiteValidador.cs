using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Validadores;

namespace SGE.Aplicacion.Validadores;

/// <summary>
///     Clase que implementa la interfaz ITramiteValidador. Permite validar un trámite, o
///     arrojar un mensaje de error si no cumple con las reglas preestablecidas.
/// </summary>
public class TramiteValidador : ITramiteValidador
{
    #region IMPLEMENTACIONES DE INTERFACES -------------------------------------------------------------
    #region ITramiteValidador
    /// <inheritdoc />
    public bool Validar(Tramite tramite, out string error)
    {
        error = "";

        if (string.IsNullOrEmpty(tramite.Contenido)) {
            error += "El contenido de un trámite no puede estar vacío.\n";
        }

        return string.IsNullOrEmpty(error);
    }
    #endregion
    #endregion
}