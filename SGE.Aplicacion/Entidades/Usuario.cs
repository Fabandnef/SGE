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
    
    public string Nombre { get; set; }
    
    public string Apellido { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public virtual List<Permiso> Permisos { get; set; } = [];
    #endregion

    public DateTime  CreatedAt { get; set; }
    public DateTime  UpdatedAt { get; set; }
}