using BuscaEmprego.Entities;
using System;

namespace BuscaEmprego.DAO
{
    public class VagaDAO
    {
        public Int32 RegistraVaga(Vaga vaga)
        {
            try
            {
                using (var db = new BuscaEmprego())
                {
                    db.Vaga.Add(vaga);
                    db.SaveChanges();
                    return vaga.Id;
                }
            }
            catch (Exception e)
            {
                throw new DAOException("Erro ao inserir nova vaga.");
            }
        }
    }
}
