using BuscaEmprego.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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
            catch (Exception)
            {
                throw new DAOException("Erro ao inserir nova vaga.");
            }
        }

        public void EditarVaga(Vaga vaga)
        {
            try
            {
                using (var db = new BuscaEmprego())
                {
                    db.Vaga.Add(vaga);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new DAOException("Erro ao editar vaga.");
            }
        }

        public List<Vaga> ListaVagas(int tipoVaga, string query)
        {
            try
            {
                List<Vaga> retorno = new List<Vaga>();
                using (var db = new BuscaEmprego())
                {
                    var vaga = db.Vaga.Where(x => x.Ativa && x.Tipo_Id == tipoVaga);

                    if (!string.IsNullOrEmpty(query))
                        vaga = vaga.Where(x => x.Descricao.Contains(query) ||
                            x.Beneficios.Contains(query));

                    retorno = vaga.ToList();                    
                }

                return retorno;
            }
            catch (Exception e)
            {
                throw new DAOException("Erro ao buscar vagas.");
            }
        }

        public void RemoverVaga(int idVaga)
        {
            try
            {
                using (var db = new BuscaEmprego())
                {
                    db.Vaga.Remove(BuscarVaga(idVaga));
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new DAOException("Erro ao remover vaga.");
            }
        }

        public Vaga BuscarVaga(int idVaga)
        {
            try
            {
                using (var db = new BuscaEmprego())
                {
                    return db.Vaga.Find(idVaga);
                }
            }
            catch (Exception e)
            {
                throw new DAOException("Erro ao buscar vaga.");
            }
        }

        public void CandidatarVaga(Vaga_Usuario vagaUsuario)
        {
            try
            {
                using (var db = new BuscaEmprego())
                {
                    db.Vaga_Usuario.Add(vagaUsuario);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new DAOException("Erro ao candidatar-se a vaga.");
            }
        }

        public List<Vaga_Usuario> ListarCandidatos(int idVaga)
        {
            try
            {
                using (var db = new BuscaEmprego())
                {
                    return db.Vaga_Usuario.Where(x => x.Vaga_Id == idVaga).ToList();
                }
            }
            catch (Exception e)
            {
                throw new DAOException("Erro ao buscar vaga.");
            }
        }
    }
}
