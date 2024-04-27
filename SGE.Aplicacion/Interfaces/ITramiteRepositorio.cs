using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Interfaces;

public interface ITramiteRepositorio
{
    void AgregarTramite(Tramite tramite);
    
    Tramite? ObtenerTramitePorId(int idTramite);
    
    IEnumerable<Tramite> ObtenerTramitesPorEtiqueta(EtiquetaTramite etiqueta);
    
    void EliminarTramite(int idTramite);
}