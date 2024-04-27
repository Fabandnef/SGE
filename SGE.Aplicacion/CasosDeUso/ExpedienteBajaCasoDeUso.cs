using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Interfaces;

namespace SGE.Aplicacion.CasosDeUso;

public class ExpedienteBajaCasoDeUso(IExpedienteRepositorio repositorio, IServicioAutorizacion servicioAutorizacion)
{
    public void Ejecutar(int idExpediente, int idUsuario)
    {
        // TODO: Verificar si existe el expediente y tirar "RepositorioException" si no existe (Está bien así?)

        // Variable auxiliar debido al warning "Expression is always false according to nullable reference types' annotations"
        Expediente? expedienteExiste = repositorio.ExpedienteBuscarPorId(idExpediente); 

        if (expedienteExiste == null) {
            throw new RepositorioException("El expediente a eliminar no existe");
        }

        // Verificar acá o en el repositorio?
        // TODO: Preguntar si verifico por el permiso en el repositorio o en el caso de uso.
        if (servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.ExpedienteBaja)) {
            repositorio.ExpedienteBaja(idExpediente); // TODO: Consultar si hay que pasar el usuario o se usa el actual del expediente.
        } else {
            throw new AutorizacionException("El usuario no tiene permisos para dar de baja un expediente");
        }
    }
}