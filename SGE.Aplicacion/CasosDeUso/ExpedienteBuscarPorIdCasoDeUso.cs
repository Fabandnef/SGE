using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteBuscarPorIdCasoDeUso(IExpedienteRepositorio repositorio)
{
    public Expediente? Ejecutar(int idExpediente) => repositorio.ExpedienteBuscarPorId(idExpediente);
}