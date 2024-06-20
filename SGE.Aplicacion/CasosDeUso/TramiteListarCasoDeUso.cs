using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteListarCasoDeUso(IRepositorioTramite repositorioTramite)
{
    public List<Tramite> Ejecutar(int pagina) => repositorioTramite.ObtenerTramites(pagina);
}