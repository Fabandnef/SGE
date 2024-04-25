using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Validadores;

public class TramiteValidador
{
    public bool Validar(Tramite tramite, out string mensajeError)
    {
        if (string.IsNullOrEmpty(tramite.Contenido))
        {
            mensajeError = "El contenido de un trámite no puede estar vacío.";
            return false;
        }

        if (tramite.ID <= 0)
        {
            mensajeError = "El ID debe ser válido (Entero mayor que 0).";
            return false;
        }

        mensajeError = "";
        return true;
    }
}