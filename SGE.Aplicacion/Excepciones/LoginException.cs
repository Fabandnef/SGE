using System.Runtime.Serialization;

namespace SGE.Aplicacion.Excepciones;

public class LoginException : Exception
{
    public LoginException() { }
    public LoginException(string? message) : base(message) { }
    public LoginException(string? message, Exception? innerException) : base(message, innerException) { }
}