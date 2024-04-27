using System.Collections.ObjectModel;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces;

namespace SGE.Repositorios;

public class RepositorioTramiteTxt : ITramiteRepositorio
{
    const string RutaArchivo = "Tramites.txt";
    static private int _ultimoId = 1;

    public void AltaTramite(Tramite tramite)
    {
        tramite.Id = _ultimoId++;
        using StreamWriter sw = new(RutaArchivo, append: true);
        sw.WriteLine(tramite.ToString());
    }
    
    public void BajaTramite(int id)
    {
        List<string> lineas = LeerTramites().ToList();
        
        int lineaParaEliminar = lineas.FindIndex(linea => linea.StartsWith(id.ToString() + '\x1F'));

        if (lineaParaEliminar == -1) {
            return;
        }
        
        // Eliminar y guardar
        lineas.RemoveAt(lineaParaEliminar);
        GuardarTramites(lineas);
    }

    public Tramite? ObtenerTramitePorId(int id)
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
    
    public IEnumerable<Tramite> ObtenerTramites()
    {
        List<Tramite> tramites = new();

        using StreamReader sr = new(RutaArchivo);

        string? linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            string[] partes = linea.Split(", ");
        }

        return tramites;
    }
    
    public IEnumerable<Tramite> ObtenerTramitesPorEtiqueta(EtiquetaTramite etiquetaTramite)
    {
        Collection<Tramite> tramites = new();
        
        using StreamReader sr = new(RutaArchivo);
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
        List<string> lineas = LeerTramites().ToList();
        
        int lineaParaEditar = lineas.FindIndex(linea => linea.StartsWith(tramite.Id.ToString()));

        if (lineaParaEditar == -1) {
            return;
        }

        // Editar y guardar
        lineas[lineaParaEditar] = tramite.ToString();
        GuardarTramites(lineas);
    }

    private IEnumerable<string> LeerTramites()
    {
        using StreamReader sr = new(RutaArchivo);

        string? linea;

        while (!string.IsNullOrEmpty(linea = sr.ReadLine())) {
            yield return linea;
        }
    }

    private void GuardarTramites(IEnumerable<Tramite> tramites)
    {
        using StreamWriter sw = new StreamWriter(RutaArchivo);

        foreach (Tramite tramite in tramites) {
            sw.WriteLine(tramite.ToString());
        }
    }
    
    private void GuardarTramites(IEnumerable<string> tramites)
    {
        using StreamWriter sw = new(RutaArchivo);

        foreach (string tramite in tramites) {
            sw.WriteLine(tramite);
        }
    }
}