using desafio.Entities;
using desafio.Model;
using desafio.Data;                 // <-- para ApplicationDbContext
using Microsoft.EntityFrameworkCore;

namespace desafio.Infrastructure.Repositories  // <-- use este namespace
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public List<Usuario> GetPaged(int page, int size)
        {
            if (page < 1) page = 1;
            if (size < 1) size = 10;

            return _context.Usuarios
                .AsNoTracking()
                .OrderBy(u => u.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();
        }

        public Usuario? GetById(long id) =>
            _context.Usuarios.AsNoTracking().FirstOrDefault(u => u.Id == id);

        public void Update(Usuario usuario)
        {
            var db = _context.Usuarios.Find(usuario.Id);
            if (db is null) return;

            db.Nome           = usuario.Nome;
            db.SobreNome      = usuario.SobreNome;
            db.Email          = usuario.Email;
            db.Genero         = usuario.Genero;
            db.DataNascimento = usuario.DataNascimento;

            _context.SaveChanges();
        }

        public bool Delete(long id)
        {
            var db = _context.Usuarios.Find(id);
            if (db is null) return false;

            _context.Usuarios.Remove(db);
            _context.SaveChanges();
            return true;
        }

        public bool EmailExists(string email, long? ignoreId = null) =>
            _context.Usuarios.AsNoTracking()
                .Any(u => u.Email == email && (!ignoreId.HasValue || u.Id != ignoreId.Value));
    }
}
