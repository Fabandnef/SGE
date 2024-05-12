using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteConsultaPorEtiquetaCasoDeUso(ITramiteRepositorio tramiteRepositorio)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public List<Tramite> Ejecutar(EtiquetaTramite etiquetaTramite)
        => tramiteRepositorio.ObtenerPorEtiqueta(etiquetaTramite);
    #endregion
}