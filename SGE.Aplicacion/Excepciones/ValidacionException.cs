namespace SGE.Aplicacion.Excepciones;

/// <summary>
///     Excepción que se lanza cuando ocurre un error de validación.
/// </summary>
public class ValidacionException : Exception
{
    public ValidacionException() { }

    public ValidacionException(string mensaje) : base(mensaje) { }

    public ValidacionException(string mensaje, Exception causa) : base(mensaje, causa) { }
}