
using Mentoria.Services.Mentoring.API.Controller;
using Mentoria.Services.Mentoring.Application.MentorAssignments.AutomaticAssingment;
using Mentoria.Services.Mentoring.Application.MentorAssignments.Create;
using Mentoria.Services.Mentoring.Application.MentorAssignments.Delete;
using Mentoria.Services.Mentoring.Application.MentorAssignments.GetAll;
using Mentoria.Services.Mentoring.Application.MentorAssignments.GetById;
using Mentoria.Services.Mentoring.Application.MentorAssignments.GetByMentor;
using Mentoria.Services.Mentoring.Application.MentorAssignments.GetByUser;
using Mentoria.Services.Mentoring.Application.MentorAssignments.Update;
using Microsoft.AspNetCore.Authorization;

namespace Mentoria.Services.Mentoring.API.Controllers
{
    [Route("mentor-assignment")]
    public class MentorAssignmentController : ApiController
    {
        private readonly ISender _mediator;

        public MentorAssignmentController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ListarTodos()
        {
            var resultadosDeListarTodos = await _mediator.Send(new MentorAssignmentsGetAllQuery());

            return resultadosDeListarTodos.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(Guid id)
        {
            var resultadosDeListarPorId = await _mediator.Send(new MentorAssignmentsGetByIdQuery(id));

            return resultadosDeListarPorId.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] MentorAssignmentCreateCommand comando)
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
            var resultadoDeEliminar = await _mediator.Send(new MentorAssignmentsDeleteCommand(id));

            return resultadoDeEliminar.Match(
                resp => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] MentorAssignmentUpdateCommand comando)
        {
            if (comando.Id != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("MentorAssignment.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeActualizarListaTarea = await _mediator.Send(comando);

            return resultadoDeActualizarListaTarea.Match(
                resp => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpGet("get-by-user/{id}")]
        public async Task<IActionResult> GetByUser(Guid id)
        {
            
            var resultadoDeActualizarListaTarea = await _mediator.Send(new MentorAssignmentsGetByUserQuery(id));

            return resultadoDeActualizarListaTarea.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }

        [HttpGet("get-by-mentor/{id}")]
        public async Task<IActionResult> GetByMentor(Guid id)
        {

            var resultadoDeActualizarListaTarea = await _mediator.Send(new MentorAssignmentsGetByMentorQuery(id));

            return resultadoDeActualizarListaTarea.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }

        [HttpGet("assignment-automatic")]
        public async Task<IActionResult> AssignmentsActumatic()
        {
            
            var resultadoDeActualizarListaTarea = await _mediator.Send(new MentorAssignmentAutomaticCommand());

            return resultadoDeActualizarListaTarea.Match(
                resp => NoContent(),
                errores => Problem(errores)
            );
        }
    }
}
