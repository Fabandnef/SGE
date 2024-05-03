using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Entidades;

public class Tramite
{
    private int      _expedienteId;
    private DateTime _fechaCreacion;
    private int      _id;

    public int Id {
        get => _id;
        set {
            if (_id == 0) {
                _id = value;
            }
        }
    }

    public int    IdUsuarioUltimaModificacion { get; set; }
    public string Contenido                   { get; set; } = "";

    public EtiquetaTramite Etiqueta { get; set; }

    public int ExpedienteId {
        get => _expedienteId;
        set {
            if (_expedienteId == 0) {
                _expedienteId = value;
            }
        }
    }

    public DateTime FechaCreacion {
        get => _fechaCreacion;
        set {
            if (_fechaCreacion == DateTime.MinValue) {
                _fechaCreacion = value;
            }
        }
    }

    public DateTime UltimaModificacion { get; set; }

    public string FormatoLegible()
        => $"Id: {Id}\n"                                 +
           $"ExpedienteId: {ExpedienteId}\n"             +
           $"Etiqueta: {Etiqueta}\n"                     +
           $"Contenido: {Contenido}\n"                   +
           $"FechaCreacion: {FechaCreacion}\n"           +
           $"UltimaModificacion: {UltimaModificacion}\n" +
           $"IdUsuarioUltimaModificacion: {IdUsuarioUltimaModificacion}";

    public override string ToString()
        => $"{Id}\x1F"                 +
           $"{ExpedienteId}\x1F"       +
           $"{Etiqueta}\x1F"           +
           $"{Contenido}\x1F"          +
           $"{FechaCreacion}\x1F"      +
           $"{UltimaModificacion}\x1F" +
           $"{IdUsuarioUltimaModificacion}";
}