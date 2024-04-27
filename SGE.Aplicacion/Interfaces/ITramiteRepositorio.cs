using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Interfaces;

public interface ITramiteRepositorio
{
    void AltaTramite(Tramite tramite);
    
    Tramite? ObtenerTramitePorId(int idTramite);
    
    IEnumerable<Tramite> ObtenerTramitesPorEtiqueta(EtiquetaTramite etiquetaTramite);
    
    void BajaTramite(int idTramite);
    
    void Modificar(Tramite tramite);
    
    void TramiteBajaPorExpediente(int idExpediente);
}