using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

/// <summary>
/// Repositorio de expedientes en un archivo de texto.
/// </summary>
public class RepositorioExpedienteTxt : IExpedienteRepositorio
{
    /// <summary>
    /// Ruta del archivo de expedientes.
    /// </summary>
    private const  string RutaArchivo = "Expedientes.txt";
    
    /// <summary>
    /// Último ID asignado a un expediente.
    /// </summary>
    static private int    _ultimoId;

    /// <summary>
    /// Constructor de la clase. Si el archivo existe y no está vacío, y el último ID es 0,
    /// se obtiene el último ID de los expedientes.
    /// </summary>
    public RepositorioExpedienteTxt()
    {
        if (File.Exists(RutaArchivo) && (new FileInfo(RutaArchivo).Length > 0) && (_ultimoId == 0)) {
            _ultimoId = ObtenerUltimoId();
        }
    }

    /// <summary>
    /// Actualiza el estado de un expediente.
    /// </summary>
    /// <param name="idExpediente">ID del expediente.</param>
    /// <param name="estadoExpediente">Nuevo <see cref="EstadoExpediente">Estado</see> del expediente.</param>
    public void ActualizarEstado(int idExpediente, EstadoExpediente estadoExpediente)
    {
        Expediente? expediente = BuscarPorId(idExpediente);

        if (expediente is null) {
            return;
        }

        if (expediente.Estado == estadoExpediente) {
            return;
        }

        expediente.Estado = estadoExpediente;
        Modificar(expediente);
    }

    /// <summary>
    /// Dar de alta un expediente.
    /// </summary>
    /// <param name="expediente">Expediente a dar de alta.</param>
    /// <returns><see cref="Expediente"/> dado de alta.</returns>
    public Expediente Alta(Expediente expediente)
    {
        expediente.Id = ++_ultimoId;

        using StreamWriter sw = new(RutaArchivo, true);
        sw.WriteLine(expediente.ToString());
        return expediente;
    }

    /// <summary>
    /// Dar de baja un expediente.
    /// </summary>
    /// <param name="idExpediente">ID del expediente a dar de baja.</param>
    /// <returns><c>True</c> si se dió de baja el expediente, <c>false</c> si no se encontró.</returns>
    public bool Baja(int idExpediente)
    {
        List<string> lineas = File.ReadAllLines(RutaArchivo).ToList();

        int lineaAEliminar = lineas.FindIndex(linea => linea.StartsWith(idExpediente.ToString() + '\x1F'));

        if (lineaAEliminar == -1) {
            return false;
        }

        lineas.RemoveAt(lineaAEliminar);
        File.WriteAllLines(RutaArchivo, lineas);

        return true;
    }

    /// <summary>
    /// Buscar un expediente por su ID.
    /// </summary>
    /// <param name="idExpediente">ID del expediente a buscar.</param>
    /// <returns><see cref="Expediente"/> encontrado, o <c>null</c> si no se encontró.</returns>
    public Expediente? BuscarPorId(int idExpediente)
    {
        using StreamReader sr = new(RutaArchivo);

        string? linea = sr.ReadLine();

        while (!string.IsNullOrEmpty(linea) && !linea.StartsWith(idExpediente.ToString() + '\x1F')) {
            linea = sr.ReadLine();
        }

        if (string.IsNullOrEmpty(linea)) {
            return null;
        }

        string[] partes = linea.Split('\x1F');

        Expediente expediente = new() {
                                          Id = int.Parse(partes[0]),
                                          Caratula = partes[1],
                                          FechaCreacion = DateTime.Parse(partes[2]),
                                          UltimaModificacion = DateTime.Parse(partes[3]),
                                          IdUsuarioUltimaModificacion = int.Parse(partes[4]),
                                          Estado = (EstadoExpediente)Enum.Parse(typeof(EstadoExpediente), partes[5]),
                                      };

        return expediente;
    }

    /// <summary>
    /// Listar todos los expedientes.
    /// </summary>
    /// <returns><see cref="IEnumerable{T}"/> de <see cref="Expediente"/>.</returns>
    public IEnumerable<Expediente> Listar()
    {
        List<Expediente> expedientes = new();

        using StreamReader sr    = new(RutaArchivo);
        string?            linea = sr.ReadLine();

        while (linea != null) {
            string[] partes = linea.Split('\x1F');

            Expediente expediente = new() {
                                              Id                          = int.Parse(partes[0]),
                                              Caratula                    = partes[1],
                                              FechaCreacion               = DateTime.Parse(partes[2]),
                                              UltimaModificacion          = DateTime.Parse(partes[3]),
                                              IdUsuarioUltimaModificacion = int.Parse(partes[4]),
                                              Estado =
                                                  (EstadoExpediente)Enum.Parse(typeof(EstadoExpediente), partes[5]),
                                          };

            expedientes.Add(expediente);
            linea = sr.ReadLine();
        }

        return expedientes;
    }

    /// <summary>
    /// Modificar un expediente.
    /// </summary>
    /// <param name="expediente"><see cref="Expediente"/> a modificar.</param>
    public void Modificar(Expediente expediente)
    {
        List<string> lineas = File.ReadAllLines(RutaArchivo).ToList();

        int expedienteIndice = lineas.FindIndex(linea => linea.StartsWith(expediente.Id.ToString() + '\x1F'));

        lineas[expedienteIndice] = expediente.ToString();
        File.WriteAllLines(RutaArchivo, lineas);
    }

    /// <summary>
    /// Obtener el último ID de los expedientes.
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

        string[] partes = prevLine.Split('\x1F');
        return int.Parse(partes[0]);
    }
}