using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteBuscarPorIdConTramitesCasoDeUso(
    IRepositorioExpediente repositorioExpediente,
    IRepositorioTramite    repositorioTramite
)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public Expediente? Ejecutar(int idExpediente)
    {
        Expediente? expediente = repositorioExpediente.BuscarPorId(idExpediente);

        if (expediente != null) {
//            expediente = repositorioTramite.ObtenerTramitesPorExpediente(expediente);
        }

        return expediente;
    }
    #endregion
}