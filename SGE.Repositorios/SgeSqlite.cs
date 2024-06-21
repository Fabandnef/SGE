using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;

namespace SGE.Repositorios;

static public class SgeSqlite
{
    static public void SetUp()
    {
        using SgeContext context = new();

        try {
//            context.Database.EnsureDeleted();

            if (!context.Database.EnsureCreated()) {
                return;
            }

            DbConnection connection = context.Database.GetDbConnection();
            connection.Open();

            using (DbCommand command = connection.CreateCommand()) {
                command.CommandText = "PRAGMA journal_mode=DELETE;";
                command.ExecuteNonQuery();
            }

            AddPermisos(context);
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }

    static private void AddPermisos(SgeContext contexto)
    {
        contexto.Permisos
                .AddRange(
                          new Permiso {
                                          Nombre = PermisoEnum.AdminGeneral.ToString(),
                                          Descripcion
                                              = "Nivel administrativo máximo. Puede hacer todo, incluso editar otros usuarios y sus permisos.",
                                      },
                          new Permiso {
                                          Nombre      = PermisoEnum.ExpedienteAlta.ToString(),
                                          Descripcion = "Alta de un expediente nuevo.",
                                      },
                          new Permiso {
                                          Nombre      = PermisoEnum.ExpedienteBaja.ToString(),
                                          Descripcion = "Baja de un expediente.",
                                      },
                          new Permiso {
                                          Nombre      = PermisoEnum.ExpedienteModificacion.ToString(),
                                          Descripcion = "Modificación de un expediente.",
                                      },
                          new Permiso {
                                          Nombre      = PermisoEnum.TramiteAlta.ToString(),
                                          Descripcion = "Alta de un trámite nuevo.",
                                      },
                          new Permiso {
                                          Nombre      = PermisoEnum.TramiteBaja.ToString(),
                                          Descripcion = "Baja de un trámite",
                                      },
                          new Permiso {
                                          Nombre      = PermisoEnum.TramiteModificacion.ToString(),
                                          Descripcion = "Modificación de un trámite",
                                      }
                         );

        contexto.SaveChanges();
    }
}