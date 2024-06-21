using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class PermisoListarCasoDeUso(IRepositorioPermiso repositorioPermiso)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public List<Permiso> Ejecutar() => repositorioPermiso.GetPermisos();
    #endregion
}