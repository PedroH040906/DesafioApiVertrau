using desafio.Entities;

namespace desafio.Model
{
    public interface IUsuarioRepository
    {
        void Add(Usuario usuario);
        List<Usuario> GetPaged(int page, int size);
        Usuario? GetById(long id);
        void Update(Usuario usuario);
        bool Delete(long id);

        // consulta usada pela regra de negócio no Service
        bool EmailExists(string email, long? ignoreId = null);
    }
}
