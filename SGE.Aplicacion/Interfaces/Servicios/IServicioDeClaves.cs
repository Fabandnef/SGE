namespace SGE.Aplicacion.Interfaces.Servicios;

public interface IServicioDeClaves
{
    string Encrypt(string plainText);
    
    bool Validate(string plainText, string hash);
}