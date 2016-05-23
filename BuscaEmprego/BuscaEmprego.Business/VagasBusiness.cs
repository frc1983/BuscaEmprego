using BuscaEmprego.DAO;
using BuscaEmprego.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaEmprego.Business
{
    public class VagasBusiness
    {
        public Nullable<Int32> RegistraVaga(Vaga vaga)
        {
            try
            {
                return new VagaDAO().RegistraVaga(vaga);
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
