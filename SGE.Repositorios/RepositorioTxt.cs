namespace SGE.Repositorios;

abstract public class RepositorioTxt
{
    #region CONSTRUCTORES ------------------------------------------------------------------------------
    protected RepositorioTxt(string archivo)
    {
        if (!File.Exists(archivo) && !string.IsNullOrEmpty(archivo)) {
            File.Create(archivo).Close();
        }
    }
    #endregion
}