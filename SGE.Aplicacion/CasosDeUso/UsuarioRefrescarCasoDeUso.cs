using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Aplicacion.CasosDeUso;

public class UsuarioRefrescarCasoDeUso(IRepositorioUsuario repositorioUsuario)
{
    public Usuario Ejecutar(Usuario usuario) => repositorioUsuario.Refresh(usuario)!;
}