using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Validadores;

namespace SGE.Aplicacion.Validadores;

public class ExpedienteValidador : IExpedienteValidador
{
    public bool Validar(Expediente expediente, out string error)
    {
        error = "";

        if (string.IsNullOrEmpty(expediente.Caratula)) {
            error += "La carátula no puede estar vacía.\n";
        }

        return string.IsNullOrEmpty(error);
    }
}