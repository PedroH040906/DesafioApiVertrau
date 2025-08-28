// Dtos/UsuarioDTO.cs
using desafio.Entities; // enum Genero

namespace desafio.DTOS
{
    public class UsuarioDTO
    {
        public string   Nome            { get; set; } = "";
        public string   SobreNome       { get; set; } = "";
        public string   Email           { get; set; } = "";
        public Genero   Genero          { get; set; }
        public DateOnly DataNascimento  { get; set; }
    }
}
