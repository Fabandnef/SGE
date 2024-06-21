using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteListarCasoDeUso(IRepositorioTramite repositorioTramite)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public List<Tramite> Ejecutar(int pagina) => repositorioTramite.ObtenerTramites(pagina);
    #endregion
}