using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteListarConTramitesCasoDeUso(
    IRepositorioExpediente repositorioExpediente,
    IRepositorioTramite    repositorioTramite
)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public List<Expediente> Ejecutar()
    {
        List<Expediente> expedientes            = repositorioExpediente.Listar();
        List<Expediente> expedientesConTramites = [];

        foreach (Expediente t in expedientes) {
            expedientesConTramites.Add(repositorioTramite.ObtenerTramitesPorExpediente(t));
        }

        return expedientesConTramites;
    }
    #endregion
}