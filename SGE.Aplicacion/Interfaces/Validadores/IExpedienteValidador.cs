using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Interfaces.Validadores;

/// <summary>
///     Interfaz que define los métodos para validar un expediente.
/// </summary>
public interface IExpedienteValidador
{
    /// <summary>
    ///     Valida un expediente.
    /// </summary>
    /// <param name="expediente">Expediente a validar.</param>
    /// <param name="error">Mensaje de error si no cumple con las reglas.</param>
    /// <returns><c>True</c> si se considera que el expediente es válido, <c>False</c> caso contrario.</returns>
    bool Validar(Expediente expediente, out string error);
}