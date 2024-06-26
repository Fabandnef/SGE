﻿using SGE.Aplicacion.Entidades;

namespace SGE.Aplicacion.Interfaces.Repositorios;

public interface IRepositorioUsuario
{
    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public Usuario? BuscarPorId(int idUsuario);

    public int ContarTotal();

    public void Delete(Usuario usuario);

    public Usuario? GetUsuario(string email);

    public List<Usuario> GetUsuarios(int  page);
    public void          Register(Usuario usuario);

    public void Update(Usuario usuario);
    #endregion
}