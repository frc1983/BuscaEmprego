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

        public void EditarVaga(Vaga vaga)
        {
            try
            {
                if (vaga.Salario < 0)
                    throw new BusinessException("Sálario deve ser superior a zero.");

                new VagaDAO().EditarVaga(vaga);
            }
            catch (DAOException e)
            {
                throw new Exception(e.Message);
            }
        }

        public void RemoverVaga(Vaga vaga)
        {
            try
            {
                new VagaDAO().RemoverVaga(vaga);
            }
            catch (DAOException e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Vaga BuscarVaga(int idVaga)
        {
            try
            {
                return new VagaDAO().BuscarVaga(idVaga);
            }
            catch (DAOException e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Vaga_Usuario> ListarCandidatos(int idVaga)
        {
            try
            {
                return new VagaDAO().ListarCandidatos(idVaga);
            }
            catch (DAOException e)
            {
                throw new Exception(e.Message);
            }
        }

        public void CandidatarVaga(Vaga_Usuario vagaUsuario)
        {
            try
            {
                new VagaDAO().CandidatarVaga(vagaUsuario);
            }
            catch (DAOException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
