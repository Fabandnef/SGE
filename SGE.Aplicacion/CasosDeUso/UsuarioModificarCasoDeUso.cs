using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;
using SGE.Aplicacion.Interfaces.Validadores;

namespace SGE.Aplicacion.CasosDeUso;

public class UsuarioModificarCasoDeUso(
    IRepositorioUsuario   repositorioUsuario,
    IServicioAutorizacion servicioAutorizacion,
    IServicioDeClaves     servicioDeClaves,
    IUsuarioValidador     usuarioValidador
)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public void Ejecutar(Usuario usuario, Usuario usuarioActual, bool passwordCambiada)
    {
        if (!servicioAutorizacion.PoseeElPermiso(usuarioActual, PermisoEnum.AdminGeneral) &&
            (usuario.Id != usuarioActual.Id)) {
            throw new AutorizacionException("El usuario no tiene permisos para realizar esta acción.");
        }

        if (!usuarioValidador.Validar(usuario, out string error)) {
            throw new ValidacionException(error);
        }

        if (passwordCambiada) {
            usuario.Password = servicioDeClaves.Encrypt(usuario.Password);
        }

        repositorioUsuario.Update(usuario);
    }
    #endregion
}