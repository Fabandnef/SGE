using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Validadores;

public class TramiteValidador
{
    public bool ValidarTramite(Tramite tramite, out string mensajeError)
    {
        mensajeError = "";

        if (string.IsNullOrEmpty(tramite.Contenido)) {
            mensajeError += "El contenido de un trámite no puede estar vacío.\n";
        }

        return mensajeError == "";
    }
}