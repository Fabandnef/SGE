using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Interfaces.Repositorios;

public interface ITramiteRepositorio
{
    Tramite              Alta(Tramite                       tramite);
    bool                 Baja(int                           idTramite);
    void                 BajaPorExpediente(int              expedienteId);
    void                 Modificar(Tramite                  tramite);
    IEnumerable<Tramite> ObtenerPorEtiqueta(EtiquetaTramite etiquetaTramite);
    Tramite?             ObtenerPorId(int                   idTramite);
    Tramite?             ObtenerUltimoPorExpediente(int     expedienteId);
}