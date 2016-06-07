namespace BuscaEmprego.DAO
{
    using Entities;
    using System.Data.Entity;

    public partial class BuscaEmprego : DbContext
    {
        public BuscaEmprego()
            : base("name=BuscaEmprego")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Tipo> Tipo { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vaga> Vaga { get; set; }
        public virtual DbSet<Vaga_Usuario> Vaga_Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.Vaga)
                .WithRequired(e => e.Empresa)
                .HasForeignKey(e => e.Empresa_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Endereco>()
                .Property(e => e.Logradouro)
                .IsUnicode(false);

            modelBuilder.Entity<Endereco>()
                .Property(e => e.Complemento)
                .IsUnicode(false);

            modelBuilder.Entity<Endereco>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Endereco>()
                .HasMany(e => e.Empresa)
                .WithRequired(e => e.Endereco)
                .HasForeignKey(e => e.Endereco_Id);

            modelBuilder.Entity<Endereco>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.Endereco)
                .HasForeignKey(e => e.Endereco_Id)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Perfil>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Perfil>()
                .HasMany(e => e.Usuario)
                .WithMany(e => e.Perfil)
                .Map(m => m.ToTable("Usuario_Perfil"));

            modelBuilder.Entity<Perfil>()
                .HasMany(e => e.Vaga)
                .WithMany(e => e.Perfil)
                .Map(m => m.ToTable("Vaga_Perfil"));

            modelBuilder.Entity<Tipo>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Tipo>()
                .HasMany(e => e.Vaga)
                .WithRequired(e => e.Tipo)
                .HasForeignKey(e => e.Tipo_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Vaga_Usuario)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.Usuario_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vaga>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Vaga>()
                .Property(e => e.Salario)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Vaga>()
                .Property(e => e.Beneficios)
                .IsUnicode(false);

            modelBuilder.Entity<Vaga>()
                .HasMany(e => e.Vaga_Usuario)
                .WithRequired(e => e.Vaga)
                .HasForeignKey(e => e.Vaga_Id)
                .WillCascadeOnDelete(false);
        }
    }
}
