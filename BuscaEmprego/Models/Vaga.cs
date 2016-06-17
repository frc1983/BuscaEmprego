using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuscaEmprego.Database
{
    [MetadataType(typeof(VagaMetaData))]
    public partial class Vaga
    {
    }

    public class VagaMetaData
    {
        [Required]
        public int Tipo_Id { get; set; }
        [Required]
        public String Descricao { get; set; }
        [Required]
        public String Beneficios { get; set; }
    }
}