using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Repositorios;

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

    public Expediente Alta(Expediente expediente)
    {
        expediente.Id = ++_ultimoId;

        using StreamWriter sw = new(RutaArchivo, true);
        sw.WriteLine(expediente.ToString());
        return expediente;
    }

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

    public void Modificar(Expediente expediente)
    {
        List<string> lineas = File.ReadAllLines(RutaArchivo).ToList();

        int expedienteIndice = lineas.FindIndex(linea => linea.StartsWith(expediente.Id.ToString() + '\x1F'));

        lineas[expedienteIndice] = expediente.ToString();
        File.WriteAllLines(RutaArchivo, lineas);
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