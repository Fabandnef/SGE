using System.Security.Cryptography;
using System.Text;
using SGE.Aplicacion.Interfaces.Servicios;

namespace SGE.Aplicacion.Servicios;

public class ServicioDeClaves : IServicioDeClaves
{
    public string Encrypt(string plainText)
    {
        string salt = new Random().Next(1000, 9999).ToString();
        byte[] bytes = Encoding.UTF8.GetBytes(salt + plainText);
        byte[] hash = SHA512.HashData(bytes);
        return salt + Convert.ToBase64String(hash);
    }
    
    public bool Validate(string plainText, string hash)
    {
        string salt      = hash[..4];
        byte[] bytes     = Encoding.UTF8.GetBytes(salt + plainText);
        byte[] hashBytes = SHA512.HashData(bytes);
        return Convert.ToBase64String(hashBytes) == hash[4..];
    }
}