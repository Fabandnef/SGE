using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Entidades;

public class Tramite
{
    private int      _id;
    private DateTime _fechaCreacion;

    public int Id {
        get => _id;
        set {
            if (_id == 0) {
                _id = value;
            }
        }
    }
    public int             ExpedienteId                { get; init; }
    public EtiquetaTramite Etiqueta                    { get; init; }
    public string          Contenido                   { get; init; } = "";

    public DateTime FechaCreacion {
        get => _fechaCreacion;
        set {
            if (_fechaCreacion == DateTime.MinValue) {
                _fechaCreacion = value;
            }
        }
    }

    public DateTime        UltimaModificacion          { get; set; }
    public int             IdUsuarioUltimaModificacion { get; set; }

    public override string ToString() => $"{Id}\x1F{ExpedienteId}\x1F{Etiqueta}\x1F{Contenido}\x1F{FechaCreacion}\x1F{UltimaModificacion}\x1F{IdUsuarioUltimaModificacion}";
}