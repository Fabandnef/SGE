using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

/// <summary>
///     Repositorio de trámites en un archivo de texto.
/// </summary>
[Obsolete("Usar RepositorioTramiteSql en su lugar.", false)]
public sealed class RepositorioTramiteTxt : RepositorioTxt, ITramiteRepositorio
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
    static private int? _ultimoId = 0;
    #endregion

    #region CONSTRUCTORES ------------------------------------------------------------------------------
    /// <summary>
    ///     Constructor de la clase. Si el archivo existe y no está vacío, y el último ID es 0,
    ///     se obtiene el último ID de los trámites.
    /// </summary>
    public RepositorioTramiteTxt() : base(RutaArchivo)
    {
        if ((new FileInfo(RutaArchivo).Length > 0) && (_ultimoId == 0)) {
            _ultimoId = ObtenerUltimoId();
        }
    }
    #endregion

    #region IMPLEMENTACIONES DE INTERFACES -------------------------------------------------------------
    #region ITramiteRepositorio
    /// <inheritdoc />
    public void Alta(Tramite tramite)
    {
        if (tramite.Id != 0) {
            throw new RepositorioException("No se puede dar de alta un trámite con ID ya asignado.");
        }

        tramite.Id = ++_ultimoId;
        using StreamWriter sw = new(RutaArchivo, true);

        try {
            sw.WriteLine(Encode(tramite));
        } catch (Exception e) {
            throw new RepositorioException("Error al guardar el trámite.", e);
        }
    }

    /// <inheritdoc />
    public void Baja(int idTramite)
    {
        List<Tramite> tramites = LeerTramites().ToList();

        int i                 = 0;
        int lineaParaEliminar = -1;

        while (
            (i         < tramites.Count)
         && (-1        == lineaParaEliminar)
         && (idTramite >= tramites[i].Id)
        ) {
            if (tramites[i].Id.Equals(idTramite)) {
                lineaParaEliminar = i;
            } else {
                i++;
            }
        }

        // Si no se encontró el trámite, tirar una excepción
        if (lineaParaEliminar == -1) {
            throw new RepositorioException("No se encontró el trámite a eliminar.");
        }

        // Eliminar y guardar
        tramites.RemoveAt(lineaParaEliminar);
        GuardarTramites(tramites);
    }

    /// <inheritdoc />
    public void BajaPorExpediente(int idExpediente)
    {
        List<Tramite> tramites = LeerTramites();

        tramites.RemoveAll(tramite => tramite.ExpedienteId == idExpediente);

        GuardarTramites(tramites);
    }

    /// <inheritdoc />
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
            throw new RepositorioException("No se encontró el trámite a modificar.");
        }

        // Editar y guardar
        tramites[lineaParaEditar] = tramite;
        GuardarTramites(tramites);
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <summary>
    ///     Obtiene todos los trámites.
    /// </summary>
    /// <returns><see cref="List{T}" /> de <see cref="Tramite" />s.</returns>
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

    /// <summary>
    ///     Obtiene los trámites de un expediente dado.
    /// </summary>
    /// <param name="expediente">Expediente a buscar.</param>
    /// <returns><see cref="Expediente" /> con sus trámites.</returns>
    public Expediente ObtenerTramitesPorExpediente(Expediente expediente)
    {
        List<Tramite> tramites = [];

        using StreamReader sr = new(RutaArchivo);
        string?            linea;

        // Busco los trámites del expediente y los agrego a la lista
        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            Tramite t = Decode(linea);

            if (t.ExpedienteId == expediente.Id) {
                tramites.Add(t);
            }
        }

        // Asigno los trámites al expediente y lo devuelvo
        expediente.Tramites = tramites;
        return expediente;
    }

    /// <inheritdoc />
    public Tramite? ObtenerUltimoPorExpediente(int idExpediente)
    {
        using StreamReader sr = new(RutaArchivo);

        string?  linea;
        Tramite? ultimoTramite = null;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            Tramite t = Decode(linea);

            if (t.ExpedienteId != idExpediente) {
                continue;
            }

            if ((ultimoTramite == null) || (t.CreatedAt > ultimoTramite.CreatedAt)) {
                ultimoTramite = t;
            }
        }

        return ultimoTramite;
    }
    #endregion
    #endregion

    #region METODOS ------------------------------------------------------------------------------------
    /// <summary>
    ///     Decodifica una línea de texto en un trámite.
    /// </summary>
    /// <param name="linea">Línea de texto a decodificar.</param>
    /// <returns><see cref="Tramite" /> decodificado.</returns>
    private Tramite Decode(string linea)
    {
        string[] partes = linea.Split('\x1F');

        return new Tramite {
                               Id                          = int.Parse(partes[0]),
                               ExpedienteId                = int.Parse(partes[1]),
                               Etiqueta                    = Enum.Parse<EtiquetaTramite>(partes[2]),
                               Contenido                   = partes[3],
                               CreatedAt               = DateTime.Parse(partes[4]),
                               UpdatedAt          = DateTime.Parse(partes[5]),
                               IdUsuarioUltimaModificacion = int.Parse(partes[6]),
                           };
    }

    /// <summary>
    ///     Codifica un trámite en una línea de texto.
    /// </summary>
    /// <param name="tramite">Trámite a codificar.</param>
    /// <returns>Línea de texto codificada.</returns>
    private string Encode(Tramite tramite)
        => $"{tramite.Id}\x1F"                 +
           $"{tramite.ExpedienteId}\x1F"       +
           $"{tramite.Etiqueta}\x1F"           +
           $"{tramite.Contenido}\x1F"          +
           $"{tramite.CreatedAt}\x1F"      +
           $"{tramite.UpdatedAt}\x1F" +
           $"{tramite.IdUsuarioUltimaModificacion}";

    /// <summary>
    ///     Guarda los trámites en el archivo.
    /// </summary>
    /// <param name="tramites"><see cref="IEnumerable{T}" /> de <see cref="Tramite" />s a guardar.</param>
    private void GuardarTramites(IEnumerable<Tramite> tramites)
    {
        using StreamWriter sw = new(RutaArchivo);

        try {
            foreach (Tramite tramite in tramites) {
                sw.WriteLine(Encode(tramite));
            }
        } catch (Exception e) {
            throw new RepositorioException("Error al guardar los trámites.", e);
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
    private int? ObtenerUltimoId()
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

        Tramite t = Decode(prevLine);
        return t.Id;
    }
    #endregion
}