using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Validadores;

namespace SGE.Aplicacion.Validadores;

/// <summary>
///     Clase que implementa la interfaz IExpedienteValidador. Permite validar un expediente, o
///     arrojar un mensaje de error si no cumple con las reglas preestablecidas.
/// </summary>
public class ExpedienteValidador : IExpedienteValidador
{
    #region IMPLEMENTACIONES DE INTERFACES -------------------------------------------------------------
    #region IExpedienteValidador
    /// <inheritdoc />
    public bool Validar(Expediente expediente, out string error)
    {
        error = "";

        if (string.IsNullOrWhiteSpace(expediente.Caratula)) {
            error += "La carátula no puede estar vacía.";
        }

        return string.IsNullOrWhiteSpace(error);
    }
    #endregion
    #endregion
}