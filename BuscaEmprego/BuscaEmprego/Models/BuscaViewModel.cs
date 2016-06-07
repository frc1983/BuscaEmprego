using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuscaEmprego.Models
{
    public class BuscaViewModel : ResultadoViewModel
    {
        [Display(Name = "Tipo de Vaga")]
        public int TipoVaga { get; set; }
        [Display(Name = "Palavras-chave")]
        public String Query { get; set; }
        public List<VagasViewModel> Vagas { get; set; }

        public BuscaViewModel()
        {
            Vagas = new List<VagasViewModel>();
        }
    }
}