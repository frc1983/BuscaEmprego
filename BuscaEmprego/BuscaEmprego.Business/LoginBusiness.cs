using BuscaEmprego.DAO;
using BuscaEmprego.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaEmprego.Business
{
    public class UsuarioBusiness
    {
        public Nullable<Int32> RegistraLogin(Usuario usuario)
        {
            try
            {
                return new UsuarioDAO().RegistraUsuario(usuario);
            }            
            catch (BusinessException e)
            {
                throw new Exception(e.Message);
            }
            catch (DAOException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
