using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteBuscarPorIdCasoDeUso(IRepositorioExpediente repositorioExpediente)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public Expediente? Ejecutar(int idExpediente) => repositorioExpediente.BuscarPorId(idExpediente);
    #endregion
}