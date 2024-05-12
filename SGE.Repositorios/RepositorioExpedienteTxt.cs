using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

/// <summary>
///     Repositorio de expedientes en un archivo de texto.
/// </summary>
public sealed class RepositorioExpedienteTxt : RepositorioTxt, IExpedienteRepositorio
{
    #region CONSTANTES ---------------------------------------------------------------------------------
    /// <summary>
    ///     Ruta del archivo de expedientes.
    /// </summary>
    private const string RutaArchivo = "Expedientes.txt";
    #endregion

    #region CAMPOS ESTATICOS ---------------------------------------------------------------------------
    /// <summary>
    ///     Último ID asignado a un expediente.
    /// </summary>
    static private int _ultimoId;
    #endregion

    #region CONSTRUCTORES ------------------------------------------------------------------------------
    /// <summary>
    ///     Constructor de la clase. Si el archivo existe y no está vacío, y el último ID es 0,
    ///     se obtiene el último ID de los expedientes.
    /// </summary>
    public RepositorioExpedienteTxt() : base(RutaArchivo)
    {
        if ((new FileInfo(RutaArchivo).Length > 0) && (_ultimoId == 0)) {
            _ultimoId = ObtenerUltimoId();
        }
    }
    #endregion

    #region IMPLEMENTACIONES DE INTERFACES -------------------------------------------------------------
    #region IExpedienteRepositorio
    /// <inheritdoc />
    public void ActualizarEstado(int idExpediente, EstadoExpediente estadoExpediente)
    {
        Expediente? expediente = BuscarPorId(idExpediente);

        if (expediente is null) {
            throw new RepositorioException("No se encontró el expediente a modificar.");
        }

        if (expediente.Estado == estadoExpediente) {
            return;
        }

        expediente.Estado = estadoExpediente;
        Modificar(expediente);
    }

    /// <inheritdoc />
    public void Alta(Expediente expediente)
    {
        if (expediente.Id != 0) {
            throw new RepositorioException("No se puede dar de alta un expediente que ya tiene ID.");
        }

        expediente.Id = ++_ultimoId;
        using StreamWriter sw = new(RutaArchivo, true);

        try {
            sw.WriteLine(Encode(expediente));
        } catch (Exception e) {
            throw new RepositorioException("Error al guardar el expediente.", e);
        }
    }

    /// <inheritdoc />
    public void Baja(int idExpediente)
    {
        List<Expediente> expedientes = LeerExpedientes();

        int i                      = 0;
        int expedienteParaEliminar = -1;

        // Buscar el índice del expediente a eliminar.
        while (
            (i            < expedientes.Count)
         && (-1           == expedienteParaEliminar)
         && (idExpediente >= expedientes[i].Id)
        ) {
            if (expedientes[i].Id.Equals(idExpediente)) {
                expedienteParaEliminar = i;
            } else {
                i++;
            }
        }

        // Si no se encontró el expediente, tirar una excepción.
        if (expedienteParaEliminar == -1) {
            throw new RepositorioException($"No se pudo eliminar el expediente con ID {idExpediente}. Expediente no encontrado.");
        }

        // Eliminar el expediente y guardar los cambios.
        expedientes.RemoveAt(expedienteParaEliminar);
        GuardarExpedientes(expedientes);
    }

    /// <inheritdoc />
    public Expediente? BuscarPorId(int idExpediente)
    {
        using StreamReader sr = new(RutaArchivo);

        string?     linea      = sr.ReadLine();
        bool        found      = false;
        Expediente? expediente = null;

        // Recorrer el archivo hasta encontrar el expediente o llegar al final.
        while (!string.IsNullOrEmpty(linea) && !found) {
            expediente = Decode(linea);

            if (expediente.Id == idExpediente) {
                found = true;
            } else {
                linea = sr.ReadLine();
            }
        }

        return found ? expediente : null;
    }

    /// <inheritdoc />
    public List<Expediente> Listar()
    {
        List<Expediente> expedientes = [];

        using StreamReader sr = new(RutaArchivo);
        string?            linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            Expediente expediente = Decode(linea);
            expedientes.Add(expediente);
        }

        return expedientes;
    }

    /// <inheritdoc />
    public void Modificar(Expediente expediente)
    {
        List<Expediente> expedientes = LeerExpedientes().ToList();

        int  i                = 0;
        int  expedienteIndice = -1;
        bool found            = false;

        // Buscar el índice del expediente a modificar.
        while ((i < expedientes.Count) && !found) {
            if (expedientes[i].Id == expediente.Id) {
                expedienteIndice = i;
                found            = true;
            } else {
                i++;
            }
        }

        if (!found) {
            throw new RepositorioException("No se encontró el expediente a modificar.");
        }

        expedientes[expedienteIndice] = expediente;
        GuardarExpedientes(expedientes);
    }
    #endregion
    #endregion

    #region METODOS ------------------------------------------------------------------------------------
    /// <summary>
    ///     Decodificar una línea de texto en un expediente.
    /// </summary>
    /// <param name="linea">Línea de texto a decodificar.</param>
    /// <returns><see cref="Expediente" /> decodificado.</returns>
    private Expediente Decode(string linea)
    {
        string[] partes = linea.Split('\x1F');

        return new Expediente {
                                  Id                          = int.Parse(partes[0]),
                                  Caratula                    = partes[1],
                                  FechaCreacion               = DateTime.Parse(partes[2]),
                                  UltimaModificacion          = DateTime.Parse(partes[3]),
                                  IdUsuarioUltimaModificacion = int.Parse(partes[4]),
                                  Estado                      = Enum.Parse<EstadoExpediente>(partes[5]),
                              };
    }

    /// <summary>
    ///     Codificar un expediente en una línea de texto.
    /// </summary>
    /// <param name="expediente">Expediente a codificar.</param>
    /// <returns>Línea de texto codificada.</returns>
    private string Encode(Expediente expediente)
        => $"{expediente.Id}\x1F"                          +
           $"{expediente.Caratula}\x1F"                    +
           $"{expediente.FechaCreacion}\x1F"               +
           $"{expediente.UltimaModificacion}\x1F"          +
           $"{expediente.IdUsuarioUltimaModificacion}\x1F" +
           $"{expediente.Estado}";

    /// <summary>
    ///     Guardar los expedientes en el archivo.
    /// </summary>
    /// <param name="expedientes"><see cref="IEnumerable{T}" /> de <see cref="Expediente" /></param>
    private void GuardarExpedientes(IEnumerable<Expediente> expedientes)
    {
        using StreamWriter sw = new(RutaArchivo);

        try {
            foreach (Expediente expediente in expedientes) {
                sw.WriteLine(Encode(expediente));
            }
        } catch (Exception e) {
            throw new RepositorioException("Error al guardar los expedientes.", e);
        }
    }

    /// <summary>
    ///     Lee los expedientes del archivo.
    /// </summary>
    /// <returns><see cref="List{T}" /> de <see cref="Expediente" />.</returns>
    private List<Expediente> LeerExpedientes()
    {
        using StreamReader sr = new(RutaArchivo);

        string?          linea;
        List<Expediente> expedientes = [];

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            expedientes.Add(Decode(linea));
        }

        return expedientes;
    }

    /// <summary>
    ///     Obtiene el último ID de los expedientes.
    /// </summary>
    /// <returns>Último ID de los expedientes.</returns>
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

        Expediente e = Decode(prevLine);
        return e.Id;
    }
    #endregion
}