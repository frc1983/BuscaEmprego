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
                if (vaga.Salario < 0)
                    throw new BusinessException("Sálario deve ser superior a zero.");

                return new VagaDAO().RegistraVaga(vaga);
            }
            catch (DAOException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
