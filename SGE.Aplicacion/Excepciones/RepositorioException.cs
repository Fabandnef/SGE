namespace SGE.Aplicacion.Excepciones;

internal class RepositorioException : Exception
{
    public RepositorioException() { }
    public RepositorioException(string? message) : base(message) { }
    public RepositorioException(string? message, Exception? innerException) : base(message, innerException) { }
}