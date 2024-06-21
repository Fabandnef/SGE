namespace SGE.Aplicacion.Entidades;

public class Permiso
{
    public int Id { get; set; }
    
    public required string Nombre { get; set; }
    
    public required string Descripcion { get; set; }
    
    public virtual List<Usuario> Usuarios { get; set; } = [];
}