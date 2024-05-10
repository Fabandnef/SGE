using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteListarConTramitesCasoDeUso(IExpedienteRepositorio expedienteRepositorio, ITramiteRepositorio tramiteRepositorio)
{
    public List<Expediente> Ejecutar()
    {
        List<Expediente> expedientes = expedienteRepositorio.Listar();
        List<Expediente> expedientesConTramites = [];
        
        foreach (Expediente t in expedientes) {
            expedientesConTramites.Add(tramiteRepositorio.ObtenerTramitesPorExpediente(t));
        }
        
        return expedientesConTramites;
    }
}