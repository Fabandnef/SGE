using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteContarTotalCasoDeUso(IRepositorioTramite repositorioTramite)
{
    public int Ejecutar() => repositorioTramite.ContarTotal();
}