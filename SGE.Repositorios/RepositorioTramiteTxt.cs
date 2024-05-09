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
        sw.WriteLine(Encode(tramite));
        return tramite;
    }

    /// <summary>
    /// Da de baja un trámite.
    /// </summary>
    /// <param name="id">ID del trámite a dar de baja.</param>
    /// <returns><c>True</c> si se dio de baja el trámite, <c>False</c> si no se encontró.</returns>
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

    /// <summary>
    /// Modifica un trámite.
    /// </summary>
    /// <param name="tramite">Trámite a modificar.</param>
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
        Tramite?            t = null;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            t = Decode(linea);
            
            if (t.Etiqueta == etiquetaTramite) {
                tramites.Add(t);
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

    /// <summary>
    /// Da de baja todos los trámites de un expediente.
    /// </summary>
    /// <param name="idExpediente">ID del expediente.</param>
    public void BajaPorExpediente(int idExpediente)
    {
        List<Tramite> tramites = LeerTramites().ToList();

        tramites.RemoveAll(tramite => expedienteId == tramite.ExpedienteId);

        GuardarTramites(tramites);
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
            tramites.Add(Decode(linea));
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
            sw.WriteLine(Encode(tramite));
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
            yield return Decode(linea);
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