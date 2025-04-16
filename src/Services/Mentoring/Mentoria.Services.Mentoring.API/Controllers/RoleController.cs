
using Mentoria.Services.Mentoring.Application.Roles.Create;
//using Mentoria.Services.Mentoring.Application.Roles.Delete;
using Mentoria.Services.Mentoring.Application.Roles.GetAll;/*
using Mentoria.Services.Mentoring.Application.Roles.GetByFilter;
using Mentoria.Services.Mentoring.Application.Roles.GetById;
using Mentoria.Services.Mentoring.Application.Roles.Update;*/
using Microsoft.AspNetCore.Authorization;

namespace Mentoria.Services.Mentoring.API.Controller
{
    [Route("role")]
    [Authorize]
    public class RoleController : ApiController
    {
        private readonly ISender _mediator;

        public RoleController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var resultadosDeListarTodos = await _mediator.Send(new RoleGetAllQuery());

            return resultadosDeListarTodos.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }

        /*[HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(Guid id)
        {
            var resultadosDeListarPorId = await _mediator.Send(new RoleGetByIdQuery(id));

            return resultadosDeListarPorId.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }*/


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] RoleCreateCommand comando)
        {
            var resultadoDeCrear = await _mediator.Send(comando);

            return resultadoDeCrear.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }

        /*
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var resultadoDeEliminar = await _mediator.Send(new RoleDeleteCommand(id));

            return resultadoDeEliminar.Match(
                resp => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] RoleUpdateCommand comando)
        {
            if (comando.Id != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("Role.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
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
        public async Task<IActionResult> ListarPorFiltro([FromBody] RoleGetByFilterQuery comando)
        {
            var resultadoDeFiltrar = await _mediator.Send(comando);

            return resultadoDeFiltrar.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }*/
    }
}
