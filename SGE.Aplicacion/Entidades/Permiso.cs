using System.ComponentModel.DataAnnotations;
using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Entidades;

public class Permiso
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(32)]
    [StringLength(32)]
    public string Nombre { get; set; }
    
    public string Descripcion { get; set; }
    
    public virtual List<Usuario> Usuarios { get; set; } = [];
}