using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class UsuarioBuscarPorIdCasoDeUso(
    IRepositorioUsuario repositorioUsuario
)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public Usuario? Ejecutar(int idUsuario)
    {
        Usuario? usuario = repositorioUsuario.BuscarPorId(idUsuario);

        return usuario;
    }
    #endregion
}