using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

/// <summary>
///     Repositorio de trámites en un archivo de texto.
/// </summary>
public class RepositorioTramiteTxt : ITramiteRepositorio
{
    #region CONSTANTES ---------------------------------------------------------------------------------
    /// <summary>
    ///     Ruta del archivo de trámites.
    /// </summary>
    private const string RutaArchivo = "Tramites.txt";
    #endregion

    #region CAMPOS ESTATICOS ---------------------------------------------------------------------------
    /// <summary>
    ///     Último ID asignado a un trámite.
    /// </summary>
    static private int _ultimoId;
    #endregion

    #region CONSTRUCTORES ------------------------------------------------------------------------------
    /// <summary>
    ///     Constructor de la clase. Si el archivo existe y no está vacío, y el último ID es 0,
    ///     se obtiene el último ID de los trámites.
    /// </summary>
    public RepositorioTramiteTxt()
    {
        if (File.Exists(RutaArchivo) && (new FileInfo(RutaArchivo).Length > 0) && (_ultimoId == 0)) {
            _ultimoId = ObtenerUltimoId();
        }
    }
    #endregion

    #region IMPLEMENTACIONES DE INTERFACES -------------------------------------------------------------
    #region ITramiteRepositorio
    public Tramite Alta(Tramite tramite)
    {
        tramite.Id = ++_ultimoId;
        using StreamWriter sw = new(RutaArchivo, true);
        sw.WriteLine(Encode(tramite));
        return tramite;
    }

    public bool Baja(int idTramite)
    {
        List<Tramite> tramites = LeerTramites().ToList();

        int i                 = 0;
        int lineaParaEliminar = -1;

        while ((i < tramites.Count) && (lineaParaEliminar == -1)) {
            if (tramites[i].Id.Equals(idTramite)) {
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

        while ((i < tramites.Count) && (lineaParaEditar == -1)) {
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

    public List<Tramite> ObtenerPorEtiqueta(EtiquetaTramite etiquetaTramite)
    {
        List<Tramite> tramites = [];

        using StreamReader sr = new(RutaArchivo);
        string?            linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            Tramite t = Decode(linea);

            if (t.Etiqueta == etiquetaTramite) {
                tramites.Add(t);
            }
        }

        return tramites;
    }

    public Tramite? ObtenerPorId(int idTramite)
    {
        using StreamReader sr = new(RutaArchivo);

        string?  linea;
        bool     found = false;
        Tramite? t     = null;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine()) && !found) {
            t = Decode(linea);

            if (t.Id == idTramite) {
                found = true;
            }
        }

        return found ? t : null;
    }

    public void BajaPorExpediente(int idExpediente)
    {
        List<Tramite> tramites = LeerTramites();

        tramites.RemoveAll(tramite => tramite.idExpediente == idExpediente);

        GuardarTramites(tramites);
    }

    public Tramite? ObtenerUltimoPorExpediente(int idExpediente)
    {
        using StreamReader sr = new(RutaArchivo);

        string?  linea;
        Tramite? ultimoTramite = null;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            Tramite t = Decode(linea);

            if (t.idExpediente != idExpediente) {
                continue;
            }

            if ((ultimoTramite == null) || (t.FechaCreacion > ultimoTramite.FechaCreacion)) {
                ultimoTramite = t;
            }
        }

        return ultimoTramite;
    }
    #endregion
    #endregion

    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public List<Tramite> ObtenerTramites()
    {
        List<Tramite> tramites = [];

        using StreamReader sr = new(RutaArchivo);

        string? linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            tramites.Add(Decode(linea));
        }

        return tramites;
    }

    public Expediente ObtenerTramitesPorExpediente(Expediente expediente)
    {
        List<Tramite> tramites = [];

        using StreamReader sr = new(RutaArchivo);
        string?            linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            Tramite t = Decode(linea);

            if (t.idExpediente == expediente.Id) {
                tramites.Add(t);
            }
        }
        
        expediente.Tramites = tramites;

        return expediente;
    }
    #endregion

    #region METODOS ------------------------------------------------------------------------------------
    /// <summary>
    ///     Guarda los trámites en el archivo.
    /// </summary>
    /// <param name="tramites"><see cref="IEnumerable{T}" /> de <see cref="Tramite" />s a guardar.</param>
    private void GuardarTramites(IEnumerable<Tramite> tramites)
    {
        using StreamWriter sw = new(RutaArchivo);

        foreach (Tramite tramite in tramites) {
            sw.WriteLine(Encode(tramite));
        }
    }

    /// <summary>
    ///     Lee los trámites del archivo.
    /// </summary>
    /// <returns><see cref="List{T}" /> de strings leídos.</returns>
    private List<Tramite> LeerTramites()
    {
        using StreamReader sr = new(RutaArchivo);

        string?       linea;
        List<Tramite> tramites = [];

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            tramites.Add(Decode(linea));
        }

        return tramites;
    }

    /// <summary>
    ///     Obtiene el último ID de los trámites.
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

    /// <summary>
    ///     Codifica un trámite en una cadena de texto.
    /// </summary>
    /// <param name="tramite">Trámite a codificar.</param>
    /// <returns>Cadena de texto codificada.</returns>
    private string Encode(Tramite tramite)
        => $"{tramite.Id}\x1F"                 +
           $"{tramite.idExpediente}\x1F"       +
           $"{tramite.Etiqueta}\x1F"           +
           $"{tramite.Contenido}\x1F"          +
           $"{tramite.FechaCreacion}\x1F"      +
           $"{tramite.UltimaModificacion}\x1F" +
           $"{tramite.IdUsuarioUltimaModificacion}";

    /// <summary>
    ///     Decodifica una cadena de texto en un trámite.
    /// </summary>
    /// <param name="linea">Línea a decodificar.</param>
    /// <returns><see cref="Tramite" /> decodificado.</returns>
    private Tramite Decode(string linea)
    {
        string[] partes = linea.Split('\x1F');

        return new Tramite {
                               Id                          = int.Parse(partes[0]),
                               idExpediente                = int.Parse(partes[1]),
                               Etiqueta                    = Enum.Parse<EtiquetaTramite>(partes[2]),
                               Contenido                   = partes[3],
                               FechaCreacion               = DateTime.Parse(partes[4]),
                               UltimaModificacion          = DateTime.Parse(partes[5]),
                               IdUsuarioUltimaModificacion = int.Parse(partes[6]),
                           };
    }
    #endregion
}