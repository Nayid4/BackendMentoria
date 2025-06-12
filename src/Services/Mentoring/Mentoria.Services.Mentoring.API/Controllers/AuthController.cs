
using Mentoria.Services.Mentoring.Application.Auth.ForgetPassword;
using Mentoria.Services.Mentoring.Application.Auth.LoginIn;
using Mentoria.Services.Mentoring.Application.Auth.RefreshToken;
using Mentoria.Services.Mentoring.Application.Auth.UserData;
using Microsoft.AspNetCore.Authorization;

namespace Mentoria.Services.Mentoring.API.Controller
{
    [Route("auth")]
    
    public class AuthController : ApiController
    {
        private readonly ISender _mediator;

        public AuthController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("login-in")]
        [AllowAnonymous]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginInQuery comando)
        {
            var resultadoDeIniciarSesion = await _mediator.Send(comando);

            return resultadoDeIniciarSesion.Match(
                auth => Ok(auth),
                errores => Problem(errores)
            );
        }

        [HttpGet("refresh-token")]
        [Authorize]
        public async Task<IActionResult> RefescarToken()
        {
            var resultado = await _mediator.Send(new RefreshTokenQuery());

            return resultado.Match(
                auth => Ok(auth),
                errores => Problem(errores)
            );
        }

        [HttpGet("data-user")]
        [Authorize]
        public async Task<IActionResult> ListarDatosUsuario()
        {
            var resultadoDeListarDatosUsuario = await _mediator.Send(new UserDataQuery());

            return resultadoDeListarDatosUsuario.Match(
                usuarioid => Ok(usuarioid),
                errores => Problem(errores)
            );
        }

        [HttpPut("forget-password/{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ForgetPasswordCommand comando)
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
    }
}
