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
        
        if ((tramite.FechaCreacion != DateTime.MinValue) && tramite.Id.Equals(0)) {
            error += "No se puede dar de alta un trámite que ya tenga asignado un ID.\n";
        }
        
        if (tramite.IdExpediente <= 0) {
            error += "El trámite debe estar asociado a un expediente.\n";
        }

        return string.IsNullOrEmpty(error);
    }
    #endregion
    #endregion
}