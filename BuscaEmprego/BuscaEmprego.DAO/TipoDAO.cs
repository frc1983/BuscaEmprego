using BuscaEmprego.Entities;
using System;

namespace BuscaEmprego.DAO
{
    public class TipoDAO
    {
        public Tipo BuscarTipoVaga (int tipoVaga)
        {
            try
            {
                using (var db = new BuscaEmprego())
                {
                    return db.Tipo.Find(tipoVaga);
                }
            }
            catch (Exception e)
            {
                throw new DAOException("Erro ao buscar tipo da vaga.");
            }
        }
    }
}
