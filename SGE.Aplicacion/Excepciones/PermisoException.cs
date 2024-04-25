namespace SGE.Aplicacion.Excepciones;

public class PermisoException : Exception
{
    public PermisoException(){}
    
    public PermisoException(string mensaje) : base(mensaje){}
    
    public PermisoException(string mensaje, Exception causa) : base(mensaje, causa){}
}