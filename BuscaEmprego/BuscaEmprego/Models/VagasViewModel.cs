using BuscaEmprego.Entities;
using BuscaEmprego.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BuscaEmprego.Business;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BuscaEmprego.Models
{
    public class VagasViewModel : ResultadoViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Salario")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public String Salario { get; set; }

        [Required]
        [Display(Name = "Beneficios")]
        public string Beneficios { get; set; }

        [Display(Name = "Tipo de Vaga")]
        public EnumTipoVaga TipoVaga { get; set; }

        [Display(Name = "Ativa")]
        public bool Ativa { get; set; }

        public String EmailEmpresa { get; set; }

        public static Vaga ParseVagaToEntity(VagasViewModel vm)
        {
            if (vm.Id == 0)
                return VagasBusiness.BuscarVaga(vm.Id);

            Empresa empresa = EmpresaBusiness.BuscarEmpresa(vm.EmailEmpresa);
            Tipo tipo = TipoBusiness.BuscarTipoVaga((int)vm.TipoVaga);
            var salario = decimal.Parse(vm.Salario.Replace("R$", "").Replace(".", "").Trim());
            
            var vaga = new Vaga()
            {
                Descricao = vm.Descricao,
                Salario = salario,
                Beneficios = vm.Beneficios,
                Tipo = tipo,
                Ativa = false,
                Data_Cadastro = DateTime.Now,
                Empresa = empresa
            };

            return vaga;
        }

        public static Vaga_Usuario ParseVagaToVagaUsuario(VagasViewModel vm)
        {
            Usuario usuario = UsuarioBusiness.BuscarUsuario(vm.EmailEmpresa);
            Vaga vaga = VagasBusiness.BuscarVaga(vm.Id);

            var vagaUsuario = new Vaga_Usuario()
            {
                Usuario = usuario,
                Vaga = vaga,
                Data_Candidatura = DateTime.Now
            };

            return vagaUsuario;
        }
    }
}