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
    public void Ejecutar(Expediente expediente, int idUsuario)
    {
        if (!expedienteValidador.Validar(expediente, out string error)) {
            throw new ValidacionException(error);
        }

        if (!servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.ExpedienteModificacion)) {
            throw new AutorizacionException("El usuario no tiene permisos para editar un expediente.");
        }

        expediente.IdUsuarioUltimaModificacion = idUsuario;
        expediente.UltimaModificacion          = DateTime.Now;
        expedienteRepositorio.Modificar(expediente);
        Console.WriteLine($"Expediente {expediente.Id} modificado correctamente.");
    }
}