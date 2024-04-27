using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteListarCasoDeUso(IExpedienteRepositorio repositorio)
{
    public IEnumerable<Expediente> Ejecutar() => repositorio.ExpedienteListar();
}