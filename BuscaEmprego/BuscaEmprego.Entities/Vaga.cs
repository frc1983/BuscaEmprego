namespace BuscaEmprego.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Vaga")]
    public partial class Vaga
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vaga()
        {
            Vaga_Usuario = new HashSet<Vaga_Usuario>();
            Perfil = new HashSet<Perfil>();
        }

        public int Id { get; set; }

        public int Empresa_Id { get; set; }

        public int Tipo_Id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Descricao { get; set; }

        public decimal Salario { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Beneficios { get; set; }

        public bool Ativa { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data_Cadastro { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Data_Preenchimento_Vaga { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Data_Cancelamento { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Data_Ativacao { get; set; }

        public virtual Empresa Empresa { get; set; }

        public virtual Tipo Tipo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vaga_Usuario> Vaga_Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Perfil> Perfil { get; set; }
    }
}
