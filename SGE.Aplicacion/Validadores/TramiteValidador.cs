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

        if (string.IsNullOrWhiteSpace(tramite.Contenido)) {
            error += "El contenido de un trámite no puede estar vacío.";
        }

        if (tramite.ExpedienteId > 0) {
            return string.IsNullOrWhiteSpace(error);
        }

        if (!string.IsNullOrWhiteSpace(error)) {
            error += "\n";
        }

        error += "El trámite debe estar asociado a un expediente.";

        return string.IsNullOrWhiteSpace(error);
    }
    #endregion
    #endregion
}