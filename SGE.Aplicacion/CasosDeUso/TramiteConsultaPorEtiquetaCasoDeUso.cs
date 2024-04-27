using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces;
using SGE.Aplicacion.Validadores;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteConsultaPorEtiquetaCasoDeUso(ITramiteRepositorio repositorio)
{
    public IEnumerable<Tramite> Ejecutar(EtiquetaTramite etiqueta) => repositorio.ObtenerTramitesPorEtiqueta(etiqueta);
}