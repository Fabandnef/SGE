using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Enumerativos;
using SGE.Aplicacion.Interfaces.Servicios;
using SGE.Aplicacion.Servicios;

namespace SGE.Repositorios;

static public class SgeSqlite
{
    static public void SetUp()
    {
        using SgeContext contexto = new();

        try {
            contexto.Database.EnsureDeleted();

            if (!contexto.Database.EnsureCreated()) {
                return;
            }

            AddPermisos(contexto);
            SeedData(contexto);
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

    static private void SeedData(SgeContext contexto)
    {
        IServicioDeClaves servicioDeClaves = new ServicioDeClaves();

        contexto.Usuarios.Add(new Usuario {
                                              Nombre   = "Fabrizio",
                                              Apellido = "Girardi",
                                              Email    = "fabro.la22@gmail.com",
                                              Password = servicioDeClaves.Encrypt("123456"),
                                              Permisos = [
                                                             contexto.Permisos
                                                                     .First(p => p.Nombre == PermisoEnum
                                                                                            .AdminGeneral
                                                                                            .ToString()
                                                                           ),
                                                         ],
                                          });

        contexto.SaveChanges();

        Usuario usuario = contexto.Usuarios
                                  .First(u => u.Email == "fabro.la22@gmail.com");

        contexto.Expedientes
                .AddRange(new Expediente {
                                             Caratula                    = "Expediente de prueba",
                                             IdUsuarioUltimaModificacion = usuario.Id,
                                         });

        contexto.SaveChanges();

        Expediente expediente = contexto.Expedientes
                                        .First(e => e.Id == 1);

        contexto.Tramites
                .AddRange(new Tramite {
                                          ExpedienteId                = expediente.Id,
                                          Contenido                   = "Trámite de prueba",
                                          IdUsuarioUltimaModificacion = usuario.Id,
                                      });

        contexto.SaveChanges();
    }
}