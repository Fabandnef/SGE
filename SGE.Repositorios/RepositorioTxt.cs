namespace SGE.Repositorios;

abstract public class RepositorioTxt
{
    protected RepositorioTxt(string archivo)
    {
        if (!File.Exists(archivo) && !string.IsNullOrEmpty(archivo)) {
            File.Create(archivo).Close();
        }
    }
}