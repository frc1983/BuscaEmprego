namespace BuscaEmprego.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Empresa")]
    public partial class Empresa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empresa()
        {
            Vaga = new HashSet<Vaga>();
        }

        public int Id { get; set; }

        public int Endereco_Id { get; set; }

        public long CNPJ { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        public virtual Endereco Endereco { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vaga> Vaga { get; set; }
    }
}
