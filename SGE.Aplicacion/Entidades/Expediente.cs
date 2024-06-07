using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.Entidades;

/// <summary>
///     Entidad base para todos los expedientes del sistema.
///     La construcción de un expediente se realiza a través de un constructor vacío y la asignación
///     de los valores de las propiedades autoimplementadas.
/// </summary>
public class Expediente : ITimestampable
{
    #region PROPIEDADES PUBLICAS -----------------------------------------------------------------------
    /// <summary>
    ///     La carátula del expediente, que es un resumen del contenido del mismo.
    /// </summary>
    public string? Caratula { get; set; }

    /// <summary>
    ///     El estado actual del expediente, que puede ser uno de los valores definidos en el enumerativo
    ///     <see cref="EstadoExpediente" />.
    /// </summary>
    public EstadoExpediente Estado { get; set; }

    /// <summary>
    ///     El usuario que realizó la última modificación en el trámite.
    /// </summary>
    public int IdUsuarioUltimaModificacion { get; set; }

    public Usuario UsuarioUltimaModificacion { get; set; } = null!;

    /// <summary>
    ///     Lista de trámites asociados al expediente. Se inicializa como una lista vacía.
    /// </summary>
    public List<Tramite> Tramites { get; set; } = [];

    /// <summary>
    ///     La fecha de la última modificación del expediente. Se basa en la fecha de creación si no
    ///     se ha modificado. Esta fecha también se actualiza cada vez que se agrega, edita o elimina
    ///     un trámite del expediente, si es que dicha acción genera una actualización del
    ///     estado del expediente.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    ///     La fecha de creación del expediente. Se permite la asignación de la fecha de creación solo
    ///     si el valor actual es la fecha mínima, o sea, si no fue asignado. La funcionalidad final
    ///     termina siendo la misma que si fuera de solo lectura.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     Identificador único del expediente. Ya que el ID se calcula antes de ser insertado, se
    ///     permite la asignación del ID solo si el valor actual es 0, o sea, si no fue asignado.
    ///     La funcionalidad final termina siendo la misma que si el ID fuera de solo lectura.
    /// </summary>
    public int Id { get; set; }
    #endregion

    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    /// <summary>
    ///     Devuelve una representación en formato de cadena de texto del expediente, con el valor de
    ///     todas sus propiedades de una forma legible.
    /// </summary>
    /// <returns>String representando todos los elementos del expediente</returns>
    public override string ToString()
    {
        string st = $"Id: {Id}\n"                                 +
                    $"Estado: {Estado}\n"                         +
                    $"Caratula: {Caratula}\n"                     +
                    $"FechaCreacion: {CreatedAt}\n"           +
                    $"UpdatedAt: {UpdatedAt}\n" +
                    $"IdUsuarioUltimaModificacion: {IdUsuarioUltimaModificacion}";

        // Si no hay trámites, devuelvo solo el expediente.
        if (Tramites.Count == 0) {
            return st;
        }

        // Si hay trámites, los agrego al string.
        st += "\n--------------\n" +
              $"Tramites del expediente {Id}:";

        foreach (Tramite tramite in Tramites) {
            st += "\n--------------\n";
            st += tramite.ToString();
        }

        st += "\n--------------";

        return st;
    }
    #endregion

}