using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class PermisoListarCasoDeUso(IRepositorioPermiso repositorioPermiso)
{
    public List<Permiso> Ejecutar() => repositorioPermiso.GetPermisos();
}