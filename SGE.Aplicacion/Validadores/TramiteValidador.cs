using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Validadores;

public class TramiteValidador
{
    public bool Validar(Tramite tramite, out string mensajeError)
    {
        mensajeError = "";

        if (string.IsNullOrEmpty(tramite.Contenido)) {
            mensajeError = "El contenido de un trámite no puede estar vacío.";
        }

        if (tramite.IdUsuarioActual <= 0) {
            mensajeError = "El ID debe ser válido (Entero mayor que 0).";
        }

        return mensajeError == "";
    }
}