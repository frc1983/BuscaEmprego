using BuscaEmprego.DAO;
using BuscaEmprego.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaEmprego.Business
{
    public class TipoBusiness
    {
        public static Tipo BuscarTipoVaga(int tipoVaga)
        {
            try
            {
                return new TipoDAO().BuscarTipoVaga(tipoVaga);
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
