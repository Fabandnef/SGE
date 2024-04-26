using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Entidades;

public class Expediente
{
    static private int _ultimoId = 0;

    public int              Id                          { get; private set; }
    public string           Caratula                    { get; set; }
    public DateTime         FechaCreacion               { get; private set; }
    public DateTime         UltimaModificacion          { get; private set; }
    public int              IDUsuarioActual             { get; private set; }
    public int              IDUsuarioUltimaModificacion { get; private set; }
    public EstadoExpediente Estado                      { get; set; }
}