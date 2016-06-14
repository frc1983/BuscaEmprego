using BuscaEmprego.Business;
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
        public UsuarioViewModel()
        {
            Perfis = new List<PerfilViewModel>();

            var perfis = new PerfilBusiness().BuscarPerfis();
            foreach (var item in perfis)
                Perfis.Add(new PerfilViewModel() { Checked = false, Name = item.Nome, Value = item.Id.ToString() });

        }

        [Required]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "CPF")]
        public String CPF { get; set; }

        [Required]
        [Display(Name = "CNPJ")]
        public String CNPJ { get; set; }

        [Display(Name = "Tipo de Usuário")]
        public EnumTipoUsuario TipoUsuario { get; set; }

        public EnderecoViewModel Endereco { get; set; }

        public IList<PerfilViewModel> Perfis { get; set; }

        public static Usuario ParseUsuarioToEntity(UsuarioViewModel vm)
        {
            return new Usuario()
            {
                Nome = vm.Nome,
                CPF = long.Parse(vm.CPF),
                Email = vm.Email,
                Senha = vm.Password,
                Endereco = EnderecoViewModel.ParseEnderecoToEntity(vm.Endereco),
                Perfil = PerfilViewModel.ParseToEntity(vm.Perfis)
            };
        }

        public static Empresa ParseEmpresaToEntity(UsuarioViewModel vm)
        {
            return new Empresa()
            {
                Nome = vm.Nome,
                CNPJ = long.Parse(vm.CNPJ),
                Email = vm.Email,
                Senha = vm.Password,
                Endereco = EnderecoViewModel.ParseEnderecoToEntity(vm.Endereco)
            };
        }
    }
}