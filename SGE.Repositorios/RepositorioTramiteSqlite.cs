using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

public class RepositorioTramiteSqlite(SgeContext context) : RepositorioSqlite(context), ITramiteRepositorio
{
    public void Alta(Tramite tramite)
    {
        try {
            Context.Tramites.Add(tramite);
            Context.SaveChanges();
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

            Context.Tramites.Remove(tramite);
            Context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al dar de baja el trámite con id {idTramite}. {e.Message}");
        }
    }

    public void BajaPorExpediente(int idExpediente)
    {
        try {
            List<Tramite> tramites = Context.Tramites.Where(t => t.ExpedienteId == idExpediente).ToList();

            Context.Tramites.RemoveRange(tramites);
            Context.SaveChanges();
        } catch (Exception e) {
            throw new
                RepositorioException($"Error al dar de baja los trámites del expediente con id {idExpediente}. {e.Message}");
        }
    }

    public void Modificar(Tramite tramite)
    {
        try {
            Context.SaveChanges();
        } catch (Exception e) {
            throw new RepositorioException($"Error al modificar el trámite con id {tramite.Id}. {e.Message}");
        }
    }

    public List<Tramite> ObtenerPorEtiqueta(EtiquetaTramite etiquetaTramite)
        => Context.Tramites.Where(t => t.Etiqueta == etiquetaTramite).ToList();

    public Tramite? ObtenerPorId(int idTramite) => Context.Tramites.Find(idTramite);

    public List<Tramite> ObtenerTramites() => Context.Tramites.ToList();

    public Expediente ObtenerTramitesPorExpediente(Expediente expediente)
    {
        Context.Entry(expediente).Collection(e => e.Tramites).Load();
        return expediente;
    }

    public Tramite? ObtenerUltimoPorExpediente(int idExpediente)
        => Context.Tramites.Where(t => t.ExpedienteId == idExpediente).OrderByDescending(t => t.UpdatedAt)
                  .FirstOrDefault();
}