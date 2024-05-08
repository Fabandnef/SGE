using System.Collections.ObjectModel;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

/// <summary>
/// Repositorio de trámites en un archivo de texto.
/// </summary>
public class RepositorioTramiteTxt : ITramiteRepositorio
{
    /// <summary>
    /// Ruta del archivo de trámites.
    /// </summary>
    private const  string RutaArchivo = "Tramites.txt";
    
    /// <summary>
    /// Último ID asignado a un trámite.
    /// </summary>
    static private int    _ultimoId;

    /// <summary>
    /// Constructor de la clase. Si el archivo existe y no está vacío, y el último ID es 0,
    /// se obtiene el último ID de los trámites.
    /// </summary>
    public RepositorioTramiteTxt()
    {
        if (File.Exists(RutaArchivo) && (new FileInfo(RutaArchivo).Length > 0) && (_ultimoId == 0)) {
            _ultimoId = ObtenerUltimoId();
        }
    }

    /// <summary>
    /// Da de alta un trámite.
    /// </summary>
    /// <param name="tramite">Trámite a dar de alta.</param>
    /// <returns><see cref="Tramite"/> dado de alta.</returns>
    public Tramite Alta(Tramite tramite)
    {
        tramite.Id = ++_ultimoId;
        using StreamWriter sw = new(RutaArchivo, true);
        sw.WriteLine(tramite.ToString());
        return tramite;
    }

    /// <summary>
    /// Da de baja un trámite.
    /// </summary>
    /// <param name="id">ID del trámite a dar de baja.</param>
    /// <returns><c>True</c> si se dio de baja el trámite, <c>False</c> si no se encontró.</returns>
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

    /// <summary>
    /// Modifica un trámite.
    /// </summary>
    /// <param name="tramite">Trámite a modificar.</param>
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

    /// <summary>
    /// Obtiene un trámite por su etiqueta.
    /// </summary>
    /// <param name="etiquetaTramite">Etiqueta del trámite.</param>
    /// <returns><see cref="IEnumerable{T}"/> de <see cref="Tramite"/>s con la etiqueta especificada.</returns>
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

    /// <summary>
    /// Obtiene un trámite por su ID.
    /// </summary>
    /// <param name="id">ID del trámite.</param>
    /// <returns><see cref="Tramite"/> con el ID especificado, <c>null</c> si no se encontró.</returns>
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

    /// <summary>
    /// Da de baja todos los trámites de un expediente.
    /// </summary>
    /// <param name="idExpediente">ID del expediente.</param>
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

    /// <summary>
    /// Obtiene el último trámite de un expediente.
    /// </summary>
    /// <param name="idExpediente">ID del expediente.</param>
    /// <returns>Último <see cref="Tramite"/> del expediente, <c>null</c> si no se encontró.</returns>
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

    /// <summary>
    /// Obtiene todos los trámites.
    /// </summary>
    /// <returns><see cref="IEnumerable{T}"/> de <see cref="Tramite"/>.</returns>
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

    /// <summary>
    /// Obtiene todos los trámites de un expediente.
    /// </summary>
    /// <param name="expediente">Expediente al que pertenecen los trámites.</param>
    /// <returns><see cref="IEnumerable{T}"/> de <see cref="Tramite"/>s del expediente.</returns>
    public IEnumerable<Tramite> ObtenerTramitesPorExpediente(Expediente expediente)
        => ObtenerTramitesPorExpediente(expediente.Id);

    /// <summary>
    /// Obtiene todos los trámites de un expediente.
    /// </summary>
    /// <param name="idExpediente">ID del expediente al que pertenecen los trámites.</param>
    /// <returns><see cref="IEnumerable{T}"/> de <see cref="Tramite"/>s del expediente.</returns>
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

    /// <summary>
    /// Guarda los trámites en el archivo.
    /// </summary>
    /// <param name="tramites"><see cref="IEnumerable{T}"/> de strings a guardar.</param>
    private void GuardarTramites(IEnumerable<string> tramites)
    {
        using StreamWriter sw = new(RutaArchivo);

        foreach (string tramite in tramites) {
            sw.WriteLine(tramite);
        }
    }

    /// <summary>
    /// Guarda los trámites en el archivo.
    /// </summary>
    /// <param name="tramites"><see cref="IEnumerable{T}"/> de <see cref="Tramite"/>s a guardar.</param>
    private void GuardarTramites(IEnumerable<Tramite> tramites)
    {
        using StreamWriter sw = new(RutaArchivo);

        foreach (Tramite tramite in tramites) {
            sw.WriteLine(tramite.ToString());
        }
    }

    /// <summary>
    /// Lee los trámites del archivo.
    /// </summary>
    /// <returns><see cref="IEnumerable{T}"/> de strings leídos.</returns>
    private IEnumerable<string> LeerTramites()
    {
        using StreamReader sr = new(RutaArchivo);

        string? linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            yield return linea;
        }
    }

    /// <summary>
    /// Obtiene el último ID de los trámites.
    /// </summary>
    /// <returns>Último ID de los trámites.</returns>
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