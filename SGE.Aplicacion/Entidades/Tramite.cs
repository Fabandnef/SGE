﻿using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.Entidades;

/// <summary>
///     Entidad base para todos los trámites del sistema, que se asocian a un expediente.
///     La construcción de un trámite se realiza a través de un constructor vacío y la asignación
///     de los valores de las propiedades autoimplementadas.
/// </summary>
public class Tramite : ITimestampable
{
    #region PROPIEDADES PUBLICAS -----------------------------------------------------------------------
    /// <summary>
    ///     El contenido del trámite, en formato de texto.
    /// </summary>
    public string? Contenido { get; set; }

    /// <summary>
    ///     Una etiqueta que describe el tipo de trámite, para facilitar su búsqueda y clasificación.
    ///     Los valores posibles están definidos en el enumerativo <see cref="EtiquetaTramite" />.
    /// </summary>
    public EtiquetaTramite Etiqueta { get; set; }

    /// <summary>
    ///     El identificador del expediente al que pertenece el trámite. Se asocia a un expediente
    ///     a través de este identificador. Solo se permite la asignación del valor al crear el trámite.
    /// </summary>
    public int ExpedienteId { get; set; }

    /// <summary>
    ///     Identificador único del trámite. Ya que el ID se calcula antes de ser insertado, se
    ///     permite la asignación del ID solo si el valor actual es null, o sea, si no fue asignado.
    ///     La funcionalidad final termina siendo la misma que si el ID fuera de solo lectura.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     El usuario que realizó la última modificación en el trámite.
    /// </summary>
    public int IdUsuarioUltimaModificacion { get; set; }

    public virtual Usuario UsuarioUltimaModificacion { get; set; } = null!;
    #endregion

    #region IMPLEMENTACIONES DE INTERFACES -------------------------------------------------------------
    #region ITimestampable
    /// <summary>
    ///     La fecha en la que se creó el trámite. Se permite la asignación de la fecha solo si el
    ///     valor actual es la fecha mínima, o sea, si no fue asignado. La funcionalidad final termina
    ///     siendo la misma que si fuera de solo lectura.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     La fecha en la que se realizó la última modificación en el trámite. La fecha se actualiza
    ///     automáticamente cada vez que se modifica el trámite.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
    #endregion
    #endregion

    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    /// <summary>
    ///     Devuelve una representación en formato de cadena de texto del trámite, con el valor de
    ///     todas sus propiedades de una forma legible.
    /// </summary>
    /// <returns>String representando todos los elementos del trámite</returns>
    public override string ToString()
        => $"Id: {Id}\n"                     +
           $"IdExpediente: {ExpedienteId}\n" +
           $"Etiqueta: {Etiqueta}\n"         +
           $"Contenido: {Contenido}\n"       +
           $"FechaCreacion: {CreatedAt}\n"   +
           $"UpdatedAt: {UpdatedAt}\n"       +
           $"IdUsuarioUltimaModificacion: {IdUsuarioUltimaModificacion}";
    #endregion
}