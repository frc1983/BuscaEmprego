using BuscaEmprego.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuscaEmprego.DAO
{
    public class PerfilDAO
    {

        public List<Perfil> BuscarPerfis()
        {
            try
            {
                using (var db = new BuscaEmprego())
                {
                    return db.Perfil.ToList();
                }
            }
            catch (Exception e)
            {
                throw new DAOException("Erro ao buscar perfis.");
            }
        }
    }
}
