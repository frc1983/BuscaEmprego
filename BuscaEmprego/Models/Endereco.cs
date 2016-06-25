using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuscaEmprego.Database
{
    [MetadataType(typeof(EnderecoMetaData))]
    public partial class Endereco
    {
    }

    public class EnderecoMetaData
    {
        [Required]
        public String Logradouro { get; set; }
    }
}