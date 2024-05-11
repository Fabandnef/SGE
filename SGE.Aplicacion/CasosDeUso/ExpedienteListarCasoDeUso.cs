using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteListarCasoDeUso(IExpedienteRepositorio expedienteRepositorio)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public List<Expediente> Ejecutar() => expedienteRepositorio.Listar();
    #endregion
}