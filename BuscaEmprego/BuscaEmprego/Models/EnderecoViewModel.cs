using BuscaEmprego.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuscaEmprego.Models
{
    public class EnderecoViewModel
    {
        [Required]
        [Display(Name = "CEP")]
        public int CEP { get; set; }

        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Display(Name = "Telefone")]
        public int Telefone { get; set; }

        [Display(Name = "Tipo (Casa, Apartamento, Comércio)")]
        public string Tipo { get; set; }

        public static Endereco ParseEnderecoToEntity(EnderecoViewModel vm)
        {
            return new Endereco()
            {
                CEP = vm.CEP,
                Complemento = vm.Complemento,
                Logradouro = vm.Logradouro,
                Telefone = vm.Telefone,
                Tipo = vm.Tipo,
            };
        }
    }
}