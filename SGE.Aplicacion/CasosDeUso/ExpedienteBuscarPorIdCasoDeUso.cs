using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteBuscarPorIdCasoDeUso(IExpedienteRepositorio expedienteRepositorio)
{
    public Expediente? Ejecutar(int idExpediente) => expedienteRepositorio.BuscarPorId(idExpediente);
}