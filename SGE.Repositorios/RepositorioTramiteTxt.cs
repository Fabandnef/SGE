using System.Collections.ObjectModel;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces;

namespace SGE.Repositorios;

public class RepositorioTramiteTxt : ITramiteRepositorio
{
    const string RUTA_ARCHIVO = "Tramites.txt";
    static private int _ultimoId = 1;

    public void AltaTramite(Tramite tramite)
    {
        tramite.Id = _ultimoId++;
        using StreamWriter sw = new(RUTA_ARCHIVO, append: true);
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

    public void BajaTramite(int id)
    {
        // Leer todas las líneas del archivo
        List<string> tramites = new();

        using (StreamReader sr = new(RUTA_ARCHIVO)) {
            string? tramiteImportado;

            while (!string.IsNullOrEmpty(tramiteImportado = sr.ReadLine())) {
                tramites.Add(tramiteImportado);
            }
        }

        // Buscar la línea que contiene la información del trámite que queremos eliminar
        int lineaParaEliminar = tramites.FindIndex(linea => linea.StartsWith(id.ToString()));

        // Si encontramos la línea, la eliminamos
        if (lineaParaEliminar > -1) {
            tramites.RemoveAt(lineaParaEliminar);
        }

        // Escribir todas las líneas de nuevo en el archivo
        using (StreamWriter sw = new(RUTA_ARCHIVO)) {
            foreach (string tramite in tramites) {
                sw.WriteLine(tramite);
            }
        }
    }

    public Tramite? ObtenerTramitePorId(int id)
    {
        using StreamReader sr = new(RUTA_ARCHIVO);
        
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
                    IdUsuarioUltimaModificacion = int.Parse(partes[6])
                };
            }
        }

        return null;
    }
    
    public IEnumerable<Tramite> ObtenerTramites()
    {
        List<Tramite> tramites = new();

        using StreamReader sr = new(RUTA_ARCHIVO);

        string? linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            string[] partes = linea.Split(", ");
        }

        return tramites;
    }
    
    public IEnumerable<Tramite> ObtenerTramitesPorEtiqueta(EtiquetaTramite etiquetaTramite)
    {
        Collection<Tramite> tramites = new();
        
        using StreamReader sr = new(RUTA_ARCHIVO);
        string? linea;
        
        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            string[] partes = linea.Split('\x1F');
            
            if (Enum.TryParse(partes[2], out EtiquetaTramite etiqueta) && (etiqueta == etiquetaTramite)) {
                tramites.Add(new Tramite {
                    Id = int.Parse(partes[0]),
                    ExpedienteId = int.Parse(partes[1]),
                    Etiqueta = etiqueta,
                    Contenido = partes[3],
                    FechaCreacion = DateTime.Parse(partes[4]),
                    UltimaModificacion = DateTime.Parse(partes[5]),
                    IdUsuarioUltimaModificacion = int.Parse(partes[6])
                });
            }
        }
        
        return tramites;
    }
    
    public void Modificar(Tramite tramite)
    {
        // Leer todas las líneas del archivo
        List<string> lineas = new();

        using (StreamReader sr = new(RUTA_ARCHIVO)) {
            string? tramiteImportado;

            while (!string.IsNullOrEmpty(tramiteImportado = sr.ReadLine())) {
                lineas.Add(tramiteImportado);
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

    private void GuardarTramites(IEnumerable<Tramite> tramites)
    {
        using StreamWriter sw = new StreamWriter(RUTA_ARCHIVO);

        foreach (Tramite tramite in tramites) {
            sw.WriteLine(tramite.ToString());
        }
    }
}