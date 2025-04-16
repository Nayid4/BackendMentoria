using Mentoria.Services.Mentoring.Application.Storage;
using Mentoria.Services.Mentoring.Application.Common.Dtos;

namespace Mentoria.Services.Mentoring.Application.Files.Download
{
    public sealed class FileDownloadQueryHandler : IRequestHandler<FileDownloadQuery, ErrorOr<FileResponse>>
    {
        private readonly IBlobService _blobService;

        public FileDownloadQueryHandler(IBlobService blobService)
        {
            _blobService = blobService ?? throw new ArgumentNullException(nameof(blobService));
        }

        public async Task<ErrorOr<FileResponse>> Handle(FileDownloadQuery query, CancellationToken cancellationToken)
        {
            try
            {
                // Intentar descargar el archivo desde el servicio
                var result = await _blobService.DownloadFileAsync(query.Id);

                // Usar Match para procesar el resultado
                return result.Match<ErrorOr<FileResponse>>(
                    file => file, // Caso exitoso: devolver el archivo descargado
                    errors => errors // Caso de error: propagar los errores
                );
            }
            catch (Exception ex)
            {
                // Manejar excepciones inesperadas y devolver un error genérico
                return Error.Failure("ArchivoError", $"Ocurrió un error al intentar descargar el archivo: {ex.Message}");
            }
        }
    }
}
