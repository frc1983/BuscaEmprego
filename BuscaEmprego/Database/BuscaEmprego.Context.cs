﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BuscaEmprego.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BuscaEmpregoEntities : DbContext
    {
        public BuscaEmpregoEntities()
            : base("name=BuscaEmpregoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Tipo> Tipo { get; set; }
        public DbSet<Tipo_Usuario> Tipo_Usuario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Vaga> Vaga { get; set; }
        public DbSet<Vaga_Usuario> Vaga_Usuario { get; set; }
    }
}
