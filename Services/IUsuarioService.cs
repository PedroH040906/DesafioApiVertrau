using desafio.Application.ViewModel;
using desafio.DTOS;

namespace desafio.Application.Services
{
    public interface IUsuarioService
    {
        List<UsuarioViewModel> Get(int page, int size);
        UsuarioViewModel? GetById(long id);
        UsuarioViewModel Create(UsuarioDTO dto);

        UsuarioViewModel? Update(long id, UsuarioDTO dto);

        bool Delete(long id);
    }
}
