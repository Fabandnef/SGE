namespace SGE.Aplicacion.Excepciones;

public class ValidacionException : Exception
{
    public ValidacionException(){}
    
    public ValidacionException(string mensaje) : base(mensaje){}
    
    public ValidacionException(string mensaje, Exception causa) : base(mensaje, causa){}
}