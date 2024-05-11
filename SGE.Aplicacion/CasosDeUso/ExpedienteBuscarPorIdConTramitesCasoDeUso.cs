using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteBuscarPorIdConTramitesCasoDeUso(
    IExpedienteRepositorio expedienteRepositorio,
    ITramiteRepositorio    tramiteRepositorio
)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public Expediente? Ejecutar(int idExpediente)
    {
        Expediente? expediente = expedienteRepositorio.BuscarPorId(idExpediente);

        if (expediente != null) {
            expediente = tramiteRepositorio.ObtenerTramitesPorExpediente(expediente);
        }

        return expediente;
    }
    #endregion
}