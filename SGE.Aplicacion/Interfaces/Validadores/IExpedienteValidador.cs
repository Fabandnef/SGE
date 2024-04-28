using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Interfaces.Validadores;

public interface IExpedienteValidador
{
    bool Validar(Expediente expediente, out string error);
}