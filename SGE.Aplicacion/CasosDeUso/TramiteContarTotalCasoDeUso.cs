using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteContarTotalCasoDeUso(IRepositorioTramite repositorioTramite)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public int Ejecutar() => repositorioTramite.ContarTotal();
    #endregion
}