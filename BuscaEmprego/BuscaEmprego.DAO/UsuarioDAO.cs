using BuscaEmprego.Entities;
using System;
using System.Linq;

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

        public Usuario BuscarUsuario(string email)
        {
            try
            {
                using (var db = new BuscaEmprego())
                {
                    return db.Usuario.Where(x => x.Email == email).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new DAOException("Erro ao buscar usuário.");
            }
        }
    }
}
