using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces;
using SGE.Aplicacion.Validadores;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteModificarCasoDeUso(
    IExpedienteRepositorio repositorio,
    ExpedienteValidador    validador,
    IServicioAutorizacion  servicioAutorizacion
)
{
    public void Ejecutar(Expediente expedienteNuevo, int idUsuario)
    {
        if (!validador.ValidarExpediente(expedienteNuevo, out string message1)) {
            throw new ValidacionException(message1);
        }

        if (!servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.ExpedienteModificacion)) {
            throw new AutorizacionException("El usuario no tiene permisos para editar un expediente");
        }

        expedienteNuevo.IdUsuarioUltimaModificacion = idUsuario;
        expedienteNuevo.UltimaModificacion          = DateTime.Now;
        repositorio.ExpedienteModificar(expedienteNuevo);
    }
}