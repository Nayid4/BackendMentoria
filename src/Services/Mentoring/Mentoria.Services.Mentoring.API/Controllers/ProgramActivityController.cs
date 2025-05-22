using Mentoria.Services.Mentoring.API.Controller;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.AddToProgram;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.DeleteFromProgram;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.GetAll;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.GetById;
using Microsoft.AspNetCore.Authorization;

namespace Mentoria.Services.Mentoring.API.Controllers
{
    [Route("program-activity")]
    [Authorize]
    public class ProgramActivityController : ApiController
    {
        private readonly ISender _mediator;

        public ProgramActivityController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> ListarTodos(Guid id)
        {
            var resultadosDeListarPorId = await _mediator.Send(new ProgramActivityGetAllQuery(id));

            return resultadosDeListarPorId.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] AddProgramActivityToProgramCommand comando)
        {
            var resultadoDeCrear = await _mediator.Send(comando);

            return resultadoDeCrear.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }


        [HttpPost("delete-from-program/{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromBody] DeleteProgramActivityFromProgramCommand comando)
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
        [HttpPost("get-by-id-activity/{id}")]
        public async Task<IActionResult> GetById(Guid id, [FromBody] ProgramActivityGetByIdQuery comando)
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
