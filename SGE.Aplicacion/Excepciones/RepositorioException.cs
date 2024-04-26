namespace SGE.Aplicacion.Excepciones;

public class RepositorioException : Exception
{
    public RepositorioException(){}
    
    public RepositorioException(string mensaje) : base(mensaje){}
    
    public RepositorioException(string mensaje, Exception causa) : base(mensaje, causa){}
}