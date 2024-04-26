using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces;

namespace SGE.Repositorios;

public class RepositorioTramiteTXT : ITramiteRepositorio
{
    const string RUTA_ARCHIVO = "C:\\Tramites.txt";

    public void AgregarTramite(Tramite tramite)
    {
        StreamWriter sw = new(RUTA_ARCHIVO, true);
        sw.WriteLine(tramite.ToString());
    }

    public void EditarTramite(Tramite tramite)
    {
        // Leer todas las líneas del archivo
        List<string> lineas = new();

        using (StreamReader sr = new(RUTA_ARCHIVO)) {
            string? linea;

            while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
                lineas.Add(linea);
            }
        }

        // Buscar la línea que contiene la información del trámite que queremos editar
        int lineaParaEditar = lineas.FindIndex(linea => linea.StartsWith(tramite.Id.ToString()));

        // Si encontramos la línea, la reemplazamos con la nueva información del trámite
        if (lineaParaEditar > -1) {
            lineas[lineaParaEditar] = tramite.ToString();
        }

        // Escribir todas las líneas de nuevo en el archivo
        using (StreamWriter sw = new(RUTA_ARCHIVO)) {
            foreach (string linea in lineas) {
                sw.WriteLine(linea);
            }
        }
    }

    public void EliminarTramite(Tramite tramite)
    {
        File.WriteAllLines(
                           RUTA_ARCHIVO,
                           File.ReadAllLines(RUTA_ARCHIVO)
                               .Where(linea => !linea.StartsWith(tramite.Id.ToString())).ToList()
                          );
    }

    public Tramite ObtenerTramitePorId(int id)
    {
        string? linea = File.ReadAllLines(RUTA_ARCHIVO)
                            .ToList()
                            .Find(linea => linea.StartsWith(id.ToString()));

        if (!string.IsNullOrEmpty(linea)) { }
    }

    public IEnumerable<Tramite> ObtenerTramites()
    {
        List<Tramite> tramites = new();

        using StreamReader sr = new(RUTA_ARCHIVO);

        string? linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            string[] partes = linea.Split(", ");

            tramites.Add(new Tramite(int.Parse(partes[0].Split(": ")[1]),
                                     int.Parse(partes[1].Split(": ")[1]),
                                     (EtiquetaTramite)Enum.Parse(typeof(EtiquetaTramite), partes[2].Split(": ")[1]),
                                     partes[3].Split(": ")[1],
                                     DateTime.Parse(partes[4].Split(": ")[1]),
                                     DateTime.Parse(partes[5].Split(": ")[1]),
                                     int.Parse(partes[6].Split(": ")[1]),
                                     int.Parse(partes[7].Split(": ")[1])
                                    )
                        );
        }

        return tramites;
    }

    private void GuardarTramites(IEnumerable<Tramite> tramites)
    {
        using StreamWriter sw = new StreamWriter(RUTA_ARCHIVO);

        foreach (Tramite tramite in tramites) {
            sw.WriteLine(tramite.ToString());
        }
    }
}