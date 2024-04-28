using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Interfaces.Validadores;

public interface ITramiteValidador
{
    bool Validar(Tramite tramite, out string error);
}