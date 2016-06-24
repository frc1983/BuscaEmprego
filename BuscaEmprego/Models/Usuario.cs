using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace BuscaEmprego.Database
{
    [MetadataType(typeof(UsuarioMetaData))]
    public partial class Usuario_Perfil
    {
    }

    public class UsuarioMetaData
    {
        [Required]
        public String Email { get; set; }
        [Required]
        public String Nome { get; set; }
        [Required]
        public String Senha { get; set; }
        [Required]
        [DisplayName("CPF ou CNPJ")]
        public String CPF_CNPJ { get; set; }

        [DisplayName("Tipo de Cadastro")]
        public virtual Tipo_Usuario Tipo_Usuario { get; set; }
    }
}