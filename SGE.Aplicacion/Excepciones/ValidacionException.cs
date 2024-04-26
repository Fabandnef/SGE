namespace SGE.Aplicacion.Excepciones;

internal class ValidacionException : Exception
{
    public ValidacionException() { }
    public ValidacionException(string? message) : base(message) { }
    public ValidacionException(string? message, Exception? innerException) : base(message, innerException) { }
}