namespace SGE.Aplicacion.Enumerativos;

/// <summary>
///     Enumerativo que representa las etiquetas de un trámite.
///     <list type="bullet">
///         <item>
///             <term>EscritoPresentado</term>
///             <description>Escrito presentado.</description>
///         </item>
///         <item>
///             <term>PaseAEstudio</term>
///             <description>Pase a estudio.</description>
///         </item>
///         <item>
///             <term>Despacho</term>
///             <description>Despacho.</description>
///         </item>
///         <item>
///             <term>Resolucion</term>
///             <description>Resolución.</description>
///         </item>
///         <item>
///             <term>Notificacion</term>
///             <description>Notificación.</description>
///         </item>
///         <item>
///             <term>PaseAlArchivo</term>
///             <description>Pase al archivo.</description>
///         </item>
///     </list>
/// </summary>
public enum EtiquetaTramite
{
    EscritoPresentado,
    PaseAEstudio,
    Despacho,
    Resolucion,
    Notificacion,
    PaseAlArchivo,
}