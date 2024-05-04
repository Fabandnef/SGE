using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Interfaces.Repositorios;

public interface IExpedienteRepositorio
{
    void                    ActualizarEstado(int expedienteId, EstadoExpediente estadoExpediente);
    Expediente              Alta(Expediente      expediente);
    bool                    Baja(int             expedienteId);
    Expediente?             BuscarPorId(int      expedienteId);
    IEnumerable<Expediente> Listar();
    void                    Modificar(Expediente expediente);
}