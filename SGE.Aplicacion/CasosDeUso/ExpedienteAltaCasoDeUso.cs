using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces;
using SGE.Aplicacion.Validadores;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteAltaCasoDeUso(
    IExpedienteRepositorio repositorio,
    ExpedienteValidador    validador,
    IServicioAutorizacion  servicioAutorizacion
)
{
    public void Ejecutar(Expediente expediente, int idUsuario)
    {
        if (!validador.ValidarExpediente(expediente, out string message)) {
            throw new ValidacionException(message);
        }

        /* Según la página 5:
         * ... Se debe tener en cuenta que las fechas de creación y modificación de estas entidades (Tramites y Expedientes) serán
         * establecidas por el respectivo caso de uso, sobrescribiendo las fechas del objeto a persistir recibido como
         * parámetro ...
         *
         * Seteamos las fechas de creación y modificación.
         */

        expediente.FechaCreacion               = DateTime.Now;
        expediente.UltimaModificacion          = DateTime.Now;
        expediente.IdUsuarioUltimaModificacion = idUsuario;

        // Verificar acá o en el repositorio?
        if (!servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.ExpedienteAlta)) {
            throw new AutorizacionException("El usuario no tiene permisos para dar de alta un expediente");
        }
            
        repositorio.ExpedienteAlta(expediente);
    }
}