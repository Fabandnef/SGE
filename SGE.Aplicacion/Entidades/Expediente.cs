using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Entidades;

/// <summary>
/// Entidad base para todos los expedientes del sistema.
/// La construcción de un expediente se realiza a través de un constructor vacío y la asignación
/// de los valores de las propiedades autoimplementadas.
/// </summary>
public class Expediente
{
    private DateTime       _fechaCreacion;
    private int            _id;

    /// <summary>
    /// Identificador único del expediente. Ya que el ID se calcula antes de ser insertado, se
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
    public int IdUsuarioUltimaModificacion { get; set; }

    /// <summary>
    /// La carátula del expediente, que es un resumen del contenido del mismo.
    /// </summary>
    public string?           Caratula { get; set; }
    
    /// <summary>
    /// El estado actual del expediente, que puede ser uno de los valores definidos en el enumerativo
    /// <see cref="EstadoExpediente"/>.
    /// </summary>
    public EstadoExpediente Estado   { get; set; }

    /// <summary>
    /// La fecha de creación del expediente. Se permite la asignación de la fecha de creación solo
    /// si el valor actual es la fecha mínima, o sea, si no fue asignado. La funcionalidad final
    /// termina siendo la misma que si fuera de solo lectura.
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
    /// Lista de trámites asociados al expediente. Se inicializa como una lista vacía.
    /// </summary>
    public List<Tramite> Tramites { get; set; } = [];

    /// <summary>
    /// La fecha de la última modificación del expediente. Se basa en la fecha de creación si no
    /// se ha modificado. Esta fecha también se actualiza cada vez que se agrega, edita o elimina
    /// un trámite del expediente, si es que dicha acción genera una actualización del
    /// estado del expediente.
    /// </summary>
    public DateTime UltimaModificacion { get; set; }

    /// <summary>
    /// Devuelve una representación en formato de cadena de texto del expediente, con el valor de
    /// todas sus propiedades de una forma legible.
    /// </summary>
    /// <returns>String representando todos los elementos del trámite</returns>
    public override string ToString()
    {
        string st = $"Id: {Id}\n"                                 +
                    $"Estado: {Estado}\n"                         +
                    $"Caratula: {Caratula}\n"                     +
                    $"FechaCreacion: {FechaCreacion}\n"           +
                    $"UltimaModificacion: {UltimaModificacion}\n" +
                    $"IdUsuarioUltimaModificacion: {IdUsuarioUltimaModificacion}\n";

        // Si no hay trámites, devuelvo solo el expediente.
        if (Tramites.Count == 0) {
            return st;
        }

        // Si hay trámites, los agrego al string.
        st += "--------------\n" +
              "Tramites:\n";
        foreach (Tramite tramite in Tramites) {
            st += "--------------\n";
            st += tramite.ToString();
        }
        
        st += "--------------\n";

        return st;
    }
    
    /// <summary>
    /// Compara el expediente con otro objeto para determinar si son iguales. Dos expedientes son iguales si
    /// pertenecen a la misma clase y tienen el mismo ID. En caso de que el objeto a comparar sea correctamente
    /// convertido a un expediente, se realiza la comparación de los IDs mediante el método
    /// <see cref="Equals(Expediente)"/>.
    /// </summary>
    /// <param name="obj">El objeto a comparar con el expediente actual</param>
    /// <returns><c>True</c> si los expedientes son iguales, <c>False</c> en caso contrario.</returns>
    public override bool Equals(object? obj) => obj is Expediente expediente 
                                             && Equals(expediente);

    /// <summary>
    /// Compara dos expedientes para determinar si son iguales mediante la comparación de sus IDs.
    /// </summary>
    /// <param name="other">El expediente contra el que se compara el actual</param>
    /// <returns><c>True</c> si los expedientes son iguales, <c>False</c> en caso contrario.</returns>
    private bool Equals(Expediente other) => _id == other._id;
}