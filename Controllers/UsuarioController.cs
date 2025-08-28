// Controllers/UsuarioController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using desafio.Application.Services;
using desafio.DTOS;
using desafio.Application.ViewModel;

[ApiController]
[Route("api/v1/usuarios")]
[Produces("application/json")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _service;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(IUsuarioService service, ILogger<UsuarioController> logger)
    {
        _service = service;
        _logger  = logger;
    }

    // GET /api/v1/usuarios?page=1&size=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UsuarioViewModel>), StatusCodes.Status200OK)]
    public IActionResult Get([FromQuery] int page = 1, [FromQuery] int size = 10)
        => Ok(_service.Get(page, size));

    // GET /api/v1/usuarios/123
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(long id)
    {
        var vm = _service.GetById(id);
        if (vm is null)
        {
            return NotFound(new ProblemDetails
            {
                Title  = "Usuário não encontrado",
                Detail = $"Nenhum usuário com ID {id} foi localizado.",
                Status = StatusCodes.Status404NotFound
            });
        }
        return Ok(vm);
    }

    // POST /api/v1/usuarios
    [HttpPost]
    [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Post([FromBody] UsuarioDTO dto)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        try
        {
            var vm = _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = vm.Id }, vm);
        }
        catch (InvalidOperationException ex)
        {
            // conflito de negócio (ex.: e-mail já existente)
            return Conflict(new ProblemDetails
            {
                Title  = "Conflito de negócio",
                Detail = ex.Message,
                Status = StatusCodes.Status409Conflict
            });
        }
    }

    // PUT /api/v1/usuarios/123
    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Put(long id, [FromBody] UsuarioDTO dto)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        try
        {
            var vm = _service.Update(id, dto);
            if (vm is null)
            {
                return NotFound(new ProblemDetails
                {
                    Title  = "Usuário não encontrado",
                    Detail = $"Não foi possível atualizar: usuário {id} não existe.",
                    Status = StatusCodes.Status404NotFound
                });
            }
            return Ok(vm);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new ProblemDetails
            {
                Title  = "Conflito de negócio",
                Detail = ex.Message,
                Status = StatusCodes.Status409Conflict
            });
        }
    }

    // DELETE /api/v1/usuarios/123
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(long id)
    {
        return _service.Delete(id)
            ? NoContent()
            : NotFound(new ProblemDetails
            {
                Title  = "Usuário não encontrado",
                Detail = $"Não foi possível excluir: usuário {id} não existe.",
                Status = StatusCodes.Status404NotFound
            });
    }
}
