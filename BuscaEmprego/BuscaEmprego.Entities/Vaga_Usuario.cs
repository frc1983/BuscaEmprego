namespace BuscaEmprego.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Vaga_Usuario
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Vaga_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Usuario_Id { get; set; }

        public bool Aprovado { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data_Candidatura { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data_Aprovacao { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Vaga Vaga { get; set; }
    }
}
