using Mentoria.Services.Mentoring.Application.Storage;

namespace Mentoria.Services.Mentoring.Application.Files.Delete
{
    public sealed class FileDeleteCommandHandler : IRequestHandler<FileDeleteCommand, ErrorOr<Unit>>
    {
        private readonly IBlobService _blobService;

        public FileDeleteCommandHandler(IBlobService blobService)
        {
            _blobService = blobService ?? throw new ArgumentNullException(nameof(blobService));
        }

        public async Task<ErrorOr<Unit>> Handle(FileDeleteCommand query, CancellationToken cancellationToken)
        {
            try
            {
                // Intentar eliminar el archivo
                var result = await _blobService.DeleteFileAsync(query.Id);

                // Usar Match para procesar el resultado
                return result.Match<ErrorOr<Unit>>(
                    _ => Unit.Value, // Caso exitoso: devolver Unit.Value
                    errors => errors // Caso de error: propagar los errores
                );
            }
            catch (Exception ex)
            {
                // Manejar excepciones inesperadas y devolver un error genérico
                return Error.Failure("ArchivoError", $"Ocurrió un error al intentar eliminar el archivo: {ex.Message}");
            }
        }
    }
}
