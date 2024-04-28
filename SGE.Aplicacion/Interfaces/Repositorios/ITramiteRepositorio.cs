using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Interfaces.Repositorios;

public interface ITramiteRepositorio
{
    void Alta(Tramite tramite);
    
    Tramite? ObtenerPorId(int idTramite);
    
    IEnumerable<Tramite> ObtenerPorEtiqueta(EtiquetaTramite etiquetaTramite);
    
    bool Baja(int idTramite);
    
    void Modificar(Tramite tramite);
    
    void BajaPorExpediente(int idExpediente);
  
    Tramite? ObtenerUltimoPorExpediente(int idExpediente);
}