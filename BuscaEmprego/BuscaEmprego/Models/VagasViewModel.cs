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
    public class VagasViewModel : RegisterViewModel
    {
        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Salario")]
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal Salario { get; set; }

        [Display(Name = "Beneficios")]
        public string Beneficios { get; set; }

        [Display(Name = "Tipo de Vaga")]
        public EnumTipoVaga TipoVaga { get; set; }

        [Display(Name = "Ativa")]
        public bool Ativa { get; set; }

        public Empresa Empresa { get; set; }

        public static Vaga ParseVagaToEntity(VagasViewModel vm)
        {
            Empresa empresa = EmpresaBusiness.BuscarEmpresa(System.Environment.UserName);
            Tipo tipo = VagasBusiness.BuscarTipoVaga((int)vm.TipoVaga);
            return new Vaga()
            {
                Descricao = vm.Descricao,
                Salario = vm.Salario,
                Beneficios = vm.Beneficios,
                Tipo = tipo,
                Ativa = true,
                Data_Cadastro = new DateTime(),
                Data_Ativacao = new DateTime(),
                Empresa = empresa
            };
        }
    }
}