using Mentoria.Services.Mentoring.API.Controller;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.AddToProgram;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.DeleteFromProgram;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.GetAll;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivities.GetById;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.AddToProgramActivity;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.DeleteFromProgramActivity;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.GetAll;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.GetByIdUser;
using Mentoria.Services.Mentoring.Application.ProgramMentoring.ProgramActivitiesSolutions.UpdateCalification;
using Microsoft.AspNetCore.Authorization;

namespace Mentoria.Services.Mentoring.API.Controllers
{
    [Route("program-activity-solution")]
    [Authorize]
    public class ProgramActivitySolutionController : ApiController
    {
        private readonly ISender _mediator;

        public ProgramActivitySolutionController(ISender mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> ListarTodos(Guid id)
        {
            var resultadosDeListarPorId = await _mediator.Send(new ActivitySolutionGetAllQuery(id));

            return resultadosDeListarPorId.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] AddSolutionToProgramActivityCommand comando)
        {
            var resultadoDeCrear = await _mediator.Send(comando);

            return resultadoDeCrear.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }


        [HttpPost("delete-from-activity/{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromBody] DeleteSolutionCommand comando)
        {
            if (comando.IdProgramActivity != id)
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
        public async Task<IActionResult> GetById(Guid id, [FromBody] ActivitySolutionGetByIdUserQuery comando)
        {
            if (comando.IdProgramActivity != id)
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

        [HttpPost("update-calification/{id}")]
        public async Task<IActionResult> UpdateCalification(Guid id, [FromBody] ActivitySolutionUpdateCalificationCommand comando)
        {
            if (comando.IdProgramActivity != id)
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
