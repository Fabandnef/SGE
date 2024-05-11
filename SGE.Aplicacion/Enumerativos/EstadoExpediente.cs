namespace SGE.Aplicacion.Enumerativos;

/// <summary>
///     Enumerativo que representa el estado de un expediente.
///     <list type="bullet">
///         <item>
///             <term>RecienIniciado</term>
///             <description>Expediente recién iniciado.</description>
///         </item>
///         <item>
///             <term>ParaResolver</term>
///             <description>Expediente en espera de resolución.</description>
///         </item>
///         <item>
///             <term>ConResolucion</term>
///             <description>Expediente con resolución.</description>
///         </item>
///         <item>
///             <term>EnNotificacion</term>
///             <description>Expediente en proceso de notificación.</description>
///         </item>
///         <item>
///             <term>Finalizado</term>
///             <description>Expediente finalizado.</description>
///         </item>
///     </list>
/// </summary>
public enum EstadoExpediente
{
    RecienIniciado,
    ParaResolver,
    ConResolucion,
    EnNotificacion,
    Finalizado,
}