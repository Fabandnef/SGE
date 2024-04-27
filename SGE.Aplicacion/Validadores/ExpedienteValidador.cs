using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Validadores;

public class ExpedienteValidador
{
    public bool ValidarExpediente(Expediente expediente, out string mensajeError)
    {
        mensajeError = "";
        if (string.IsNullOrEmpty(expediente.Caratula)) {
            mensajeError = "La carátula no puede estar vacía.";
            return false;
        }

        // TODO: Ver cómo retornar que el ID no es válido.
        // if (expediente.IdUsuarioActual <= 0) {
        //     mensajeError = "El ID debe ser válido (Entero mayor que 0).";
        //     return false;
        // }

        return true;
    }
}