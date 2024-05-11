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

        if (string.IsNullOrEmpty(expediente.Caratula)) {
            error += "La carátula no puede estar vacía.\n";
        }

        return string.IsNullOrEmpty(error);
    }
    #endregion
    #endregion
}