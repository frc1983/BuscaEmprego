using BuscaEmprego.Entities;
using System;
using System.Linq;

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

        public Empresa BuscarEmpresa(string email)
        {
            try
            {
                using (var db = new BuscaEmprego())
                {
                    return db.Empresa.Where(x => x.Email == email).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new DAOException("Erro ao buscar empresa.");
            }
        }

        public Empresa BuscarEmpresa(int id)
        {
            try
            {
                using (var db = new BuscaEmprego())
                {
                    return db.Empresa.Where(x => x.Id == id).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new DAOException("Erro ao buscar empresa.");
            }
        }
    }
}
