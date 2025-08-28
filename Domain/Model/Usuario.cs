// desafio.Entities/Usuario.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace desafio.Entities
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public long Id { get; set; }

        public string Nome { get; set; } = "";
        public string SobreNome { get; set; } = "";
        public string Email { get; set; } = "";
        public Genero Genero { get; set; }
        public DateOnly DataNascimento { get; set; }

        public Usuario() {} //ctor vazio ajuda o EF

        public Usuario(string nome, string sobreNome, string email, Genero genero, DateOnly dataNascimento)
        {
            Nome = nome;
            SobreNome = sobreNome;
            Email = email;
            Genero = genero;
            DataNascimento = dataNascimento;
        }
    }
}
