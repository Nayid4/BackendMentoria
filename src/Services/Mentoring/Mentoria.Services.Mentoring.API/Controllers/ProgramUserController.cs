using Mentoria.Services.Mentoring.API.Controller;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.AddToProgram;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.DeleteFromProgram;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramUsers.GetAll;
using Microsoft.AspNetCore.Authorization;

namespace Mentoria.Services.Mentoring.API.Controllers
{
    [Route("program-user")]
    [Authorize]
    public class ProgramUserController : ApiController
    {
        private readonly ISender _mediator;

        public ProgramUserController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> ListarTodos(Guid id)
        {
            var resultadosDeListarPorId = await _mediator.Send(new ProgramUserGetAllQuery(id));

            return resultadosDeListarPorId.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] AddUserToProgramCommand comando)
        {
            var resultadoDeCrear = await _mediator.Send(comando);

            return resultadoDeCrear.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }


        [HttpPost("delete-from-program/{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromBody] DeleteUserFromProgramCommand comando)
        {
            if (comando.IdProgram != id)
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

    
    }
}
