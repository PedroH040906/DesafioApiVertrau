// Application/Services/UsuarioService.cs
using desafio.Application.Services;
using desafio.Application.ViewModel;
using desafio.DTOS;
using desafio.Entities;
using desafio.Model;

namespace desafio.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repo;
    public UsuarioService(IUsuarioRepository repo) => _repo = repo;

    public List<UsuarioViewModel> Get(int page, int size) =>
        _repo.GetPaged(page, size).Select(Map).ToList();

    public UsuarioViewModel? GetById(long id) =>
        _repo.GetById(id) is { } u ? Map(u) : null;

    public UsuarioViewModel Create(UsuarioDTO dto)
    {
        if (_repo.EmailExists(dto.Email))
            throw new InvalidOperationException("Já existe usuário com este e-mail.");

        var u = new Usuario(dto.Nome, dto.SobreNome, dto.Email, dto.Genero, dto.DataNascimento);
        _repo.Add(u);
        return Map(u);
    }

    public UsuarioViewModel? Update(long id, UsuarioDTO dto)
    {
        var u = _repo.GetById(id);
        if (u is null) return null;

        if (!string.Equals(u.Email, dto.Email, StringComparison.Ordinal) &&
            _repo.EmailExists(dto.Email, id))
            throw new InvalidOperationException("Já existe usuário com este e-mail.");

        u.Nome = dto.Nome;
        u.SobreNome = dto.SobreNome;
        u.Email = dto.Email;
        u.Genero = dto.Genero;
        u.DataNascimento = dto.DataNascimento;

        _repo.Update(u);
        return Map(u);
    }

    public bool Delete(long id) => _repo.Delete(id);

    private static UsuarioViewModel Map(Usuario u) => new()
    {
        Id = u.Id,
        Nome = u.Nome,
        SobreNome = u.SobreNome,
        Email = u.Email,
        Genero = u.Genero,
        DataNascimento = u.DataNascimento
    };
}
