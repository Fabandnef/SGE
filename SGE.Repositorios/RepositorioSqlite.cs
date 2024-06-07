using Microsoft.EntityFrameworkCore;

namespace SGE.Repositorios;

abstract public class RepositorioSqlite
{
    protected SgeContext Context;
    
    #region CONSTRUCTORES ------------------------------------------------------------------------------
    protected RepositorioSqlite(SgeContext context)
    {
        Context = context;
    }
    #endregion
}