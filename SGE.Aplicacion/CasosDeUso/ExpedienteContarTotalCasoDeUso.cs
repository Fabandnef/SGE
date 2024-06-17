using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteContarTotalCasoDeUso(IRepositorioExpediente repositorioExpediente)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public int Ejecutar() => repositorioExpediente.ContarTotal();
    #endregion
}