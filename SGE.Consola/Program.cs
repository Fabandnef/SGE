using SGE.Aplicacion.CasosDeUso;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;
using SGE.Aplicacion.Interfaces.Validadores;
using SGE.Aplicacion.Servicios;
using SGE.Aplicacion.Validadores;
using SGE.Repositorios;

namespace SGE.Consola;

public class Program
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    static public void Main(string[] args)
    {
        // COMPLETAR CON EL CÓDIGO DESEADO DE LOS EJEMPLOS.
    }
    #endregion

    #region METODOS ------------------------------------------------------------------------------------
    static private string GenerarFrase(int palabras = 5)
    {
        Random obj = new();
        string st  = string.Empty;
        int    letras;

        for (int i = 0; i < palabras; i++) {
            letras =  obj.Next(5, 10);
            st     += GenerarPalabra(letras) + " ";
        }

        return st.TrimEnd();
    }

    static private string GenerarPalabra(int longitud = 5)
    {
        Random       obj            = new();
        const string letras         = "abcdefghijklmnopqrstuvwxyz";
        int          cantidadLetras = letras.Length;
        string       st             = letras[obj.Next(longitud)].ToString().ToUpper();

        for (int i = 0; i < longitud; i++) {
            st += letras[obj.Next(cantidadLetras)].ToString();
        }

        return st;
    }
    #endregion
}