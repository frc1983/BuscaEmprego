namespace BuscaEmprego.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Endereco")]
    public partial class Endereco
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Endereco()
        {
            Usuario = new HashSet<Usuario>();
            Empresa = new HashSet<Empresa>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Logradouro { get; set; }

        [Required]
        [StringLength(255)]
        public string Complemento { get; set; }

        public int CEP { get; set; }

        [Required]
        [StringLength(255)]
        public string Tipo { get; set; }

        public long Telefone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empresa> Empresa { get; set; }
    }
}
