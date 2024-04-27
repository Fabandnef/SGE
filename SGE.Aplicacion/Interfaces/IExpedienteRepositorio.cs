using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;

namespace SGE.Aplicacion.Interfaces;

public interface IExpedienteRepositorio
{
    void                    ExpedienteAlta(Expediente      expediente);
    bool                    ExpedienteBaja(int             idExpediente);
    Expediente?             ExpedienteBuscarPorId(int      idExpediente);
    void                    ExpedienteModificar(Expediente expedienteNuevo);
    IEnumerable<Expediente> ExpedienteListar();
    
    void                    ExpedienteActualizarEstado(int idExpediente, EstadoExpediente estadoExpediente);
}