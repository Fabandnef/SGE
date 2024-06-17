using Microsoft.EntityFrameworkCore;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

public class RepositorioExpedienteSqlite(SgeContext context) : RepositorioSqlite(context), IRepositorioExpediente
{
    public void ActualizarEstado(int idExpediente, EstadoExpediente estadoExpediente)
    {
        try {
            Expediente? expediente = BuscarPorId(idExpediente);

            if (expediente is null) {
                throw new RepositorioException($"No se encontró el expediente con id {idExpediente}.");
            }

            expediente.Estado = estadoExpediente;
            Modificar(expediente);
        } catch (Exception e) {
            throw new
                RepositorioException($"Error al actualizar el estado del expediente con id {idExpediente}. {e.Message}");
        }
    }

    public void Alta(Expediente expediente)
    {
        if (expediente.Id != 0) {
            throw new RepositorioException("Error al dar de alta un expediente. No se pueden asignar los ID manualmente.");
        }
        
        try {
            Context.Expedientes.Add(expediente);
            Context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al dar de alta el expediente con id {expediente.Id}. {e.Message}");
        }
    }

    public void Baja(int idExpediente)
    {
        try {
            Expediente? expediente = BuscarPorId(idExpediente);

            if (expediente is null) {
                throw new RepositorioException($"No se encontró el expediente con id {idExpediente}.");
            }

            Context.Expedientes.Remove(expediente);
            Context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al dar de baja el expediente con id {idExpediente}. {e.Message}");
        }
    }

    public Expediente? BuscarPorId(int idExpediente) => Context.Expedientes.Include("Tramites").FirstOrDefault(e => e.Id == idExpediente);

    public List<Expediente> Listar(int pagina) => Context.Expedientes.Include("UsuarioUltimaModificacion")
                                                         .Skip((pagina - 1) * 10).Take(10).ToList();
    
    public int ContarTotal() => Context.Expedientes.Count();

    public void Modificar(Expediente expediente)
    {
        try {
            Context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al modificar el expediente con id {expediente.Id}. {e.Message}");
        }
    }
}