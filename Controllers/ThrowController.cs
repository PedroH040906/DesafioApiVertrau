using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace desafio.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ThrowController : ControllerBase
    {
        [Route("/error")]
        public IActionResult HandleError()
        {
            // Não expõe detalhes em produção
            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title  = "Ocorreu um erro inesperado.",
                Type   = "https://httpwg.org/specs/rfc9110.html#name-500-internal-server-error",
                Instance = HttpContext.Request.Path
            };

            // Anexa um traceId para correlação com logs
            problem.Extensions["traceId"] =
                Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return StatusCode(problem.Status!.Value, problem);
        }

        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment env)
        {
            if (!env.IsDevelopment())
                return NotFound();

            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title  = ex?.Message,
                Detail = ex?.StackTrace,
                Type   = "https://httpwg.org/specs/rfc9110.html#name-500-internal-server-error",
                Instance = HttpContext.Request.Path
            };

            problem.Extensions["traceId"] =
                Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return StatusCode(problem.Status!.Value, problem);
        }
    }
}
