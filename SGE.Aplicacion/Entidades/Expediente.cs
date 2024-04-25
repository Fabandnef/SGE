using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Entidades;

public class Expediente
{
    private static int _ultimoID = 0;
    
    public int ID { get; private set; }
    public string Caratula { get; set; }
    public DateTime FechaCreacion { get; private set; }
    public DateTime UltimaModificacion { get; private set; }
    public int IDUsuarioUltimaModificacion { get; private set; }
    public EstadoExpediente Estado { get; set; }




}