using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces;

namespace SGE.Repositorios;

public class RepositorioExpedienteTxt : IExpedienteRepositorio
{
    private const  string RutaArchivo = "Expedientes.txt";
    static private int    _ultimoId;

    public RepositorioExpedienteTxt()
    {
        if (File.Exists(RutaArchivo) && (new FileInfo(RutaArchivo).Length > 0) && (_ultimoId == 0)) {
            _ultimoId = ObtenerUltimoId();
        }
    }

    public void ExpedienteAlta(Expediente expediente)
    {
        expediente.Id = ++_ultimoId;

        using StreamWriter sw = new(RutaArchivo, true);
        sw.WriteLine(expediente.ToString());
    }

    public void ExpedienteModificar(Expediente expedienteNuevo)
    {
        // TODO: Preguntar si podemos usar la clase "File"

        List<string> lineas = File.ReadAllLines(RutaArchivo).ToList();

        int expedienteIndice = lineas.FindIndex(linea => linea.StartsWith(expedienteNuevo.Id.ToString() + '\x1F'));

        // TODO: Tener en cuenta que chequear la existencia de la linea se hace en el caso de uso. Así que esto debería ser redundante.
        if (expedienteIndice == -1) {
            return;
        }

        lineas[expedienteIndice] = expedienteNuevo.ToString();
        File.WriteAllLines(RutaArchivo, lineas);
    }

    public bool ExpedienteBaja(int idExpediente)
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

    public Expediente? ExpedienteBuscarPorId(int idExpediente)
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

    public IEnumerable<Expediente> ExpedienteListar()
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

    public void ExpedienteActualizarEstado(int idExpediente, EstadoExpediente estadoExpediente)
    {
        Expediente? expediente = ExpedienteBuscarPorId(idExpediente);
        
        if (expediente == null) {
            return;
        }
        
        expediente.Estado = estadoExpediente;
        ExpedienteModificar(expediente);
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