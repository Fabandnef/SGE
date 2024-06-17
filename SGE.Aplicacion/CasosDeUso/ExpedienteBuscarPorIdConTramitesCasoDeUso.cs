using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteBuscarPorIdConTramitesCasoDeUso(
    IRepositorioExpediente repositorioExpediente
)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public Expediente? Ejecutar(int idExpediente)
    {
        Expediente? expediente = repositorioExpediente.BuscarPorId(idExpediente);

        return expediente;
    }
    #endregion
}