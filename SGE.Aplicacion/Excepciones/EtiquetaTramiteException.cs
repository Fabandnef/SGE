namespace SGE.Aplicacion.Excepciones;

public class EtiquetaTramiteException : Exception
{
    public EtiquetaTramiteException(){}
    
    public EtiquetaTramiteException(string mensaje) : base(mensaje){}
    
    public EtiquetaTramiteException(string mensaje, Exception causa) : base(mensaje, causa){}
}