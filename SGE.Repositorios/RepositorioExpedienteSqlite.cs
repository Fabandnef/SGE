using Microsoft.EntityFrameworkCore;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

public class RepositorioExpedienteSqlite(SgeContext context) : IRepositorioExpediente
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
            context.Expedientes.Add(expediente);
            context.SaveChanges();
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

            context.Expedientes.Remove(expediente);
            context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al dar de baja el expediente con id {idExpediente}. {e.Message}");
        }
    }

    public Expediente? BuscarPorId(int idExpediente) => context.Expedientes.Include("Tramites").Include("UsuarioUltimaModificacion").Include("Tramites.UsuarioUltimaModificacion").FirstOrDefault(e => e.Id == idExpediente);

    public List<Expediente> Listar(int pagina) => context.Expedientes.Include("UsuarioUltimaModificacion").Include("Tramites")
                                                         .Skip((pagina - 1) * 10).Take(10).ToList();
    
    public int ContarTotal() => context.Expedientes.Count();

    public void Modificar(Expediente expediente)
    {
        try {
            context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al modificar el expediente con id {expediente.Id}. {e.Message}");
        }
    }
}