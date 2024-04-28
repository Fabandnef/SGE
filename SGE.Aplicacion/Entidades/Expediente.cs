using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Entidades;

public class Expediente
{
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

    public EstadoExpediente Estado   { get; set; }
    public string           Caratula { get; set; } = "";

    public DateTime FechaCreacion {
        get => _fechaCreacion;
        set {
            if (_fechaCreacion == DateTime.MinValue) {
                _fechaCreacion = value;
            }
        }
    }

    public DateTime UltimaModificacion          { get; set; }
    public int      IdUsuarioUltimaModificacion { get; set; }

    // TODO: Consultar si esto está bien.
    public IEnumerable<Tramite>?
        Tramites { get; set; } // * IMPORTANT: Esto no sale en el PDF, pero lo pongo porque si no no tiene sentido.

    public override string ToString()
        => $"{Id}\x1F"                          +
           $"{Estado}\x1F"                      +
           $"{Caratula}\x1F"                    +
           $"{FechaCreacion}\x1F"               +
           $"{UltimaModificacion}\x1F"          +
           $"{IdUsuarioUltimaModificacion}";

    public string FormatoLegible()
        => $"Id: {Id}\n"                                                   +
           $"Estado: {Estado}\n"                                           +
           $"Caratula: {Caratula}\n"                                       +
           $"FechaCreacion: {FechaCreacion}\n"                             +
           $"UltimaModificacion: {UltimaModificacion}\n"                   +
           $"IdUsuarioUltimaModificacion: {IdUsuarioUltimaModificacion}\n";
}