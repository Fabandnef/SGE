using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Entidades;

/// <summary>
/// Entidad base para todos los trámites del sistema, que se asocian a un expediente.
/// La construcción de un trámite se realiza a través de un constructor vacío y la asignación
/// de los valores de las propiedades autoimplementadas.
/// </summary>
public class Tramite
{
    private int      _expedienteId;
    private DateTime _fechaCreacion;
    private int      _id;

    /// <summary>
    /// Identificador único del trámite. Ya que el ID se calcula antes de ser insertado, se
    /// permite la asignación del ID solo si el valor actual es 0, o sea, si no fue asignado.
    /// La funcionalidad final termina siendo la misma que si el ID fuera de solo lectura.
    /// </summary>
    public int Id {
        get => _id;
        set {
            if (_id == 0) {
                _id = value;
            }
        }
    }

    /// <summary>
    /// El usuario que realizó la última modificación en el trámite.
    /// </summary>
    public int    IdUsuarioUltimaModificacion { get; set; }
    
    /// <summary>
    /// El contenido del trámite, en formato de texto.
    /// </summary>
    public string? Contenido                   { get; set; }

    /// <summary>
    /// Una etiqueta que describe el tipo de trámite, para facilitar su búsqueda y clasificación.
    /// Los valores posibles están definidos en el enumerativo <see cref="EtiquetaTramite"/>.
    /// </summary>
    public EtiquetaTramite Etiqueta { get; set; }

    /// <summary>
    /// El identificador del expediente al que pertenece el trámite. Se permite la asignación
    /// del ID solo si el valor actual es 0, o sea, si no fue asignado. La funcionalidad final
    /// termina siendo la misma que si fuera de solo lectura.
    /// </summary>
    public int ExpedienteId {
        get => _expedienteId;
        set {
            if (_expedienteId == 0) {
                _expedienteId = value;
            }
        }
    }

    /// <summary>
    /// La fecha en la que se creó el trámite. Se permite la asignación de la fecha solo si el
    /// valor actual es la fecha mínima, o sea, si no fue asignado. La funcionalidad final termina
    /// siendo la misma que si fuera de solo lectura.
    /// </summary>
    public DateTime FechaCreacion {
        get => _fechaCreacion;
        set {
            if (_fechaCreacion == DateTime.MinValue) {
                _fechaCreacion = value;
            }
        }
    }

    /// <summary>
    /// La fecha en la que se realizó la última modificación en el trámite. La fecha se actualiza
    /// automáticamente cada vez que se modifica el trámite.
    /// </summary>
    public DateTime UltimaModificacion { get; set; }

    /// <summary>
    /// Devuelve una representación en formato de cadena de texto del trámite, con el valor de
    /// todas sus propiedades de una forma legible.
    /// </summary>
    /// <returns>String representando todos los elementos del trámite</returns>
    public override string ToString()
        => $"Id: {Id}\n"                                 +
           $"ExpedienteId: {ExpedienteId}\n"             +
           $"Etiqueta: {Etiqueta}\n"                     +
           $"Contenido: {Contenido}\n"                   +
           $"FechaCreacion: {FechaCreacion}\n"           +
           $"UltimaModificacion: {UltimaModificacion}\n" +
           $"IdUsuarioUltimaModificacion: {IdUsuarioUltimaModificacion}";

    /// <summary>
    /// Compara el trámite con otro objeto para determinar si son iguales. Dos trámites son iguales si pertenecen
    /// a la misma clase y tienen el mismo ID. En caso de que el objeto a comparar sea correctamente
    /// convertido a un trámite, se realiza la comparación de los IDs mediante el método
    /// <see cref="Equals(Tramite)"/>.
    /// </summary>
    /// <param name="obj">El objeto a comparar con el trámite actual</param>
    /// <returns><c>True</c> si los trámites son iguales, <c>False</c> en caso contrario.</returns>
    public override bool Equals(object? obj) => obj is Tramite tramite 
                                             && Equals(tramite);

    /// <summary>
    /// Compara dos trámites para determinar si son iguales mediante la comparación de sus IDs.
    /// </summary>
    /// <param name="other">El trámite contra el que se compara el actual</param>
    /// <returns><c>True</c> si los trámites son iguales, <c>False</c> en caso contrario.</returns>
    private bool Equals(Tramite other) => _id == other._id;
}