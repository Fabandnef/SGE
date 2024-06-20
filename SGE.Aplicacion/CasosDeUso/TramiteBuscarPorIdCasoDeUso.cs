using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteBuscarPorIdCasoDeUso(IRepositorioTramite repositorioTramite)
{
    public Tramite? Ejecutar(int idTramite) => repositorioTramite.ObtenerPorId(idTramite);
}