using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Validadores;

namespace SGE.Aplicacion.Validadores;

public class TramiteValidador : ITramiteValidador
{
    public bool Validar(Tramite tramite, out string error)
    {
        error = "";

        if (string.IsNullOrEmpty(tramite.Contenido)) {
            error += "El contenido de un trámite no puede estar vacío.\n";
        }

        return !string.IsNullOrEmpty(error);
    }
}