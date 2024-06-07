using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

public class RepositorioExpedienteSqlite(SgeContext context) : RepositorioSqlite(context), IExpedienteRepositorio
{
    public void ActualizarEstado(int idExpediente, EstadoExpediente estadoExpediente)
    {
        try {
            Expediente? expediente = BuscarPorId(idExpediente);

            if (expediente is null) {
                throw new Exception($"No se encontró el expediente con id {idExpediente}.");
            }

            expediente.Estado = estadoExpediente;
            Modificar(expediente);
        } catch (Exception e) {
            throw new Exception($"Error al actualizar el estado del expediente con id {idExpediente}. {e.Message}");
        }
    }

    public void Alta(Expediente expediente)
    {
        try {
            Context.Expedientes.Add(expediente);
            Context.SaveChanges();
        } catch (Exception e) {
            throw new Exception($"Error al dar de alta el expediente con id {expediente.Id}. {e.Message}");
        }
    }

    public void Baja(int idExpediente)
    {
        try {
            Expediente? expediente = BuscarPorId(idExpediente);

            if (expediente is null) {
                throw new Exception($"No se encontró el expediente con id {idExpediente}.");
            }

            Context.Expedientes.Remove(expediente);
            Context.SaveChanges();
        } catch (Exception e) {
            throw new Exception($"Error al dar de baja el expediente con id {idExpediente}. {e.Message}");
        }
    }

    public Expediente? BuscarPorId(int idExpediente) => Context.Expedientes.Find(idExpediente);

    public List<Expediente> Listar() => Context.Expedientes.ToList();

    public void Modificar(Expediente expediente)
    {
        try {
            Context.SaveChanges();
        } catch (Exception e) {
            throw new Exception($"Error al modificar el expediente con id {expediente.Id}. {e.Message}");
        }
    }
}