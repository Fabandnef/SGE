using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Validadores;

public class ExpedienteValidador
{
    public bool Validar(Expediente expediente, out string mensajeError)
    {
        mensajeError = "";
        
        if (string.IsNullOrEmpty(expediente.Caratula)) {
            mensajeError = "La carátula no puede estar vacía.";
        }

        if (expediente.IDUsuarioActual <= 0) {
            mensajeError = "El ID debe ser válido (Entero mayor que 0).";
        }
        
        return mensajeError == "";
    }
}