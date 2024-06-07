using Microsoft.EntityFrameworkCore;
using SGE.Aplicacion.CasosDeUso;
using SGE.Aplicacion.Entidades;
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
        SgeSqlite.SetUp();

        using SgeContext  contexto         = new();
        IServicioDeClaves servicioDeClaves = new ServicioDeClaves();

        Expediente expediente = contexto.Expedientes
                                        .Include("UsuarioUltimaModificacion.Permisos")
                                        .Include("Tramites")
                                        .First();
    }
    #endregion
}