using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.Entidades;

/// <summary>
///     Representa un usuario del sistema.
/// </summary>
public class Usuario : ITimestampable
{
    #region PROPIEDADES PUBLICAS -----------------------------------------------------------------------
    /// <summary>
    ///     Identificador del usuario.
    /// </summary>
    public int Id { get; set; }
    
    public required string Nombre { get; set; }
    
    public required string Apellido { get; set; }
    
    public string NombreCompleto => $"{Nombre} {Apellido}";
    
    public required string Email { get; set; }
    
    public required string Password { get; set; }
    
    public List<Permiso> Permisos { get; set; } = [];
    
    public bool IsAdmin => Permisos.Any(p => p.Nombre == PermisoEnum.AdminGeneral.ToString()) || (Id == 1);

    public DateTime  CreatedAt { get; set; }
    
    public DateTime  UpdatedAt { get; set; }
    #endregion
}