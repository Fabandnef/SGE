using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteListarCasoDeUso(IExpedienteRepositorio expedienteRepositorio)
{
    public IEnumerable<Expediente> Ejecutar() => expedienteRepositorio.Listar();
}