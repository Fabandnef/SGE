namespace SGE.Aplicacion.Excepciones;

/// <summary>
///     Excepción que se lanza cuando un usuario no tiene autorización para realizar una operación.
/// </summary>
public class AutorizacionException : Exception
{
    public AutorizacionException() { }

    public AutorizacionException(string mensaje) : base(mensaje) { }

    public AutorizacionException(string mensaje, Exception causa) : base(mensaje, causa) { }
}