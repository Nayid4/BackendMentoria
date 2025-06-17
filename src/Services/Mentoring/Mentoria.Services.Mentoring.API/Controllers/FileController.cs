using Mentoria.Services.Mentoring.API.Controller;
using Mentoria.Services.Mentoring.Application.Files.Delete;
using Mentoria.Services.Mentoring.Application.Files.Download;
using Mentoria.Services.Mentoring.Application.Files.Upload;

namespace Mentoria.Services.Mentoring.API.Controllers
{
    [Route("file")]
    public class FileController : ApiController
    {
        private readonly ISender _mediador;

        public FileController(ISender mediador)
        {
            _mediador = mediador ?? throw new ArgumentNullException(nameof(mediador));
        }

        [HttpPost("upload")]
        public async Task<IActionResult> SubirArchivo(IFormFile archivo)
        {
            var resultado = await _mediador.Send(new FileUploadCommand(archivo));

            return resultado.Match(
                idArchivo => Ok(new { Id = idArchivo }),
                error => Problem(error)
            );
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DescargarArchivo(Guid id)
        {
            var resultado = await _mediador.Send(new FileDownloadQuery(id));

            return resultado.Match(
                resp => File(resp.Stream, resp.ContentType),
                error => Problem(error)
            );
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> EliminarArchivo(Guid id)
        {
            var resultado = await _mediador.Send(new FileDeleteCommand(id));

            return resultado.Match(
                _ => NoContent(),
                error => Problem(error)
            );
        }
    }
}
