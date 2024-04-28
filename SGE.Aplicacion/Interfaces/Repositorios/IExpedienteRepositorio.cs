using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Interfaces.Repositorios;

public interface IExpedienteRepositorio
{
    void                    Alta(Expediente      expediente);
    bool                    Baja(int             idExpediente);
    Expediente?             BuscarPorId(int      idExpediente);
    void                    Modificar(Expediente expediente);
    IEnumerable<Expediente> Listar();

    void ActualizarEstado(int idExpediente, EstadoExpediente estadoExpediente);
}