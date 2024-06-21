using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;
using SGE.Aplicacion.Interfaces.Validadores;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteAltaCasoDeUso(
    IRepositorioExpediente repositorioExpediente,
    IExpedienteValidador   expedienteValidador,
    IServicioAutorizacion  servicioAutorizacion
)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public void Ejecutar(Expediente expediente, Usuario usuario)
    {
        if (!servicioAutorizacion.PoseeElPermiso(usuario, PermisoEnum.ExpedienteAlta)) {
            throw
                new
                    AutorizacionException($"El usuario {usuario.Nombre} {usuario.Apellido} no tiene permisos para dar de alta un expediente.");
        }

        if (!expedienteValidador.Validar(expediente, out string error)) {
            throw new ValidacionException(error);
        }

        expediente.IdUsuarioUltimaModificacion = usuario.Id;
        repositorioExpediente.Alta(expediente);
    }
    #endregion
}