using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Entidades;

public class Expediente
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
    public int      IdUsuarioActual             { get; set; }

    // TODO: Consultar si esto está bien.
    public IEnumerable<Tramite>?
        Tramites { get; set; } // * IMPORTANT: Esto no sale en el PDF, pero lo pongo porque si no no tiene sentido.
    
    public override string ToString()
    {
        string st = Id.ToString()               + '\x1F' +
                    Caratula                    + '\x1F' +
                    FechaCreacion               + '\x1F' +
                    UltimaModificacion          + '\x1F' +
                    IdUsuarioUltimaModificacion + '\x1F' +
                    Estado                      + '\x1F';
        /*  TRAMITES                    + '\x1F'; */ // Cómo quedará esto?

        int contador = 1;

        if (Tramites != null) {
            foreach (Tramite tramite in Tramites) {
                st += $"\n------\nTramite {contador++}:\n{tramite}";
            }
        }

        return st;
    }

    public string FormatoLegible()
    {
        string st = "Id: "                          + Id.ToString()               + '\n' +
                    "Caratula: "                    + Caratula                    + '\n' +
                    "FechaCreacion: "               + FechaCreacion               + '\n' +
                    "UltimaModificacion: "          + UltimaModificacion          + '\n' +
                    "IdUsuarioUltimaModificacion: " + IdUsuarioUltimaModificacion + '\n' +
                    "Estado: "                      + Estado                      + '\n';
                /*  TRAMITES                        + '\n'; */ // Cómo quedará esto?

        int contador = 1;

        if (Tramites != null) {
            foreach (Tramite tramite in Tramites) {
                st += $"\n------\nTramite {contador++}:\n{tramite}";
            }
        }

        return st;
    }
    
}