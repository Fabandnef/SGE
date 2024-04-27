using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteBajaCasoDeUso(
    IExpedienteRepositorio repositorioExpediente,
    ITramiteRepositorio    repositorioTramite,
    IServicioAutorizacion  servicioAutorizacion
)
{
    public void Ejecutar(int idExpediente, int idUsuario)
    {
        if (!servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.ExpedienteBaja)) {
            throw new AutorizacionException("El usuario no tiene permisos para dar de baja un expediente");
        }
        
        if (!repositorioExpediente.ExpedienteBaja(idExpediente)) {
            throw new RepositorioException("El expediente a eliminar no existe");
        }
        
        repositorioTramite.BajaTramitePorExpediente(idExpediente);
    }
}