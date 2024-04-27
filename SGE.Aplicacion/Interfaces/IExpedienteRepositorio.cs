using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Interfaces;

public interface IExpedienteRepositorio
{
    void                    ExpedienteAlta(Expediente      expediente);
    void                    ExpedienteBaja(int             idExpediente);
    Expediente?             ExpedienteBuscarPorId(int      idExpediente);
    void                    ExpedienteModificar(Expediente expedienteNuevo);
    IEnumerable<Expediente> ExpedienteListar();
}