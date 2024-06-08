namespace SGE.Aplicacion.Enumerativos;

/// <summary>
///     Enumerativo que representa los permisos de un usuario.
///     <list type="bullet">
///         <item>
///             <term>ExpedienteAlta</term>
///             <description>Permiso para dar de alta expedientes.</description>
///         </item>
///         <item>
///             <term>ExpedienteBaja</term>
///             <description>Permiso para dar de baja expedientes.</description>
///         </item>
///         <item>
///             <term>ExpedienteModificacion</term>
///             <description>Permiso para modificar expedientes.</description>
///         </item>
///         <item>
///             <term>TramiteAlta</term>
///             <description>Permiso para dar de alta trámites.</description>
///         </item>
///         <item>
///             <term>TramiteBaja</term>
///             <description>Permiso para dar de baja trámites.</description>
///         </item>
///         <item>
///             <term>TramiteModificacion</term>
///             <description>Permiso para modificar trámites.</description>
///         </item>
///     </list>
/// </summary>
public enum PermisoEnum
{
    AdminGeneral,
    ExpedienteAlta,
    ExpedienteBaja,
    ExpedienteModificacion,
    TramiteAlta,
    TramiteBaja,
    TramiteModificacion,
}