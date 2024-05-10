using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteBuscarPorIdConTramitesCasoDeUso(IExpedienteRepositorio expedienteRepositorio, ITramiteRepositorio tramiteRepositorio)
{
    public Expediente? Ejecutar(int expedienteId)
    {
        Expediente? expediente = expedienteRepositorio.BuscarPorId(expedienteId);
        
        if (expediente != null)
        {
            expediente = tramiteRepositorio.ObtenerTramitesPorExpediente(expediente);
        }
        
        return expediente;
    }
}