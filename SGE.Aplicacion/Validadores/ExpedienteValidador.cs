using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Excepciones;

namespace SGE.Aplicacion.Validadores;

public class ExpedienteValidador
{
    public bool Validar(Expediente expediente)
    {
        if (string.IsNullOrEmpty(expediente.Caratula)) {
            throw new ValidacionException("La carátula no puede estar vacía.");
        }

        if (expediente.ID <= 0) {
            throw new ValidacionException("El ID debe ser válido (Entero mayor que 0).");
        }

        return true;
    }
}