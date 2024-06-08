using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteConsultaPorEtiquetaCasoDeUso(IRepositorioTramite repositorioTramite)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public List<Tramite> Ejecutar(EtiquetaTramite etiquetaTramite)
        => repositorioTramite.ObtenerPorEtiqueta(etiquetaTramite);
    #endregion
}