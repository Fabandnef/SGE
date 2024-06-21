namespace SGE.Aplicacion.Entidades;

public class Permiso
{
    #region PROPIEDADES PUBLICAS -----------------------------------------------------------------------
    public required string Descripcion { get; set; }
    public          int    Id          { get; set; }

    public required string Nombre { get; set; }

    public virtual List<Usuario> Usuarios { get; set; } = [];
    #endregion
}