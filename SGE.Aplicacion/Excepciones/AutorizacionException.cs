namespace SGE.Aplicacion.Excepciones;

public class AutorizacionException : Exception
{
    public AutorizacionException(){}
    
    public AutorizacionException(string mensaje) : base(mensaje){}
    
    public AutorizacionException(string mensaje, Exception causa) : base(mensaje, causa){}
}