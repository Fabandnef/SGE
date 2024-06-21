using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Interfaces.Repositorios;

public interface IRepositorioPermiso
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public void Agregar(Permiso permiso);

    public void Eliminar(Permiso permiso);

    public List<Permiso> GetPermisos();

    public void Modificar(Permiso permiso);
    #endregion
}