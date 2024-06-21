using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class UsuarioListarCasoDeUso(IRepositorioUsuario repositorioUsuario)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public List<Usuario> Ejecutar(int page) => repositorioUsuario.GetUsuarios(page);
    #endregion
}