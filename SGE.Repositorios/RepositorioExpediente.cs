using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces;

namespace SGE.Repositorios;

public class RepositorioExpediente : IExpedienteRepositorio
{
    public void AgregarExpediente(Expediente expediente)
    {
        throw new NotImplementedException();
    }

    public void EditarExpediente(Expediente expediente)
    {
        throw new NotImplementedException();
    }

    public void EliminarExpediente(Expediente expediente)
    {
        throw new NotImplementedException();
    }

    public Expediente ObtenerExpedientePorId(int id) => throw new NotImplementedException();

    public IEnumerable<Expediente> ObtenerExpedientes() => throw new NotImplementedException();
}