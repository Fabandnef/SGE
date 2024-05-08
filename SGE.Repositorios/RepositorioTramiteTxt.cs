using System.Collections.ObjectModel;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

public class RepositorioTramiteTxt : ITramiteRepositorio
{
    private const  string RutaArchivo = "Tramites.txt";
    static private int    _ultimoId;

    public RepositorioTramiteTxt()
    {
        if (File.Exists(RutaArchivo) && (new FileInfo(RutaArchivo).Length > 0) && (_ultimoId == 0)) {
            _ultimoId = ObtenerUltimoId();
        }
    }

    public Tramite Alta(Tramite tramite)
    {
        tramite.Id = ++_ultimoId;
        using StreamWriter sw = new(RutaArchivo, true);
        sw.WriteLine(Encode(tramite));
        return tramite;
    }

    public bool Baja(int id)
    {
        List<Tramite> tramites = LeerTramites().ToList();

        int i                 = 0;
        int lineaParaEliminar = -1;

        while (i < tramites.Count && lineaParaEliminar == -1) {
            if (tramites[i].Id.Equals(id)) {
                lineaParaEliminar = i;
            }

            i++;
        }

        if (lineaParaEliminar == -1) {
            return false;
        }

        // Eliminar y guardar
        tramites.RemoveAt(lineaParaEliminar);
        GuardarTramites(tramites);

        return true;
    }

    public void Modificar(Tramite tramite)
    {
        List<Tramite> tramites = LeerTramites().ToList();

        int i               = 0;
        int lineaParaEditar = -1;

        while (i < tramites.Count && lineaParaEditar == -1) {
            if (tramites[i].Id.Equals(tramite.Id)) {
                lineaParaEditar = i;
            }

            i++;
        }

        if (lineaParaEditar == -1) {
            return;
        }

        // Editar y guardar
        tramites[lineaParaEditar] = tramite;
        GuardarTramites(tramites);
    }

    public IEnumerable<Tramite> ObtenerPorEtiqueta(EtiquetaTramite etiquetaTramite)
    {
        Collection<Tramite> tramites = new();

        using StreamReader sr = new(RutaArchivo);
        string?            linea;
        Tramite?            t = null;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            t = Decode(linea);
            
            if (t.Etiqueta == etiquetaTramite) {
                tramites.Add(t);
            }
        }

        return tramites;
    }

    public Tramite? ObtenerPorId(int id)
    {
        using StreamReader sr = new(RutaArchivo);

        string? linea;
        bool    found = false;
        Tramite? t = null;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine()) && !found) {
            t = Decode(linea);
            
            if (t.Id == id) {
                found = true;
            }
        }

        return found ? t : null;
    }

    public void BajaPorExpediente(int expedienteId)
    {
        List<Tramite> tramites = LeerTramites().ToList();

        tramites.RemoveAll(tramite => expedienteId == tramite.ExpedienteId);

        GuardarTramites(tramites);
    }

    public Tramite? ObtenerUltimoPorExpediente(int expedienteId)
    {
        using StreamReader sr = new(RutaArchivo);

        string?  linea;
        Tramite? ultimoTramite = null;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            Tramite? t = Decode(linea);

            if (t.ExpedienteId != expedienteId) {
                continue;
            }

            if ((ultimoTramite == null) || (t.FechaCreacion > ultimoTramite.FechaCreacion)) {
                ultimoTramite = t;
            }
        }

        return ultimoTramite;
    }

    public IEnumerable<Tramite> ObtenerTramites()
    {
        List<Tramite> tramites = [];

        using StreamReader sr = new(RutaArchivo);

        string? linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            tramites.Add(Decode(linea));
        }

        return tramites;
    }

    public IEnumerable<Tramite> ObtenerTramitesPorExpediente(Expediente expediente)
        => ObtenerTramitesPorExpediente(expediente.Id);

    public IEnumerable<Tramite> ObtenerTramitesPorExpediente(int expedienteId)
    {
        List<Tramite> tramites = new();

        using StreamReader sr = new(RutaArchivo);
        string?            linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            Tramite? t = Decode(linea);
            
            if (t.ExpedienteId == expedienteId) {
                tramites.Add(t);
            }
        }

        return tramites;
    }

    private void GuardarTramites(IEnumerable<Tramite> tramites)
    {
        using StreamWriter sw = new(RutaArchivo);

        foreach (Tramite tramite in tramites) {
            sw.WriteLine(Encode(tramite));
        }
    }

    private IEnumerable<Tramite> LeerTramites()
    {
        using StreamReader sr = new(RutaArchivo);

        string? linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            yield return Decode(linea);
        }
    }

    private int ObtenerUltimoId()
    {
        using StreamReader sr = new(RutaArchivo);

        string  prevLine = "";
        string? line     = sr.ReadLine();

        while (line != null) {
            prevLine = line;
            line     = sr.ReadLine();
        }

        if (string.IsNullOrEmpty(prevLine)) {
            return 0;
        }

        string[] partes = prevLine.Split('\x1F');
        return int.Parse(partes[0]);
    }

    private string Encode(Tramite tramite)
        => $"{tramite.Id}\x1F"                 +
           $"{tramite.ExpedienteId}\x1F"       +
           $"{tramite.Etiqueta}\x1F"           +
           $"{tramite.Contenido}\x1F"          +
           $"{tramite.FechaCreacion}\x1F"      +
           $"{tramite.UltimaModificacion}\x1F" +
           $"{tramite.IdUsuarioUltimaModificacion}";

    private Tramite Decode(string linea)
    {
        string[] partes = linea.Split('\x1F');

        return new Tramite {
                               Id                          = int.Parse(partes[0]),
                               ExpedienteId                = int.Parse(partes[1]),
                               Etiqueta                    = Enum.Parse<EtiquetaTramite>(partes[2]),
                               Contenido                   = partes[3],
                               FechaCreacion               = DateTime.Parse(partes[4]),
                               UltimaModificacion          = DateTime.Parse(partes[5]),
                               IdUsuarioUltimaModificacion = int.Parse(partes[6]),
                           };
    }
}