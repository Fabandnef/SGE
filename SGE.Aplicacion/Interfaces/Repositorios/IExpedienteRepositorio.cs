using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Interfaces.Repositorios;

public interface IExpedienteRepositorio
{
    void                    ActualizarEstado(int idExpediente, EstadoExpediente estadoExpediente);
    Expediente              Alta(Expediente      expediente);
    bool                    Baja(int             idExpediente);
    Expediente?             BuscarPorId(int      idExpediente);
    IEnumerable<Expediente> Listar();
    void                    Modificar(Expediente expediente);
}