using BuscaEmprego.Entities;
using BuscaEmprego.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuscaEmprego.Models
{
    public class UsuarioViewModel : RegisterViewModel
    {
        [Required]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; }

        [Display(Name = "CPF")]
        public long CPF { get; set; }

        [Display(Name = "CNPJ")]
        public long CNPJ { get; set; }

        [Display(Name = "Tipo de Usuário")]
        public EnumTipoUsuario TipoUsuario { get; set; }

        public EnderecoViewModel Endereco { get; set; }

        public static Usuario ParseUsuarioToEntity(UsuarioViewModel vm)
        {
            return new Usuario()
            {
                Nome = vm.Nome,
                CPF = vm.CPF,
                Email = vm.Email,
                Senha = vm.Password,
                Endereco = EnderecoViewModel.ParseEnderecoToEntity(vm.Endereco)
            };
        }

        public static Empresa ParseEmpresaToEntity(UsuarioViewModel vm)
        {
            return new Empresa()
            {
                Nome = vm.Nome,
                CNPJ = vm.CNPJ,
                Email = vm.Email,
                Senha = vm.Password,
                Endereco = EnderecoViewModel.ParseEnderecoToEntity(vm.Endereco)
            };
        }
    }
}