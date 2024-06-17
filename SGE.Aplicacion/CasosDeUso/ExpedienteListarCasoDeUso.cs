using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteListarCasoDeUso(IRepositorioExpediente repositorioExpediente)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public List<Expediente> Ejecutar(int page) => repositorioExpediente.Listar(page);
    #endregion
}