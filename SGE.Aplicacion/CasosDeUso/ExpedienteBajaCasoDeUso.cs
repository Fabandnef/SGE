using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces.Repositorios;
using SGE.Aplicacion.Interfaces.Servicios;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteBajaCasoDeUso(
    IExpedienteRepositorio expedienteRepositorio,
    ITramiteRepositorio    tramiteRepositorio,
    IServicioAutorizacion  servicioAutorizacion
)
{
    public void Ejecutar(int idExpediente, int idUsuario)
    {
        if (!servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.ExpedienteBaja)) {
            throw new AutorizacionException("El usuario no tiene permisos para dar de baja un expediente.");
        }

        if (!expedienteRepositorio.Baja(idExpediente)) {
            throw new RepositorioException("El expediente a eliminar no existe.");
        }

        tramiteRepositorio.BajaPorExpediente(idExpediente);
        Console.WriteLine($"Expediente con id {idExpediente} eliminado correctamente junto con sus trámites.");
    }
}