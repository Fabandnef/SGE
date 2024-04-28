using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Validadores;

namespace SGE.Aplicacion.CasosDeUso;

public class TramiteConsultaPorEtiquetaCasoDeUso(ITramiteRepositorio tramiteRepositorio)
{
    public IEnumerable<Tramite> Ejecutar(EtiquetaTramite etiquetaTramite)
        => tramiteRepositorio.ObtenerPorEtiqueta(etiquetaTramite);
}