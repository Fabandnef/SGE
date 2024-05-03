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
        sw.WriteLine(tramite.ToString());
        return tramite;
    }

    public bool Baja(int id)
    {
        List<string> lineas = LeerTramites().ToList();

        int lineaParaEliminar = lineas.FindIndex(linea => linea.StartsWith(id.ToString() + '\x1F'));

        if (lineaParaEliminar == -1) {
            return false;
        }

        // Eliminar y guardar
        lineas.RemoveAt(lineaParaEliminar);
        GuardarTramites(lineas);

        return true;
    }

    public void Modificar(Tramite tramite)
    {
        List<string> lineas = LeerTramites().ToList();

        int lineaParaEditar = lineas.FindIndex(linea => linea.StartsWith(tramite.Id.ToString()));

        if (lineaParaEditar == -1) {
            return;
        }

        // Editar y guardar
        lineas[lineaParaEditar] = tramite.ToString();
        GuardarTramites(lineas);
    }

    public IEnumerable<Tramite> ObtenerPorEtiqueta(EtiquetaTramite etiquetaTramite)
    {
        Collection<Tramite> tramites = new();

        using StreamReader sr = new(RutaArchivo);
        string?            linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            string[] partes = linea.Split('\x1F');

            if (Enum.TryParse(partes[2], out EtiquetaTramite etiqueta) && (etiqueta == etiquetaTramite)) {
                tramites.Add(new Tramite {
                                             Id                          = int.Parse(partes[0]),
                                             ExpedienteId                = int.Parse(partes[1]),
                                             Etiqueta                    = etiqueta,
                                             Contenido                   = partes[3],
                                             FechaCreacion               = DateTime.Parse(partes[4]),
                                             UltimaModificacion          = DateTime.Parse(partes[5]),
                                             IdUsuarioUltimaModificacion = int.Parse(partes[6]),
                                         });
            }
        }

        return tramites;
    }

    public Tramite? ObtenerPorId(int id)
    {
        using StreamReader sr = new(RutaArchivo);

        string? linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            string[] partes = linea.Split('\x1F');

            if (int.TryParse(partes[0], out int idTramite) && (idTramite == id)) {
                return new Tramite {
                                       Id = idTramite,
                                       ExpedienteId = int.Parse(partes[1]),
                                       Etiqueta = (EtiquetaTramite)Enum.Parse(typeof(EtiquetaTramite), partes[2]),
                                       Contenido = partes[3],
                                       FechaCreacion = DateTime.Parse(partes[4]),
                                       UltimaModificacion = DateTime.Parse(partes[5]),
                                       IdUsuarioUltimaModificacion = int.Parse(partes[6]),
                                   };
            }
        }

        return null;
    }

    public void BajaPorExpediente(int idExpediente)
    {
        List<string> lineas = LeerTramites().ToList();

        lineas.RemoveAll(linea => {
                             string[] partes = linea.Split('\x1F');

                             return int.TryParse(partes[1], out int idExpedienteTramite) &&
                                    (idExpedienteTramite == idExpediente);
                         });

        GuardarTramites(lineas);
    }

    public Tramite? ObtenerUltimoPorExpediente(int idExpediente)
    {
        using StreamReader sr = new(RutaArchivo);

        string?  linea;
        Tramite? ultimoTramite = null;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            string[] partes = linea.Split('\x1F');

            if (int.TryParse(partes[1], out int idExpedienteTramite) && (idExpedienteTramite == idExpediente)) {
                ultimoTramite = new Tramite {
                                                Id           = int.Parse(partes[0]),
                                                ExpedienteId = idExpedienteTramite,
                                                Etiqueta = (EtiquetaTramite)Enum.Parse(typeof(EtiquetaTramite),
                                                                                       partes[2]),
                                                Contenido                   = partes[3],
                                                FechaCreacion               = DateTime.Parse(partes[4]),
                                                UltimaModificacion          = DateTime.Parse(partes[5]),
                                                IdUsuarioUltimaModificacion = int.Parse(partes[6]),
                                            };
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
            string[] partes = linea.Split(", ");

            tramites.Add(new Tramite {
                                         Id = int.Parse(partes[0]),
                                         ExpedienteId = int.Parse(string.IsNullOrEmpty(partes[1]) ? "0" : partes[1]),
                                         Etiqueta = (EtiquetaTramite)Enum.Parse(typeof(EtiquetaTramite), partes[2]),
                                         Contenido = partes[3],
                                         FechaCreacion = DateTime.Parse(partes[4]),
                                         UltimaModificacion = DateTime.Parse(partes[5]),
                                         IdUsuarioUltimaModificacion = int.Parse(partes[6]),
                                     });
        }

        return tramites;
    }

    public IEnumerable<Tramite> ObtenerTramitesPorExpediente(Expediente expediente)
        => ObtenerTramitesPorExpediente(expediente.Id);

    public IEnumerable<Tramite> ObtenerTramitesPorExpediente(int idExpediente)
    {
        Collection<Tramite> tramites = new();

        using StreamReader sr = new(RutaArchivo);
        string?            linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            string[] partes = linea.Split('\x1F');

            if (int.TryParse(partes[1], out int idExpedienteTramite) && (idExpedienteTramite == idExpediente)) {
                tramites.Add(new Tramite {
                                             Id                          = int.Parse(partes[0]),
                                             ExpedienteId                = int.Parse(partes[1]),
                                             Etiqueta                    = Enum.Parse<EtiquetaTramite>(partes[2]),
                                             Contenido                   = partes[3],
                                             FechaCreacion               = DateTime.Parse(partes[4]),
                                             UltimaModificacion          = DateTime.Parse(partes[5]),
                                             IdUsuarioUltimaModificacion = int.Parse(partes[6]),
                                         });
            }
        }

        return tramites;
    }

    private void GuardarTramites(IEnumerable<string> tramites)
    {
        using StreamWriter sw = new(RutaArchivo);

        foreach (string tramite in tramites) {
            sw.WriteLine(tramite);
        }
    }

    private void GuardarTramites(IEnumerable<Tramite> tramites)
    {
        using StreamWriter sw = new(RutaArchivo);

        foreach (Tramite tramite in tramites) {
            sw.WriteLine(tramite.ToString());
        }
    }

    private IEnumerable<string> LeerTramites()
    {
        using StreamReader sr = new(RutaArchivo);

        string? linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            yield return linea;
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
}