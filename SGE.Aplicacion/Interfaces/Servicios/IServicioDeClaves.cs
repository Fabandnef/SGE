namespace SGE.Aplicacion.Interfaces.Servicios;

public interface IServicioDeClaves
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    string Encrypt(string plainText);

    bool Validate(string plainText, string hash);
    #endregion
}