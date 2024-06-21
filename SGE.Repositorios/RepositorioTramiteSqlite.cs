using Microsoft.EntityFrameworkCore;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

public class RepositorioTramiteSqlite(SgeContext context) : IRepositorioTramite
{
    #region IMPLEMENTACIONES DE INTERFACES -------------------------------------------------------------
    #region IRepositorioTramite
    public void Alta(Tramite tramite)
    {
        try {
            context.Tramites.Add(tramite);
            context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al dar de alta el trámite con id {tramite.Id}. {e.Message}");
        }
    }

    public void Baja(int idTramite)
    {
        try {
            Tramite? tramite = ObtenerPorId(idTramite);

            if (tramite is null) {
                throw new RepositorioException($"No se encontró el trámite con id {idTramite}.");
            }

            context.Tramites.Remove(tramite);
            context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al dar de baja el trámite con id {idTramite}. {e.Message}");
        }
    }

    public void BajaPorExpediente(int idExpediente)
    {
        try {
            List<Tramite> tramites = context.Tramites.Where(t => t.ExpedienteId == idExpediente).ToList();

            context.Tramites.RemoveRange(tramites);
            context.SaveChanges();
        } catch (Exception e) {
            throw new
                RepositorioException($"Error al dar de baja los trámites del expediente con id {idExpediente}. {e.Message}");
        }
    }

    public int ContarTotal() => context.Tramites.Count();

    public void Modificar(Tramite tramite)
    {
        try {
            context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al modificar el trámite con id {tramite.Id}. {e.Message}");
        }
    }

    public List<Tramite> ObtenerPorEtiqueta(EtiquetaTramite etiquetaTramite, int pagina = 1)
        => context.Tramites.Include("UsuarioUltimaModificacion")
                  .Where(t => t.Etiqueta == etiquetaTramite)
                  .Skip((pagina - 1) * 10).Take(10).ToList();

    public Tramite? ObtenerPorId(int idTramite)
        => context.Tramites.Include("UsuarioUltimaModificacion").FirstOrDefault(t => t.Id == idTramite);

    public List<Tramite> ObtenerTramites(int pagina = 1)
        => context.Tramites.Include("UsuarioUltimaModificacion")
                  .Skip((pagina - 1) * 10).Take(10).ToList();

    public Expediente ObtenerTramitesPorExpediente(Expediente expediente)
    {
        context.Entry(expediente).Collection(e => e.Tramites).Load();
        return expediente;
    }

    public Tramite? ObtenerUltimoPorExpediente(int idExpediente)
        => context.Tramites.Where(t => t.ExpedienteId == idExpediente).OrderByDescending(t => t.UpdatedAt)
                  .FirstOrDefault();
    #endregion
    #endregion
}