using desafio.Entities;

namespace desafio.Application.ViewModel   // <- ESTE é o namespace
{
    public class UsuarioViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; } = "";
        public string SobreNome { get; set; } = "";
        public string Email { get; set; } = "";
        public Genero Genero { get; set; }
        public DateOnly DataNascimento { get; set; }
    }
}
