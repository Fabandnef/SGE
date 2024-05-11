namespace SGE.Aplicacion.Excepciones;

/// <summary>
///     Excepción que se lanza cuando ocurre un error en un repositorio.
/// </summary>
public class RepositorioException : Exception
{
    #region CONSTRUCTORES ------------------------------------------------------------------------------
    public RepositorioException() { }

    public RepositorioException(string mensaje) : base(mensaje) { }

    public RepositorioException(string mensaje, Exception causa) : base(mensaje, causa) { }
    #endregion
}