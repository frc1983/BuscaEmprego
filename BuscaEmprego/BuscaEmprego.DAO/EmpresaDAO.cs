using BuscaEmprego.Entities;
using System;

namespace BuscaEmprego.DAO
{
    public class EmpresaDAO
    {
        public Int32 RegistraUsuario(Empresa usuario)
        {
            try
            {
                using (var db = new BuscaEmprego())
                {
                    db.Empresa.Add(usuario);
                    db.SaveChanges();
                    return usuario.Id;
                }
            }
            catch (Exception e)
            {
                throw new DAOException("Erro ao inserir novo usuário empresarial.");
            }
        }

        public Empresa BuscarEmpresa(string nome)
        {
            try
            {
                using (var db = new BuscaEmprego())
                {
                    return db.Empresa.Find(nome);
                }
            }
            catch (Exception e)
            {
                throw new DAOException("Erro ao buscar empresa.");
            }
        }
    }
}
