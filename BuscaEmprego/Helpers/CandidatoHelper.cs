using BuscaEmprego.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuscaEmprego.Helpers
{
    public class CandidatoHelper
    {
        public static ICollection<Vaga_Usuario> PopularCandidato(int idVaga)
        {
            ICollection<Vaga_Usuario> _candidato = new List<Vaga_Usuario>();
            using (var db = new BuscaEmprego.Database.BuscaEmpregoEntities())
            {
                foreach (var item in db.Vaga_Usuario.Where(x => x.Vaga_Id == idVaga))
                    _candidato.Add(item);
            }

            return _candidato;
        }
    }
}