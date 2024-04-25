using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Interfaces;

public interface IExpedienteRepositorio
{
    void AgregarExpediente(Expediente expediente);
}