namespace SGE.Aplicacion.Enumerativos;

public enum Permiso
{
    /// <summary>
    ///  El usuario puede realizar altas de expedientes
    /// </summary>
    ExpedienteAlta,
    
    /// <summary>
    /// El usuario puede realizar bajas de expedientes
    /// </summary>
    ExpedienteBaja,
    
    /// <summary>
    /// El usuario puede realizar modificaciones de expedientes
    /// </summary>
    ExpedienteModificacion,
    
    /// <summary>
    /// El usuario puede realizar altas de trámites
    /// </summary>
    TramiteAlta,
    
    /// <summary>
    /// El usuario puede realizar bajas de trámites
    /// </summary>
    TramiteBaja,
    
    /// <summary>
    /// El usuario puede realizar modificaciones de trámites
    /// </summary>
    TramiteModificacion
}