using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Excepciones;

namespace SGE.Aplicacion.Validadores;

public class TramiteValidador
{
    public bool Validar(Tramite tramite)
    {
        if (string.IsNullOrEmpty(tramite.Contenido)) {
            throw new ValidacionException("El contenido no puede estar vacío.");
        }

        if (tramite.ID <= 0) {
            throw new ValidacionException("El ID debe ser válido (Entero mayor que 0).");
        }
        
        return true;
    }
}