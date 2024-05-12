using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;
using SGE.Aplicacion.Interfaces.Validadores;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteAltaCasoDeUso(
    IExpedienteRepositorio expedienteRepositorio,
    IExpedienteValidador   expedienteValidador,
    IServicioAutorizacion  servicioAutorizacion
)
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public void Ejecutar(Expediente expediente, int idUsuario)
    {
        if (!servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.ExpedienteAlta)) {
            throw
                new AutorizacionException($"El usuario {idUsuario} no tiene permisos para dar de alta un expediente.");
        }

        if (!expedienteValidador.Validar(expediente, out string error)) {
            throw new ValidacionException(error);
        }

        expediente.FechaCreacion               = DateTime.Now;
        expediente.UltimaModificacion          = DateTime.Now;
        expediente.IdUsuarioUltimaModificacion = idUsuario;
        expedienteRepositorio.Alta(expediente);
    }
    #endregion
}