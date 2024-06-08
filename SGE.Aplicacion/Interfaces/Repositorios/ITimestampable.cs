namespace SGE.Aplicacion.Interfaces.Repositorios;

public interface ITimestampable
{
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}