using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Entidades;

public class Tramite
{
    private static int _ultimoID = 0;
    
    public int ID { get; private set; }
    public int ExpedienteId { get; set; }
    public EtiquetaTramite Etiqueta { get; set; }
    public string Contenido { get; set; }
    public DateTime FechaCreacion { get; private set; }
    public DateTime UltimaModificacion { get; private set; }
    public int IDUsuarioUltimaModificacion { get; private set; }
    
}