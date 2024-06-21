namespace SGE.Aplicacion.Interfaces.Repositorios;

public interface ITimestampable
{
    #region PROPIEDADES PUBLICAS -----------------------------------------------------------------------
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    #endregion
}