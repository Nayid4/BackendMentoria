
using Mentoria.Services.Mentoring.API.Controller;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Create;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Delete;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.GetAll;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.GetByFilter;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.GetById;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.Programs.Update;
using Microsoft.AspNetCore.Authorization;

namespace Mentoria.Services.Mentoring.API.Controllers
{
    [Route("program")]
    [Authorize]
    public class ProgramController : ApiController
    {
        private readonly ISender _mediator;

        public ProgramController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ListarTodos()
        {
            var resultadosDeListarTodos = await _mediator.Send(new ProgramGetAllQuery());

            return resultadosDeListarTodos.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(Guid id)
        {
            var resultadosDeListarPorId = await _mediator.Send(new ProgramGetByIdQuery(id));

            return resultadosDeListarPorId.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] ProgramCreateCommand comando)
        {
            var resultadoDeCrear = await _mediator.Send(comando);

            return resultadoDeCrear.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var resultadoDeEliminar = await _mediator.Send(new ProgramDeleteCommand(id));

            return resultadoDeEliminar.Match(
                resp => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ProgramUpdateCommand comando)
        {
            if (comando.Id != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("Usuario.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeActualizarListaTarea = await _mediator.Send(comando);

            return resultadoDeActualizarListaTarea.Match(
                resp => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPost("list-filter")]
        public async Task<IActionResult> ListarPorFiltro([FromBody] ProgramGetByFilterQuery comando)
        {
            var resultadoDeFiltrar = await _mediator.Send(comando);

            return resultadoDeFiltrar.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }
    }
}
