
using Mentoria.Services.Mentoring.Application.Careers.Create;
//using Mentoria.Services.Mentoring.Application.Careers.Delete;
using Mentoria.Services.Mentoring.Application.Careers.GetAll;/*
using Mentoria.Services.Mentoring.Application.Careers.GetByFilter;
using Mentoria.Services.Mentoring.Application.Careers.GetById;
using Mentoria.Services.Mentoring.Application.Careers.Update;*/
using Microsoft.AspNetCore.Authorization;

namespace Mentoria.Services.Mentoring.API.Controller
{
    [Route("career")]
    [Authorize]
    public class CareerController : ApiController
    {
        private readonly ISender _mediator;

        public CareerController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var resultadosDeListarTodos = await _mediator.Send(new CareerGetAllQuery());

            return resultadosDeListarTodos.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }

        /*[HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(Guid id)
        {
            var resultadosDeListarPorId = await _mediator.Send(new CareerGetByIdQuery(id));

            return resultadosDeListarPorId.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }*/


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] CareerCreateCommand comando)
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
            var resultadoDeEliminar = await _mediator.Send(new CareerDeleteCommand(id));

            return resultadoDeEliminar.Match(
                resp => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] CareerUpdateCommand comando)
        {
            if (comando.Id != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("Career.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
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
        public async Task<IActionResult> ListarPorFiltro([FromBody] CareerGetByFilterQuery comando)
        {
            var resultadoDeFiltrar = await _mediator.Send(comando);

            return resultadoDeFiltrar.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }*/
    }
}
