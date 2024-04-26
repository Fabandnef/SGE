using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Entidades;

public class Tramite
{
    static private int _ultimoId = 0;
    private        int _idUsuarioActual;

    public int             Id                          { get; private set; }
    public int             ExpedienteId                { get; set; }
    public EtiquetaTramite Etiqueta                    { get; set; }
    public string          Contenido                   { get; set; }
    public DateTime        FechaCreacion               { get; private set; }
    public DateTime        UltimaModificacion          { get; private set; }
    public int             IdUsuarioActual             { get; private set; }
    public int             IdUsuarioUltimaModificacion { get; private set; }
}