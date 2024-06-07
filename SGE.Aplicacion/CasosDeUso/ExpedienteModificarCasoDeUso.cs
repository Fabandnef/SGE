using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;
using SGE.Aplicacion.Interfaces.Validadores;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteModificarCasoDeUso(
    IExpedienteRepositorio expedienteRepositorio,
    IExpedienteValidador   expedienteValidador,
    IServicioAutorizacion  servicioAutorizacion
)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public void Ejecutar(Expediente expediente, int idUsuario)
    {
        if (!servicioAutorizacion.PoseeElPermiso(idUsuario, PermisoEnum.ExpedienteModificacion)) {
            throw new AutorizacionException("El usuario no tiene permisos para editar un expediente.");
        }

        if (!expedienteValidador.Validar(expediente, out string error)) {
            throw new ValidacionException(error);
        }

        expediente.IdUsuarioUltimaModificacion = idUsuario;
        expediente.UpdatedAt          = DateTime.Now;
        expedienteRepositorio.Modificar(expediente);
    }
    #endregion
}