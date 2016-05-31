using BuscaEmprego.DAO;
using BuscaEmprego.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaEmprego.Business
{
    public class EmpresaBusiness
    {
        public Nullable<Int32> RegistraLogin(Empresa usuario)
        {
            try
            {
                return new EmpresaDAO().RegistraUsuario(usuario);
            }
            catch (DAOException e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Empresa BuscarEmpresa(string nome)
        {
            try
            {
                var empresa = new EmpresaDAO().BuscarEmpresa(nome);
                if (empresa == null)
                    throw new BusinessException("Nenhuma empresa encontrada.");

                return empresa;
            }
            catch (DAOException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
