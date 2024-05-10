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
    public Expediente Ejecutar(Expediente expediente, int idUsuario)
    {
        if (!expedienteValidador.Validar(expediente, out string error)) {
            throw new ValidacionException(error);
        }

        if (!servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.ExpedienteAlta)) {
            throw
                new AutorizacionException($"El usuario {idUsuario} no tiene permisos para dar de alta un expediente.");
        }

        expediente.FechaCreacion               = DateTime.Now;
        expediente.UltimaModificacion          = DateTime.Now;
        expediente.IdUsuarioUltimaModificacion = idUsuario;

        Expediente e = expedienteRepositorio.Alta(expediente);

        return e;
    }
}