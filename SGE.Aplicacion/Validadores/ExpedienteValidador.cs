using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Validadores;

public class ExpedienteValidador
{
    public bool Validar(Expediente expediente, out string mensajeError)
    {
        if (string.IsNullOrEmpty(expediente.Caratula))
        {
            mensajeError = "La carátula no puede estar vacía.";
            return false;
        }

        if (expediente.Id <= 0)
        {
            mensajeError = "El ID debe ser válido (Entero mayor que 0).";
            return false;
        }

        mensajeError = "";
        return true;
    }
}