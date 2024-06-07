using Microsoft.EntityFrameworkCore;

namespace SGE.Repositorios;

abstract public class RepositorioSqlite(SgeContext context)
{
    readonly protected SgeContext Context = context;
}