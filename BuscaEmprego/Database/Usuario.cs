//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        public Usuario()
        {
            this.Vaga = new HashSet<Vaga>();
            this.Vaga_Usuario = new HashSet<Vaga_Usuario>();
            this.Perfil = new HashSet<Perfil>();
        }
    
        public int Id { get; set; }
        public int Endereco_Id { get; set; }
        public int Tipo_Usuario_Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string CPF_CNPJ { get; set; }
    
        public virtual Endereco Endereco { get; set; }
        public virtual Tipo_Usuario Tipo_Usuario { get; set; }
        public virtual ICollection<Vaga> Vaga { get; set; }
        public virtual ICollection<Vaga_Usuario> Vaga_Usuario { get; set; }
        public virtual ICollection<Perfil> Perfil { get; set; }
    }
}