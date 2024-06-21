using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class UsuarioContarTotalCasoDeUso(IRepositorioUsuario repositorioUsuario)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public int Ejecutar() => repositorioUsuario.ContarTotal();
    #endregion
}