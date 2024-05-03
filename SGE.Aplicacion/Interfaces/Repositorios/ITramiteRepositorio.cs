using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Interfaces.Repositorios;

public interface ITramiteRepositorio
{
    void Alta(Tramite tramite);

    bool Baja(int idTramite);

    void BajaPorExpediente(int idExpediente);

    void Modificar(Tramite tramite);

    IEnumerable<Tramite> ObtenerPorEtiqueta(EtiquetaTramite etiquetaTramite);

    Tramite? ObtenerPorId(int idTramite);

    Tramite? ObtenerUltimoPorExpediente(int idExpediente);
}