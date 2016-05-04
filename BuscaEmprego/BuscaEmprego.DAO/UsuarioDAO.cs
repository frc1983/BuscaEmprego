using BuscaEmprego.Entities;
using System;

namespace BuscaEmprego.DAO
{
    public class UsuarioDAO
    {
        public Int32 RegistraUsuario(Usuario usuario)
        {
            try
            {
                using (var db = new BuscaEmprego())
                {
                    db.Usuario.Add(usuario);
                    db.SaveChanges();
                    return usuario.Id;
                }
            }
            catch (Exception e)
            {
                throw new DAOException("Erro ao inserir novo usuário");
            }
        }
    }
}
